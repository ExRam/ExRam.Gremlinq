﻿using ExRam.Gremlinq.Core;
using ExRam.Gremlinq.Core.Tests;
using Xunit;
using static ExRam.Gremlinq.Core.GremlinQuerySource;

namespace ExRam.Gremlinq.Providers.WebSocket.Tests
{
    public class WebSocketGroovySerializationTest : GroovySerializationTest
    {
        public WebSocketGroovySerializationTest() : base(g
            .ConfigureEnvironment(env => env
                .UseWebSocket(builder => builder
                    .UseGraphSONVersion(GraphsonVersion.V2))
                .ConfigureExecutionPipeline(p => p
                    .ConfigureSerializer(s => s
                        .ToGroovy()))))
        {

        }
        
        [Fact]
        public void Skip_remains_skip()
        {
            _g
                .V()
                .Skip(10)
                .Should()
                .SerializeToGroovy("V().skip(_a).project(_b, _c, _d, _e).by(id).by(label).by(__.constant(_f)).by(__.properties().group().by(__.label()).by(__.project(_b, _c, _g, _e).by(id).by(__.label()).by(__.value()).by(__.valueMap()).fold()))")
                .WithParameters(10, "id", "label", "type", "properties", "vertex", "value");
        }
    }
}
