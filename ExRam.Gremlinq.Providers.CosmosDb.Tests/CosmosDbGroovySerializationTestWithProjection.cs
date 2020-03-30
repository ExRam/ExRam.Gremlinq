﻿using System;
using ExRam.Gremlinq.Core;
using Xunit.Abstractions;

namespace ExRam.Gremlinq.Providers.CosmosDb.Tests
{
    public class CosmosDbGroovySerializationTestWithProjection : CosmosDbGroovySerializationTest
    {
        public CosmosDbGroovySerializationTestWithProjection(ITestOutputHelper testOutputHelper) : base(
            GremlinQuerySource.g
                .ConfigureEnvironment(env => env
                    .UseCosmosDb(builder => builder
                        .At(new Uri("wss://localhost"), "database", "graph")
                        .AuthenticateBy("authKey"))
                    .ConfigureOptions(options => options
                        .SetItem(GremlinqOption.DontAddElementProjectionSteps, false))),
            testOutputHelper)
        {

        }
    }
}