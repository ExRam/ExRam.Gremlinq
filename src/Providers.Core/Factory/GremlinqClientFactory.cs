﻿using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

using ExRam.Gremlinq.Core;
using ExRam.Gremlinq.Core.Execution;

using Gremlin.Net.Driver.Exceptions;
using Gremlin.Net.Driver.Messages;

using static Gremlin.Net.Driver.Messages.ResponseStatusCode;

namespace ExRam.Gremlinq.Providers.Core
{
    public static class GremlinqClientFactory
    {
        private sealed class ConfigureClientGremlinClientFactory : IGremlinqClientFactory
        {
            private readonly IGremlinqClientFactory _baseFactory;
            private readonly Func<IGremlinqClient, IGremlinQueryEnvironment, IGremlinqClient> _clientTransformation;

            public ConfigureClientGremlinClientFactory(IGremlinqClientFactory baseFactory, Func<IGremlinqClient, IGremlinQueryEnvironment, IGremlinqClient> clientTransformation)
            {
                _baseFactory = baseFactory;
                _clientTransformation = clientTransformation;
            }

            public IGremlinqClient Create(IGremlinQueryEnvironment environment) => _clientTransformation(_baseFactory.Create(environment), environment);
        }

        private sealed class PoolGremlinqClientFactory<TBaseFactory> : IPoolGremlinqClientFactory<TBaseFactory>
            where TBaseFactory : IGremlinqClientFactory
        {
            private sealed class PoolGremlinqClient : IGremlinqClient
            {
                private sealed class Slot : IDisposable
                {
                    private readonly PoolGremlinqClient _poolClient;

                    private IGremlinqClient? _subClient;

                    public Slot(PoolGremlinqClient poolClient)
                    {
                        _poolClient = poolClient;
                    }

                    public IGremlinqClient GetClient()
                    {
                        if (_subClient is { } subClient)
                        {
                            return subClient != GremlinqClient.Disposed
                                ? subClient
                                : throw new ObjectDisposedException(nameof(Slot));
                        }

                        Interlocked.CompareExchange(ref _subClient, _poolClient._baseFactory.Create(_poolClient._environment), null);

                        return GetClient();
                    }

                    public void Invalidate(IGremlinqClient client)
                    {
                        Interlocked.CompareExchange(ref _subClient, null, client);
                    }

                    public void Dispose()
                    {
                        if (Interlocked.Exchange(ref _subClient, GremlinqClient.Disposed) is { } subClient)
                            subClient.Dispose();
                    }
                }

                private readonly struct SlotLease : IDisposable
                {
                    public SlotLease(ConcurrentStack<Slot> slots, Slot slot)
                    {
                        Slot = slot;
                        Slots = slots;
                    }

                    public void Dispose()
                    {
                        Slots.Push(Slot);
                    }

                    public PoolGremlinqClientFactory<TBaseFactory>.PoolGremlinqClient.Slot Slot { get; }
                    public ConcurrentStack<PoolGremlinqClientFactory<TBaseFactory>.PoolGremlinqClient.Slot> Slots { get; }
                }

                private readonly IGremlinqClientFactory _baseFactory;
                private readonly IGremlinQueryEnvironment _environment;

                private readonly Slot[] _allSlots;
                private readonly ConcurrentStack<Slot>[] _pages;

                public PoolGremlinqClient(IGremlinqClientFactory baseFactory, uint poolSize, uint maxInProcessPerConnection, IGremlinQueryEnvironment environment)
                {
                    _baseFactory = baseFactory;
                    _environment = environment;

                    _allSlots = new Slot[poolSize];
                    _pages = new ConcurrentStack<Slot>[maxInProcessPerConnection];

                    for (var i = 0 ; i < poolSize; i++)
                    {
                        _allSlots[i] = new Slot(this);
                    }

                    for (var i = 0; i < maxInProcessPerConnection; i++)
                    {
                        _pages[i] = new ();

                        for (var j = 0 ; j < poolSize; j++)
                        {
                            _pages[i].Push(_allSlots[j]);
                        }
                    }
                }

                public IAsyncEnumerable<ResponseMessage<T>> SubmitAsync<T>(RequestMessage message)
                {
                    return Core(message, this);

                    static async IAsyncEnumerable<ResponseMessage<T>> Core(RequestMessage message, PoolGremlinqClient @this, [EnumeratorCancellation] CancellationToken ct = default)
                    {
                        using (var lease = @this.GetSlot())
                        {
                            var slot = lease.Slot;

                            var client = slot.GetClient();

                            while (true)
                            {
                                await using (var e = client.SubmitAsync<T>(message).WithCancellation(ct).GetAsyncEnumerator())
                                {
                                    while (true)
                                    {
                                        try
                                        {
                                            if (!await e.MoveNextAsync())
                                                break;
                                        }
                                        catch
                                        {
                                            slot.Invalidate(client);

                                            throw;
                                        }

                                        yield return e.Current;
                                    }
                                }

                                yield break;
                            }
                        }
                    }
                }

                public void Dispose()
                {
                    foreach (var slot in _allSlots)
                    {
                        slot.Dispose();
                    }
                }

                private SlotLease GetSlot()
                {
                    for (var i = 0; i < _pages.Length; i++)
                    {
                        if (_pages[i].TryPop(out var slot))
                            return new SlotLease(_pages[i], slot);
                    }

                    throw new InvalidOperationException();
                }
            }

            private readonly uint _poolSize;
            private readonly TBaseFactory _baseFactory;
            private readonly uint _maxInProcessPerConnection;

            public PoolGremlinqClientFactory(TBaseFactory baseFactory) : this(baseFactory, 8, 16)
            {
            }

            public PoolGremlinqClientFactory(TBaseFactory baseFactory, uint poolSize, uint maxInProcessPerConnection)
            {
                _poolSize = poolSize;
                _baseFactory = baseFactory;
                _maxInProcessPerConnection = maxInProcessPerConnection;
            }

            public IPoolGremlinqClientFactory<TBaseFactory> ConfigureBaseFactory(Func<TBaseFactory, TBaseFactory> transformation) => new PoolGremlinqClientFactory<TBaseFactory>(transformation(_baseFactory));

            public IPoolGremlinqClientFactory<TBaseFactory> WithMaxInProcessPerConnection(uint maxInProcessPerConnection) => maxInProcessPerConnection <= 64
                ? new PoolGremlinqClientFactory<TBaseFactory>(_baseFactory, _poolSize, maxInProcessPerConnection)
                : throw new ArgumentOutOfRangeException(nameof(maxInProcessPerConnection));

            public IPoolGremlinqClientFactory<TBaseFactory> WithPoolSize(uint poolSize) => poolSize <= 8
                ? new PoolGremlinqClientFactory<TBaseFactory>(_baseFactory, poolSize, _maxInProcessPerConnection)
                : throw new ArgumentOutOfRangeException(nameof(poolSize));

            public IGremlinqClient Create(IGremlinQueryEnvironment environment) => new PoolGremlinqClient(_baseFactory, _poolSize, _maxInProcessPerConnection, environment);
        }

