﻿using System.Runtime.CompilerServices;
using ExRam.Gremlinq.Core;
using ExRam.Gremlinq.Core.Tests;
using ExRam.Gremlinq.Providers.GremlinServer.Tests.Fixtures;
using ExRam.Gremlinq.Support.NewtonsoftJson.Tests;

namespace ExRam.Gremlinq.Providers.GremlinServer.Tests
{
    public sealed class ObjectDeserializationTests : QueryExecutionTest, IClassFixture<SimpleGremlinServerFixture>
    {
        public new sealed class Verifier : DeserializingGremlinqVerifier
        {
            public Verifier(ITestOutputHelper testOutputHelper, [CallerFilePath] string sourceFile = "") : base(testOutputHelper, sourceFile)
            {
            }

            public override Task Verify<TElement>(IGremlinQueryBase<TElement> query) => base.Verify(query.Cast<object>());
        }

        public ObjectDeserializationTests(SimpleGremlinServerFixture fixture, ITestOutputHelper testOutputHelper) : base(
            fixture,
            new Verifier(testOutputHelper),
            testOutputHelper)
        {
        }
    }
}
