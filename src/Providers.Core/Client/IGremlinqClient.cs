﻿using Gremlin.Net.Driver.Messages;

namespace ExRam.Gremlinq.Providers.Core
{
    public interface IGremlinqClient : IDisposable
    {
        Task<ResponseMessage<T>> SubmitAsync<T>(RequestMessage message, CancellationToken ct);
    }
}