﻿using System;
using ExRam.Gremlinq.Providers.WebSocket;

namespace ExRam.Gremlinq.Providers.JanusGraph
{
    public interface IJanusGraphConfigurator : IProviderConfigurator<IJanusGraphConfigurator>
    {
        IJanusGraphConfigurator At(Uri uri);
    }
}
