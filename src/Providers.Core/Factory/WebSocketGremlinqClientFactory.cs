﻿using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;

using ExRam.Gremlinq.Core;

using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Messages;

using static Gremlin.Net.Driver.Messages.ResponseStatusCode;

namespace ExRam.Gremlinq.Providers.Core
{
    public static class WebSocketGremlinqClientFactory
    {
        private sealed class WebSocketGremlinqClientFactoryImpl : IWebSocketGremlinqClientFactory
        {
            internal sealed class WebSocketGremlinqClient : IGremlinqClient
            {
                private abstract class Channel
                {
                    public abstract void Signal(ReadOnlyMemory<byte> bytes);
                }

                private sealed class Channel<T> : Channel, IAsyncEnumerable<ResponseMessage<T>>, IDisposable
                {
                    private SemaphoreSlim? _semaphore;
                    private ConcurrentQueue<object>? _queue;

                    private readonly IGremlinQueryEnvironment _environment;

                    public Channel(Guid requestId, IGremlinQueryEnvironment environment)
                    {
                        RequestId = requestId;
                        _environment = environment;
                    }

                    public override void Signal(ReadOnlyMemory<byte> bytes)
                    {
                        var (semaphore, queue) = GetTuple();

                        try
                        {
                            if (_environment.Deserializer.TryTransform(bytes, _environment, out ResponseMessage<T>? response))
                            {
                                queue.Enqueue(response);
                                semaphore.Release();
                            }
                        }
                        catch (Exception ex)
                        {
                            queue.Enqueue(ex);
                            semaphore.Release();
                        }
                    }

                    public async IAsyncEnumerator<ResponseMessage<T>> GetAsyncEnumerator(CancellationToken ct = default)
                    {
                        var (semaphore, queue) = GetTuple();

                        while (true)
                        {
                            await semaphore.WaitAsync(ct);

                            if (queue.TryDequeue(out var item))
                            {
                                if (item is ResponseMessage<T> response)
                                {
                                    yield return response;

                                    if (response.Status.Code != PartialContent)
                                        break;
                                }
                                else if (item is Exception ex)
                                    throw ex;
                            }
                        }
                    }

                    private (SemaphoreSlim, ConcurrentQueue<object>) GetTuple()
                    {
                        if (_semaphore is { } semaphore)
                        {
                            if (_queue is { } queue)
                                return (semaphore, queue);

                            Interlocked.CompareExchange(ref _queue, new ConcurrentQueue<object>(), null);
                            return GetTuple();
                        }
                        else
                        {
                            var newSemaphore = new SemaphoreSlim(0);
                            if (Interlocked.CompareExchange(ref _semaphore, newSemaphore, null) != null)
                                newSemaphore.Dispose();
                        }

                        return GetTuple();
                    }

                    public void Dispose()
                    {
                        _semaphore?.Dispose();
                    }

                    public Guid RequestId { get; }
                }

                private record struct ResponseStatus(ResponseStatusCode Code);

                private record struct ResponseMessageEnvelope(Guid? RequestId, ResponseStatus? Status);

                private readonly Task _loopTask;
                private readonly GremlinServer _server;
                private readonly ClientWebSocket _client = new();
                private readonly SemaphoreSlim _sendLock = new(1);
                private readonly CancellationTokenSource _cts = new();
                private readonly IGremlinQueryEnvironment _environment;
                private readonly TaskCompletionSource _startTcs = new();
                private readonly ConcurrentDictionary<Guid, Channel> _channels = new();

                public WebSocketGremlinqClient(GremlinServer server, Action<ClientWebSocketOptions> optionsTransformation, IGremlinQueryEnvironment environment)
                {
                    _server = server;
                    _environment = environment;

                    _client.Options.SetRequestHeader("User-Agent", "ExRam.Gremlinq");

                    optionsTransformation(_client.Options);

                    _loopTask = Loop(_cts.Token);
                }

