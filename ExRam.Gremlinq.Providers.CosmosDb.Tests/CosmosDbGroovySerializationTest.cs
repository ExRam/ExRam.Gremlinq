﻿using System;
using ExRam.Gremlinq.Core;
using Xunit.Abstractions;

namespace ExRam.Gremlinq.Providers.CosmosDb.Tests
{
    public class CosmosDbGroovySerializationTest : CosmosDbGroovySerializationTestBase
    {
        public CosmosDbGroovySerializationTest(ITestOutputHelper testOutputHelper) : base(
            GremlinQuerySource.g
                .ConfigureEnvironment(env => env
                    .UseCosmosDb(builder => builder
                        .At(new Uri("wss://localhost"), "database", "graph")
                        .AuthenticateBy("authKey"))),
            testOutputHelper)
        {

        }
    }
}
