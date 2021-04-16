﻿using System;
using ExRam.Gremlinq.Providers.WebSocket;

namespace ExRam.Gremlinq.Providers.GremlinServer
{
    public interface IGremlinServerConfigurator : IProviderConfigurator<IGremlinServerConfigurator>
    {
        IGremlinServerConfigurator At(Uri uri);
    }
}