                public IAsyncEnumerable<ResponseMessage<T>> SubmitAsync<T>(RequestMessage message)
                {
                    return Core(message, this);

                    static async IAsyncEnumerable<ResponseMessage<T>> Core(RequestMessage message, WebSocketGremlinqClient @this, [EnumeratorCancellation] CancellationToken ct = default)
                    {
                        if (@this._loopTask.IsCompleted)
                            await @this._loopTask;

                        using (var channel = new Channel<T>(message.RequestId, @this._environment))
                        {
                            using (var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct, @this._cts.Token))
                            {
                                @this._channels.TryAdd(message.RequestId, channel);

                                try
                                {
                                    await @this.SendCore(message, ct);

                                    await foreach (var item in channel.WithCancellation(linkedCts.Token))
                                    {
                                        yield return item;
                                    }
                                }
                                finally
                                {
                                    @this._channels.TryRemove(message.RequestId, out _);
                                }
                            }
                        }
                    }
                }

                public void Dispose()
                {
                    using (_sendLock)
                    {
                        using (_client)
                        {
                            _cts.Cancel();
                            _startTcs.TrySetException(new ObjectDisposedException(nameof(WebSocketGremlinqClient)));
                        }
                    }
                }

                private async Task Loop(CancellationToken ct)
                {
                    await _startTcs.Task;

                    using (this)
                    {
                        while (true)
                        {
                            var bytes = await _client.ReceiveAsync(ct);

                            using (bytes)
                            {
                                if (_environment.Deserializer.TryTransform(bytes.Memory, _environment, out ResponseMessageEnvelope responseMessageEnvelope))
                                {
                                    if (responseMessageEnvelope is { Status.Code: var statusCode, RequestId: { } requestId })
                                    {
                                        if (statusCode == Authenticate)
                                        {
                                            var authMessage = RequestMessage
                                                .Build(Tokens.OpsAuthentication)
                                                .Processor(Tokens.ProcessorTraversal)
                                                .AddArgument(Tokens.ArgsSasl, Convert.ToBase64String(Encoding.UTF8.GetBytes($"\0{_server.Username}\0{_server.Password}")))
                                                .Create();

                                            await SendCore(authMessage, ct);
                                        }
                                        else
                                        {
                                            if (_channels.TryGetValue(requestId, out var otherChannel))
                                                otherChannel.Signal(bytes.Memory);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                private async Task SendCore(RequestMessage requestMessage, CancellationToken ct)
                {
                    await _sendLock.WaitAsync(ct);

                    try
                    {
                        if (_client.State == WebSocketState.None)
                        {
                            await _client.ConnectAsync(_server.Uri, ct);
                            _startTcs.TrySetResult();
                        }

                        if (_environment.Serializer.TryTransform(requestMessage, _environment, out byte[]? serializedRequest))
                            await _client.SendAsync(serializedRequest, WebSocketMessageType.Binary, true, ct);
                    }
                    catch
                    {
                        Dispose();

                        throw;
                    }
                    finally
                    {
                        _sendLock.Release();
                    }
                }
            }

            public static readonly WebSocketGremlinqClientFactoryImpl LocalHost = new(new GremlinServer(), _ => { });

            private readonly GremlinServer _server;
            private readonly Action<ClientWebSocketOptions> _webSocketOptionsConfiguration;

            private WebSocketGremlinqClientFactoryImpl(GremlinServer server, Action<ClientWebSocketOptions> webSocketOptionsConfiguration)
            {
                if (!"ws".Equals(server.Uri.Scheme, StringComparison.OrdinalIgnoreCase) && !"wss".Equals(server.Uri.Scheme, StringComparison.OrdinalIgnoreCase))
                    throw new ArgumentException("Expected the Uri-Scheme to be either \"ws\" or \"wss\".");

                _server = server;
                _webSocketOptionsConfiguration = webSocketOptionsConfiguration;
            }

            public IWebSocketGremlinqClientFactory ConfigureServer(Func<GremlinServer, GremlinServer> transformation) => new WebSocketGremlinqClientFactoryImpl(transformation(_server), _webSocketOptionsConfiguration);

            public IWebSocketGremlinqClientFactory ConfigureOptions(Action<ClientWebSocketOptions> configuration) => new WebSocketGremlinqClientFactoryImpl(
                _server,
                options =>
                {
                    _webSocketOptionsConfiguration(options);
                    configuration(options);
                });

            public IGremlinqClient Create(IGremlinQueryEnvironment environment) => new WebSocketGremlinqClient(_server, _webSocketOptionsConfiguration, environment);
        }

        public static readonly IWebSocketGremlinqClientFactory LocalHost = WebSocketGremlinqClientFactoryImpl.LocalHost;
    }
}
