﻿using Xunit.Abstractions;
using static ExRam.Gremlinq.Core.GremlinQuerySource;

namespace ExRam.Gremlinq.Core.Tests
{
    public sealed class BytecodeQuerySerializationTest : QueryExecutionTest
    {
        public BytecodeQuerySerializationTest(ITestOutputHelper testOutputHelper) : base(
            g.ConfigureEnvironment(_ => _
                .UseSerializer(GremlinQuerySerializer.Default)
                .UseExecutor(GremlinQueryExecutor.Echo)
                .UseDeserializer(GremlinQueryExecutionResultDeserializer.Identity)),
            testOutputHelper)
        {
        }
    }
}
