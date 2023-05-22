﻿using ExRam.Gremlinq.Core.Tests.Fixtures;

namespace ExRam.Gremlinq.Core.Tests
{
    public sealed class Graphson3GremlinQuerySerializationTest : QueryExecutionTest, IClassFixture<GraphSon3StringSerializationFixture>
    {
        public Graphson3GremlinQuerySerializationTest(GraphSon3StringSerializationFixture fixture, ITestOutputHelper testOutputHelper) : base(
            fixture,
            GremlinQueryVerifier.Default,
            testOutputHelper)
        {
        }
    }
}
