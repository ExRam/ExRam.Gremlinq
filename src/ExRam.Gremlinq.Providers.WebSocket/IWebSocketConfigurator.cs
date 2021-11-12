﻿using System;
using ExRam.Gremlinq.Core;
using _GremlinServer = Gremlin.Net.Driver.GremlinServer;

namespace ExRam.Gremlinq.Providers.WebSocket
{
    public interface IWebSocketConfigurator : IGremlinQuerySourceTransformation
    {
        IWebSocketConfigurator ConfigureAlias(Func<string, string> transformation);

        IWebSocketConfigurator ConfigureGremlinServer(Func<_GremlinServer, _GremlinServer> transformation);

        IWebSocketConfigurator ConfigureGremlinClientFactory(Func<IGremlinClientFactory, IGremlinClientFactory> transformation);
    }
}
