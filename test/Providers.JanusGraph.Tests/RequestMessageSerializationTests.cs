﻿using ExRam.Gremlinq.Core;
using ExRam.Gremlinq.Core.Tests;
using ExRam.Gremlinq.Core.Tests.Fixtures;
using ExRam.Gremlinq.Providers.Core;
using Gremlin.Net.Driver.Messages;
using static ExRam.Gremlinq.Core.GremlinQuerySource;

namespace ExRam.Gremlinq.Providers.JanusGraph.Tests
{
    public sealed class RequestMessageSerializationTests : QueryExecutionTest, IClassFixture<RequestMessageSerializationTests.RequestMessageFixture>
    {
        public sealed class RequestMessageFixture : SerializationFixture<RequestMessage>
        {
            public RequestMessageFixture() : base(g
                .UseJanusGraph(builder => builder
                    .AtLocalhost()))
            {
            }
        }

        public RequestMessageSerializationTests(RequestMessageFixture fixture, ITestOutputHelper testOutputHelper) : base(
            fixture,
            GremlinQueryVerifier.Default,
            testOutputHelper)
        {

        }
    }
}
