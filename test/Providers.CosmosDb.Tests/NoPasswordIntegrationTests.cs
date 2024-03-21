using ExRam.Gremlinq.Core;
using ExRam.Gremlinq.Tests.Entities;
using ExRam.Gremlinq.Tests.Fixtures;
using ExRam.Gremlinq.Tests.Infrastructure;

using ExRam.Gremlinq.Support.NewtonsoftJson;

using static ExRam.Gremlinq.Providers.CosmosDb.Tests.NoPasswordIntegrationTests;
using FluentAssertions;
using ExRam.Gremlinq.Core.Execution;
using Gremlin.Net.Driver.Exceptions;

namespace ExRam.Gremlinq.Providers.CosmosDb.Tests
{
    [IntegrationTest]
    public sealed class NoPasswordIntegrationTests : GremlinqTestBase, IClassFixture<NoPasswordFixture>
    {
        public sealed class NoPasswordFixture : GremlinqFixture
        {
            private const string CosmosDbEmulatorDatabaseName = "db";
            private const string CosmosDbEmulatorCollectionName = "graph";

            protected override async Task<IGremlinQuerySource> TransformQuerySource(IGremlinQuerySource g) => g
                .UseCosmosDb<Vertex, Edge>(conf => conf
                    .At(new Uri("ws://localhost:8901"), CosmosDbEmulatorDatabaseName, CosmosDbEmulatorCollectionName)
                    .WithPartitionKey(x => x.Label!)
                    .AuthenticateBy("pass")
                    .UseNewtonsoftJson());
        }

        private readonly IGremlinQuerySource _g;

        public NoPasswordIntegrationTests(NoPasswordFixture fixture) : base(new JTokenExecutingVerifier())
        {
            _g = fixture.GremlinQuerySource.Result;
        }

        [Fact]
        public async Task No_password_bubbles_up()
        {
            await _g
                .Inject(42)
                .ToArrayAsync()
                .Awaiting(_ => _)
                .Should()
                .ThrowAsync<GremlinQueryExecutionException>()
                .WithInnerException<GremlinQueryExecutionException, ResponseException>()
                .Where(ex => ex.Message.Contains("Invalid credentials provided"));
        }
    }
}
