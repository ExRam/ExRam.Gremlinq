﻿using ExRam.Gremlinq.Tests.Fixtures;
using ExRam.Gremlinq.Tests.Infrastructure;
using ExRam.Gremlinq.Tests.TestCases;

namespace ExRam.Gremlinq.Providers.GremlinServer.Tests
{
    [IntegrationTest]
    public sealed class PasswordSecuredIntegrationTests : QueryExecutionTest, IClassFixture<PasswordSecuredGremlinServerContainerFixture>
    {
        public PasswordSecuredIntegrationTests(PasswordSecuredGremlinServerContainerFixture fixture, ITestOutputHelper testOutputHelper) : base(
            fixture,
            new JTokenExecutingVerifier(),
            testOutputHelper)
        {
        }
    }
}
