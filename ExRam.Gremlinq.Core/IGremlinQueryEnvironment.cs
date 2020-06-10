﻿using System;
using Microsoft.Extensions.Logging;

namespace ExRam.Gremlinq.Core
{
    public interface IGremlinQueryEnvironment
    {
        IGremlinQueryEnvironment ConfigureAddStepHandler(Func<IAddStepHandler, IAddStepHandler> handlerTransformation);
        IGremlinQueryEnvironment ConfigureLogger(Func<ILogger, ILogger> loggerTransformation);
        IGremlinQueryEnvironment ConfigureModel(Func<IGraphModel, IGraphModel> modelTransformation);
        IGremlinQueryEnvironment ConfigureOptions(Func<IGremlinqOptions, IGremlinqOptions> optionsTransformation);
        IGremlinQueryEnvironment ConfigureFeatureSet(Func<FeatureSet, FeatureSet> featureSetTransformation);

        IGremlinQueryEnvironment ConfigureSerializer(Func<IGremlinQuerySerializer, IGremlinQuerySerializer> serializerTransformation);
        IGremlinQueryEnvironment ConfigureDeserializer(Func<IGremlinQueryExecutionResultDeserializer, IGremlinQueryExecutionResultDeserializer> deserializerTransformation);
        IGremlinQueryEnvironment ConfigureExecutor(Func<IGremlinQueryExecutor, IGremlinQueryExecutor> executorTransformation);

        ILogger Logger { get; }
        IGraphModel Model { get; }
        FeatureSet FeatureSet { get; }
        IGremlinqOptions Options { get; }
        IAddStepHandler AddStepHandler { get; }
        IGremlinQueryExecutor Executor { get; }
        IGremlinQuerySerializer Serializer { get; }
        IGremlinQueryExecutionResultDeserializer Deserializer { get; }
    }
}
