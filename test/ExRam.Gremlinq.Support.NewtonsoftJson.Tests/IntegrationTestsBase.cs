﻿using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using ExRam.Gremlinq.Core;
using ExRam.Gremlinq.Core.Execution;
using ExRam.Gremlinq.Core.Models;
using ExRam.Gremlinq.Core.Tests;
using ExRam.Gremlinq.Core.Transformation;
using ExRam.Gremlinq.Tests.Entities;
using Newtonsoft.Json.Linq;

namespace ExRam.Gremlinq.Support.NewtonsoftJson.Tests
{
    public abstract class IntegrationTestsBase : QueryExecutionTest
    {
        public abstract class Fixture : GremlinqTestFixture
        {
            protected Fixture(IGremlinQuerySource source) : base(source
                .ConfigureEnvironment(env => env
                    .ConfigureExecutor(executor => executor
                        .CatchExecutionExceptions())
                    .UseNewtonsoftJson()
                    .ConfigureDeserializer(d => d
                        .Add(ConverterFactory
                            .Create<JToken, JToken>((token, env, recurse) => token)))))
            {
            }
        }

        private static readonly Regex IdRegex = new ("(\"id\"\\s*[:,]\\s*{\\s*\"@type\"\\s*:\\s*\"g:(Int32|Int64|UUID)\"\\s*,\\s*\"@value\":\\s*)([^\\s{}]+)(\\s*})", RegexOptions.IgnoreCase);

        protected IntegrationTestsBase(Fixture fixture, ITestOutputHelper testOutputHelper, [CallerFilePath] string callerFilePath = "") : base(
            fixture,
            testOutputHelper,
            callerFilePath)
        {
        }

        [Fact]
        public virtual async Task AddV_list_cardinality_id()
        {
            await _g
               .ConfigureEnvironment(env => env
                   .UseModel(GraphModel
                       .FromBaseTypes<VertexWithListId, Edge>(lookup => lookup
                           .IncludeAssembliesOfBaseTypes())))
               .AddV(new VertexWithListId { Id = new[] { "123", "456" } })
               .Verify();
        }

        public override Task Verify<TElement>(IGremlinQueryBase<TElement> query) => base.Verify(query.Cast<JToken>());

        protected override IImmutableList<Func<string, string>> Scrubbers() => base.Scrubbers()
            .Add(x => IdRegex.Replace(x, "$1-1$4"))
            .ScrubGuids();
    }
}