        private sealed class GremlinQueryExecutorImpl : IGremlinQueryExecutor
        {
            private readonly IGremlinqClientFactory _clientFactory;
            private readonly ConcurrentDictionary<IGremlinQueryEnvironment, IGremlinqClient> _clients = new();

            public GremlinQueryExecutorImpl(IGremlinqClientFactory clientFactory)
            {
                _clientFactory = clientFactory;
            }

            public IAsyncEnumerable<T> Execute<T>(GremlinQueryExecutionContext context)
            {
                return Core(this, context);

                async static IAsyncEnumerable<T> Core(GremlinQueryExecutorImpl @this, GremlinQueryExecutionContext context, [EnumeratorCancellation] CancellationToken ct = default)
                {
                    var environment = context.Query
                        .AsAdmin()
                        .Environment;

                    var client = @this._clients.GetOrAdd(
                        environment,
                        static (environment, executor) => executor._clientFactory.Create(environment),
                        @this);

                    var requestMessage = environment
                        .Serializer
                        .TransformTo<RequestMessage>()
                        .From(context.Query, environment)
                        .Rebuild()
                        .OverrideRequestId(context.ExecutionId)
                        .Create();

                    await using (var e = client.SubmitAsync<List<T>>(requestMessage).GetAsyncEnumerator(ct))
                    {
                        while (true)
                        {
                            try
                            {
                                if (!await e.MoveNextAsync())
                                    break;
                            }
                            catch (ConnectionClosedException ex)
                            {
                                throw new GremlinQueryExecutionException(context, ex);
                            }
                            catch (NoConnectionAvailableException ex)
                            {
                                throw new GremlinQueryExecutionException(context, ex);
                            }

                            var response = e.Current;

                            if (response is { Status: { Code: { } code } status } && code is not Success and not NoContent and not PartialContent and not Authenticate)
                                throw new GremlinQueryExecutionException(context, new ResponseException(code, status.Attributes, $"{status.Code}: {status.Message}"));

                            if (response is { Result.Data: { } data })
                            {
                                foreach (var obj in data)
                                {
                                    yield return obj;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static IGremlinqClientFactory ConfigureClient(this IGremlinqClientFactory clientFactory, Func<IGremlinqClient, IGremlinqClient> clientTransformation) => new ConfigureClientGremlinClientFactory(clientFactory, (client, _) => clientTransformation(client));

        public static IPoolGremlinqClientFactory<TBaseFactory> Pool<TBaseFactory>(this TBaseFactory baseFactory)
            where TBaseFactory : IGremlinqClientFactory => new PoolGremlinqClientFactory<TBaseFactory>(baseFactory);

        public static IGremlinqClientFactory Log(this IGremlinqClientFactory clientFactory) => new ConfigureClientGremlinClientFactory(clientFactory, (client, environment) => client.Log(environment));

        public static IGremlinQueryExecutor ToExecutor(this IGremlinqClientFactory clientFactory) => new GremlinQueryExecutorImpl(clientFactory);
    }
}
