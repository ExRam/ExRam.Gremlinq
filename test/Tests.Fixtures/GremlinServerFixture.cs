﻿using ExRam.Gremlinq.Core;
using ExRam.Gremlinq.Providers.Core;
using ExRam.Gremlinq.Tests.Fixtures;
using static ExRam.Gremlinq.Core.GremlinQuerySource;

namespace ExRam.Gremlinq.Tests.Fixtures
{
    public sealed class GremlinServerFixture : GremlinqFixture
    {
        public GremlinServerFixture() : base(g
            .UseGremlinServer(_ => _
                .AtLocalhost()
                .UseNewtonsoftJson()))
        {
        }
    }
}