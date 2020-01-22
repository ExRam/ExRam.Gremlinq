﻿using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using static ExRam.Gremlinq.Core.GremlinQuerySource;

namespace ExRam.Gremlinq.Core.Tests
{
    public class OutOfTheBoxTest
    {
        private class SomeEntity
        {

        }

        [Fact]
        public async Task Execution()
        {
            (await g
                    .V()
                    .ToArrayAsync())
                .Should()
                .BeEmpty();
        }

        [Fact]
        public async Task V_SomeEntity()
        {
            g
                .ConfigureEnvironment(e => e
                    .ConfigureExecutionPipeline(p => p.ConfigureSerializer(s => s.ToGroovy())))
                .V<SomeEntity>()
                .Should()
                .SerializeToGroovy("V().hasLabel(_a).project(_b, _c, _d, _e).by(id).by(label).by(__.constant(_f)).by(__.properties().group().by(__.label()).by(__.project(_b, _c, _g, _e).by(id).by(__.label()).by(__.value()).by(__.valueMap()).fold()))")
                .WithParameters("SomeEntity", "id", "label", "type", "properties", "vertex", "value");
        }
    }
}
