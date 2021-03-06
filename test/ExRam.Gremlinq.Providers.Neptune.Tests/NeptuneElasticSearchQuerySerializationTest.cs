﻿using System;
using ExRam.Gremlinq.Core;
using ExRam.Gremlinq.Core.Tests;
using ExRam.Gremlinq.Providers.GremlinServer;
using Xunit;
using Xunit.Abstractions;
using static ExRam.Gremlinq.Core.GremlinQuerySource;

namespace ExRam.Gremlinq.Providers.Neptune.Tests
{
    public sealed class NeptuneElasticSearchQuerySerializationTest : QuerySerializationTest, IClassFixture<NeptuneElasticSearchQuerySerializationTest.Fixture>
    {
        public new sealed class Fixture : QuerySerializationTest.Fixture
        {
            public Fixture() : base(g
                .UseNeptune(builder => builder
                    .AtLocalhost()
                    .UseElasticSearch(new Uri("http://elastic.search.server"))))
            {
            }
        }

        public NeptuneElasticSearchQuerySerializationTest(Fixture fixture, ITestOutputHelper testOutputHelper) : base(
            fixture,
            testOutputHelper)
        {
        }
    }
}
