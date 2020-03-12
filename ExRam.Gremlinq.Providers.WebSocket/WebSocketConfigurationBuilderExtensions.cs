﻿using System;
using Gremlin.Net.Driver;

namespace ExRam.Gremlinq.Providers.WebSocket
{
    public static class WebSocketConfigurationBuilderExtensions
    {
        public static IWebSocketConfigurationBuilder At(this IWebSocketConfigurationBuilder builder, string uri)
        {
            return builder.At(new Uri(uri));
        }

        public static IWebSocketConfigurationBuilder AtLocalhost(this IWebSocketConfigurationBuilder builder)
        {
            return builder.At(new Uri("ws://localhost:8182"));
        }

        public static IWebSocketConfigurationBuilder ConfigureConnectionPool(this IWebSocketConfigurationBuilder builder, Func<ConnectionPoolSettings> connectionPoolSettings)
        {
            return builder.ConfigureConnectionPool(connectionPoolSettings);
        }
    }
}
