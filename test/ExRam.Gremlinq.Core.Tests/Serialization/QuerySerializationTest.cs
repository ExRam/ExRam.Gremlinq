﻿using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using ExRam.Gremlinq.Core.Execution;
using ExRam.Gremlinq.Core.Steps;
using ExRam.Gremlinq.Tests.Entities;

namespace ExRam.Gremlinq.Core.Tests
{
    public abstract class QuerySerializationTest : QueryExecutionTest
    {
        public abstract class Fixture : GremlinqTestFixture
        {
            protected Fixture(IGremlinQuerySource source) : base(source
                .ConfigureEnvironment(env => env
                    .UseExecutor(GremlinQueryExecutor.Identity)))
            {
            }
        }

        protected QuerySerializationTest(Fixture fixture, ITestOutputHelper testOutputHelper, [CallerFilePath] string callerFilePath = "") : base(
            fixture,
            testOutputHelper,
            callerFilePath)
        {
        }

        public override IImmutableList<Func<string, string>> Scrubbers() => base
            .Scrubbers()
            .ScrubGuids();

        [Fact]
        public async Task StringKey()
        {
            await _g
                .V<Person>("id")
                .Verify();
        }

        [Fact]
        public async Task Multi_step_serialization()
        {
            await _g
                .ConfigureEnvironment(env => env
                    .ConfigureSerializer(ser => ser
                        .ConfigureFragmentSerializer(f => f
                            .Override<EStep>((step, env, overridden, recurse) => recurse.Serialize(
                                new Step[]
                                {
                                    new VStep(ImmutableArray<object>.Empty),
                                    new OutEStep(ImmutableArray<string>.Empty)
                                },
                                env)))))
                .E()
                .Verify();
        }

        [Fact]
        public async Task Multi_step_serialization_with_forgotten_serialize()
        {
            await _g
                .ConfigureEnvironment(env => env
                    .ConfigureSerializer(ser => ser
                        .ConfigureFragmentSerializer(f => f
                            .Override<EStep>((step, env, overridden, recurse) =>
                                new Step[]
                                {
                                    new VStep(ImmutableArray<object>.Empty),
                                    new OutEStep(ImmutableArray<string>.Empty)
                                }))))
                .E()
                .Verify();
        }

        [Fact]  //TODO: As soon as this is supported by an actual db-system, make this an execution test.
        public async Task Fail_without_message()
        {
            await _g
                .V<object>()
                .Fail()
                .Verify();
        }

        [Fact]  //TODO: As soon as this is supported by an actual db-system, make this an execution test.
        public async Task Fail_with_message()
        {
            await _g
                .V<object>()
                .Fail("There's been an error.")
                .Verify();
        }
    }
}
