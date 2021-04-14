﻿using Gremlin.Net.Process.Traversal;
using Gremlin.Net.Structure.IO.GraphSON;

using Xunit;
using Xunit.Abstractions;
using static ExRam.Gremlinq.Core.GremlinQuerySource;

namespace ExRam.Gremlinq.Core.Tests
{
    public sealed class Graphson3GremlinQuerySerializationTest : QuerySerializationTest, IClassFixture<Graphson3GremlinQuerySerializationTest.Fixture>
    {
        public new sealed class Fixture : QuerySerializationTest.Fixture
        {
            public Fixture() : base(g.ConfigureEnvironment(_ => _
                .UseSerializer(GremlinQuerySerializer.Default
                    .Select(obj => Writer.WriteObject((Bytecode)obj)))))
            {
            }
        }

        private static readonly GraphSON3Writer Writer = new();

        public Graphson3GremlinQuerySerializationTest(Fixture fixture, ITestOutputHelper testOutputHelper) : base(
            fixture,
            testOutputHelper)
        {
        }
    }
}
