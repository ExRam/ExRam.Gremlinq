﻿using DotNet.Testcontainers.Containers;
using ExRam.Gremlinq.Core;
using ExRam.Gremlinq.Providers.Core;
using ExRam.Gremlinq.Tests.Entities;

namespace ExRam.Gremlinq.Tests.Fixtures
{
    public sealed class GremlinServerContainerFixture : TestContainerFixture
    {
        public GremlinServerContainerFixture() : base("tinkerpop/gremlin-server:3.7.0")
        {
        }

        protected override async Task<IGremlinQuerySource> TransformQuerySource(IContainer container, IConfigurableGremlinQuerySource g) => g
            .UseGremlinServer<Vertex, Edge>(_ => _
                .At(new UriBuilder("ws", container.Hostname, container.GetMappedPublicPort(8182)).Uri)
                .UseNewtonsoftJson());
    }
}
