﻿using System;
using System.Net.WebSockets;
using Gremlin.Net.Driver;

namespace ExRam.Gremlinq.Providers.WebSocket
{
    public static class GremlinClientFactory
    {
        private sealed class DefaultGremlinClientFactory : IGremlinClientFactory
        {
            public IGremlinClient Create(Gremlin.Net.Driver.GremlinServer gremlinServer, IMessageSerializer? messageSerializer = null, ConnectionPoolSettings? connectionPoolSettings = null, Action<ClientWebSocketOptions>? webSocketConfiguration = null, string? sessionId = null)
            {
                return new GremlinClient(
                    gremlinServer,
                    messageSerializer,
                    connectionPoolSettings,
                    webSocketConfiguration,
                    sessionId);
            }
        }

        private sealed class FuncGremlinClientFactory : IGremlinClientFactory
        {
            private readonly Func<Gremlin.Net.Driver.GremlinServer, IMessageSerializer?, ConnectionPoolSettings?, Action<ClientWebSocketOptions>?, string?, IGremlinClient> _factory;

            public FuncGremlinClientFactory(Func<Gremlin.Net.Driver.GremlinServer, IMessageSerializer?, ConnectionPoolSettings?, Action<ClientWebSocketOptions>?, string?, IGremlinClient> factory)
            {
                _factory = factory;
            }

            public IGremlinClient Create(Gremlin.Net.Driver.GremlinServer gremlinServer, IMessageSerializer? messageSerializer = null, ConnectionPoolSettings? connectionPoolSettings = null, Action<ClientWebSocketOptions>? webSocketConfiguration = null, string? sessionId = null) => _factory(gremlinServer, messageSerializer, connectionPoolSettings, webSocketConfiguration, sessionId);
        }

        public static readonly IGremlinClientFactory Default = new DefaultGremlinClientFactory();

        public static IGremlinClientFactory Create(Func<Gremlin.Net.Driver.GremlinServer, IMessageSerializer?, ConnectionPoolSettings?, Action<ClientWebSocketOptions>?, string?, IGremlinClient> factory) => new FuncGremlinClientFactory(factory);
    }
}
