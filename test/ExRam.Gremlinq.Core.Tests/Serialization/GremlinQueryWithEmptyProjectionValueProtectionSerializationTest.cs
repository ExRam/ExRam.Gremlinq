﻿using ExRam.Gremlinq.Core.Serialization;
using Xunit;
using Xunit.Abstractions;
using static ExRam.Gremlinq.Core.GremlinQuerySource;

namespace ExRam.Gremlinq.Core.Tests
{
    public sealed class GremlinQueryWithEmptyProjectionValueProtectionSerializationTest : QuerySerializationTest, IClassFixture<GremlinQueryWithEmptyProjectionValueProtectionSerializationTest.Fixture>
    {
        public new sealed class Fixture : QuerySerializationTest.Fixture
        {
            public Fixture() : base(g
                .ConfigureEnvironment(_ => _
                    .UseSerializer(GremlinQuerySerializer.Default.ToGroovy())
                    .ConfigureOptions(o => o.SetValue(GremlinqOption.EnableEmptyProjectionValueProtection, true))))
            {
            }
        }

        public GremlinQueryWithEmptyProjectionValueProtectionSerializationTest(Fixture fixture, ITestOutputHelper testOutputHelper) : base(
            fixture,
            testOutputHelper)
        {
        }
    }
}
