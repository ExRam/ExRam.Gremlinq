﻿using Gremlin.Net.Structure.IO.GraphSON;
using static ExRam.Gremlinq.Core.GremlinQuerySource;
using ExRam.Gremlinq.Core.Serialization;
using ExRam.Gremlinq.Core.Transformation;

namespace ExRam.Gremlinq.Core.Tests
{
    public sealed class Graphson3GremlinQuerySerializationTest : QuerySerializationTest<string>, IClassFixture<Graphson3GremlinQuerySerializationTest.Fixture>
    {
        public sealed class Fixture : GremlinqTestFixture
        {
            private static readonly GraphSON3Writer Writer = new();

            public Fixture() : base(g
                .ConfigureEnvironment(_ => _
                    .ConfigureSerializer(ser => ser
                        .Add(ConverterFactory
                            .Create<BytecodeGremlinQuery, string>((query, env, recurse) => Writer.WriteObject(query.Bytecode))))))
            {
            }
        }

        public Graphson3GremlinQuerySerializationTest(Fixture fixture, ITestOutputHelper testOutputHelper) : base(
            fixture,
            testOutputHelper)
        {
        }
    }
}
