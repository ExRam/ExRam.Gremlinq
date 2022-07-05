﻿using ExRam.Gremlinq.Core.Deserialization;
using ExRam.Gremlinq.Core.Execution;
using ExRam.Gremlinq.Core.Models;
using ExRam.Gremlinq.Core.Serialization;
using Microsoft.Extensions.Logging;

namespace ExRam.Gremlinq.Core
{
    public interface IGremlinQueryEnvironment
    {
        IGremlinQueryEnvironment ConfigureLogger(Func<ILogger, ILogger> loggerTransformation);
        IGremlinQueryEnvironment ConfigureModel(Func<IGraphModel, IGraphModel> modelTransformation);
        IGremlinQueryEnvironment ConfigureFeatureSet(Func<IFeatureSet, IFeatureSet> featureSetTransformation);
        IGremlinQueryEnvironment ConfigureOptions(Func<IGremlinqOptions, IGremlinqOptions> optionsTransformation);
        IGremlinQueryEnvironment ConfigureDebugger(Func<IGremlinQueryDebugger, IGremlinQueryDebugger> debuggerTransformation);
        IGremlinQueryEnvironment ConfigureExecutor(Func<IGremlinQueryExecutor, IGremlinQueryExecutor> executorTransformation);
        IGremlinQueryEnvironment ConfigureSerializer(Func<IGremlinQuerySerializer, IGremlinQuerySerializer> serializerTransformation);
        IGremlinQueryEnvironment ConfigureDeserializer(Func<IGremlinQueryExecutionResultDeserializer, IGremlinQueryExecutionResultDeserializer> deserializerTransformation);

        ILogger Logger { get; }
        IGraphModel Model { get; }
        IFeatureSet FeatureSet { get; }
        IGremlinqOptions Options { get; }
        IGremlinQueryDebugger Debugger { get; }
        IGremlinQueryExecutor Executor { get; }
        IGremlinQuerySerializer Serializer { get; }
        IGremlinQueryExecutionResultDeserializer Deserializer { get; }
    }
}
