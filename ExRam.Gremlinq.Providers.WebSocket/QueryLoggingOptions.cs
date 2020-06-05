﻿using ExRam.Gremlinq.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ExRam.Gremlinq.Providers.WebSocket
{
    public static class QueryLoggingGremlinqOptions
    {
        public static GremlinqOption<LogLevel> QueryLogLogLevel = new GremlinqOption<LogLevel>(LogLevel.Debug);
        public static GremlinqOption<Formatting> QueryLogFormatting = new GremlinqOption<Formatting>(Formatting.None);
        public static GremlinqOption<QueryLogVerbosity> QueryLogVerbosity = new GremlinqOption<QueryLogVerbosity>(WebSocket.QueryLogVerbosity.QueryOnly);
    }
}
