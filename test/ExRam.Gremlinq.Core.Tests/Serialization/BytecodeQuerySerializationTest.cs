﻿using Xunit;
using Xunit.Abstractions;
using static ExRam.Gremlinq.Core.GremlinQuerySource;

namespace ExRam.Gremlinq.Core.Tests
{
    public sealed class BytecodeQuerySerializationTest : QuerySerializationTest, IClassFixture<BytecodeQuerySerializationTest.Fixture>
    {
        public new sealed class Fixture : QuerySerializationTest.Fixture
        {
            public Fixture() : base(g.ConfigureEnvironment(_ => _))
            {
            }
        }

        public BytecodeQuerySerializationTest(Fixture fixture, ITestOutputHelper testOutputHelper) : base(
            fixture,
            testOutputHelper)
        {
        }
    }
}
