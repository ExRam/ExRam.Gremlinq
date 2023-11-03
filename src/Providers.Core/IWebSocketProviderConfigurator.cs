﻿using ExRam.Gremlinq.Core;

namespace ExRam.Gremlinq.Providers.Core
{
    public interface IWebSocketProviderConfigurator<out TSelf> : IGremlinqConfigurator<TSelf>
        where TSelf : IGremlinqConfigurator<TSelf>
    {
        TSelf ConfigureClientFactory(Func<IGremlinClientFactory, IGremlinClientFactory> transformation);
    }
}
