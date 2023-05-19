﻿using ExRam.Gremlinq.Core;
using ExRam.Gremlinq.Core.Tests;
using ExRam.Gremlinq.Providers.Core;
using Gremlin.Net.Driver.Messages;
using static ExRam.Gremlinq.Core.GremlinQuerySource;

namespace ExRam.Gremlinq.Providers.GremlinServer.Tests
{
    public sealed class RequestMessageWithAliasSerializationTests : SerializationTestsBase, IClassFixture<RequestMessageWithAliasSerializationTests.RequestMessageWithAliasFixture>
    {
        public sealed class RequestMessageWithAliasFixture : SerializationTestsFixture<RequestMessage>
        {
            public RequestMessageWithAliasFixture() : base(g
                .UseGremlinServer(builder => builder
                    .AtLocalhost())
                .ConfigureEnvironment(env => env
                    .ConfigureOptions(options => options
                        .SetValue(GremlinqOption.Alias, "a"))))
            {
            }
        }

        public RequestMessageWithAliasSerializationTests(RequestMessageWithAliasFixture fixture, ITestOutputHelper testOutputHelper) : base(
            fixture,
            testOutputHelper)
        {

        }
    }
}
