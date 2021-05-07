﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gremlin.Net.Driver.Exceptions;

namespace ExRam.Gremlinq.Core
{
    public static class GremlinQueryExecutorExtensions
    {
        private sealed class ExponentialBackoffExecutor : IGremlinQueryExecutor
        {
            [ThreadStatic]
            private static Random? _rnd;

            private const int MaxTries = 32;

            private readonly IGremlinQueryExecutor _baseExecutor;
            private readonly Func<int, ResponseException, bool> _shouldRetry;

            public ExponentialBackoffExecutor(IGremlinQueryExecutor baseExecutor, Func<int, ResponseException, bool> shouldRetry)
            {
                _baseExecutor = baseExecutor;
                _shouldRetry = shouldRetry;
            }

            public IAsyncEnumerable<object> Execute(object serializedQuery, IGremlinQueryEnvironment environment)
            {
                return AsyncEnumerable.Create(Core);

                async IAsyncEnumerator<object> Core(CancellationToken ct)
                {
                    var hasSeenFirst = false;

                    for (var i = 0; i < MaxTries; i++)
                    {
                        await using (var enumerator = _baseExecutor.Execute(serializedQuery, environment).GetAsyncEnumerator(ct))
                        {
                            while (true)
                            {
                                try
                                {
                                    if (!await enumerator.MoveNextAsync())
                                        yield break;

                                    hasSeenFirst = true;
                                }
                                catch (ResponseException ex)
                                {
                                    if (hasSeenFirst)
                                        throw;

                                    if (!_shouldRetry(i, ex))
                                        throw;
                                    
                                    await Task.Delay((_rnd ??= new Random()).Next(i + 2) * 16, ct);
                                    break;
                                }

                                yield return enumerator.Current;
                            }
                        }
                    }
                }
            }
        }

        public static IGremlinQueryExecutor RetryWithExponentialBackoff(this IGremlinQueryExecutor executor, Func<int, ResponseException, bool> shouldRetry)
        {
            return new ExponentialBackoffExecutor(executor, shouldRetry);
        }
    }
}
