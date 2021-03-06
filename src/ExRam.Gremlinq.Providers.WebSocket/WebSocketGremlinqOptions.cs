﻿using ExRam.Gremlinq.Core;
using ExRam.Gremlinq.Core.Serialization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ExRam.Gremlinq.Providers.WebSocket
{
    public static class WebSocketGremlinqOptions
    {
        public static GremlinqOption<LogLevel> QueryLogLogLevel = new(LogLevel.Debug);
        public static GremlinqOption<Formatting> QueryLogFormatting = new(Formatting.None);
        public static GremlinqOption<QueryLogVerbosity> QueryLogVerbosity = new(Gremlinq.Core.QueryLogVerbosity.QueryOnly);
        public static GremlinqOption<GroovyFormatting> QueryLogGroovyFormatting = new(GroovyFormatting.WithBindings);
    }
}
