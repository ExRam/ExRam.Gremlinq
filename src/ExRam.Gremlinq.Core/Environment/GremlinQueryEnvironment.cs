﻿using System;
using ExRam.Gremlinq.Core.Deserialization;
using ExRam.Gremlinq.Core.Execution;
using ExRam.Gremlinq.Core.Models;
using ExRam.Gremlinq.Core.Serialization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json.Linq;

namespace ExRam.Gremlinq.Core
{
    public static class GremlinQueryEnvironment
    {
        private sealed class GremlinQueryEnvironmentImpl : IGremlinQueryEnvironment
        {
            public GremlinQueryEnvironmentImpl(
                IGraphModel model,
                IAddStepHandler addStepHandler,
                IGremlinQuerySerializer serializer,
                IGremlinQueryExecutor executor,
                IGremlinQueryExecutionResultDeserializer deserializer,
                IGremlinQueryDebugger debugger,
                IFeatureSet featureSet,
                IGremlinqOptions options,
                ILogger logger)
            {
                Model = model;
                Logger = logger;
                Options = options;
                Executor = executor;
                Debugger = debugger;
                FeatureSet = featureSet;
                Serializer = serializer;
                Deserializer = deserializer;
                AddStepHandler = addStepHandler;
            }

            public IGremlinQueryEnvironment ConfigureModel(Func<IGraphModel, IGraphModel> modelTransformation) => new GremlinQueryEnvironmentImpl(modelTransformation(Model), AddStepHandler, Serializer, Executor, Deserializer, Debugger, FeatureSet, Options, Logger);

            public IGremlinQueryEnvironment ConfigureOptions(Func<IGremlinqOptions, IGremlinqOptions> optionsTransformation) => new GremlinQueryEnvironmentImpl(Model, AddStepHandler, Serializer, Executor, Deserializer, Debugger, FeatureSet, optionsTransformation(Options), Logger);

            public IGremlinQueryEnvironment ConfigureFeatureSet(Func<IFeatureSet, IFeatureSet> featureSetTransformation) => new GremlinQueryEnvironmentImpl(Model, AddStepHandler, Serializer, Executor, Deserializer, Debugger, featureSetTransformation(FeatureSet), Options, Logger);

            public IGremlinQueryEnvironment ConfigureAddStepHandler(Func<IAddStepHandler, IAddStepHandler> handlerTransformation) => new GremlinQueryEnvironmentImpl(Model, handlerTransformation(AddStepHandler), Serializer, Executor, Deserializer, Debugger, FeatureSet, Options, Logger);

            public IGremlinQueryEnvironment ConfigureLogger(Func<ILogger, ILogger> loggerTransformation) => new GremlinQueryEnvironmentImpl(Model, AddStepHandler, Serializer, Executor, Deserializer, Debugger, FeatureSet, Options, loggerTransformation(Logger));

            public IGremlinQueryEnvironment ConfigureDeserializer(Func<IGremlinQueryExecutionResultDeserializer, IGremlinQueryExecutionResultDeserializer> configurator) => new GremlinQueryEnvironmentImpl(Model, AddStepHandler, Serializer, Executor, configurator(Deserializer), Debugger, FeatureSet, Options, Logger);

            public IGremlinQueryEnvironment ConfigureSerializer(Func<IGremlinQuerySerializer, IGremlinQuerySerializer> configurator) => new GremlinQueryEnvironmentImpl(Model, AddStepHandler, configurator(Serializer), Executor, Deserializer, Debugger, FeatureSet, Options, Logger);

            public IGremlinQueryEnvironment ConfigureExecutor(Func<IGremlinQueryExecutor, IGremlinQueryExecutor> configurator) => new GremlinQueryEnvironmentImpl(Model, AddStepHandler, Serializer, configurator(Executor), Deserializer, Debugger, FeatureSet, Options, Logger);

            public IGremlinQueryEnvironment ConfigureDebugger(Func<IGremlinQueryDebugger, IGremlinQueryDebugger> debuggerTransformation) => new GremlinQueryEnvironmentImpl(Model, AddStepHandler, Serializer, Executor, Deserializer, debuggerTransformation(Debugger), FeatureSet, Options, Logger);

            public ILogger Logger { get; }
            public IGraphModel Model { get; }
            public IFeatureSet FeatureSet { get; }
            public IGremlinqOptions Options { get; }
            public IAddStepHandler AddStepHandler { get; }
            public IGremlinQueryDebugger Debugger { get; }
            public IGremlinQueryExecutor Executor { get; }
            public IGremlinQuerySerializer Serializer { get; }
            public IGremlinQueryExecutionResultDeserializer Deserializer { get; }
        }

        public static readonly IGremlinQueryEnvironment Empty = new GremlinQueryEnvironmentImpl(
            GraphModel.Invalid,
            AddStepHandler.Empty,
            GremlinQuerySerializer.Identity,
            GremlinQueryExecutor.Empty,
            GremlinQueryExecutionResultDeserializer.Identity,
            GremlinQueryDebugger.Groovy(GroovyFormatting.Inline),
            FeatureSet.Full,
            GremlinqOptions.Empty,
            NullLogger.Instance);

        public static readonly IGremlinQueryEnvironment Default = Empty
            .UseAddStepHandler(AddStepHandler.Default)
            .UseSerializer(GremlinQuerySerializer.Default)
            .UseExecutor(GremlinQueryExecutor.Invalid)
            .UseDeserializer(GremlinQueryExecutionResultDeserializer.Default);

        public static IGremlinQueryEnvironment UseAddStepHandler(this IGremlinQueryEnvironment source, IAddStepHandler addStepHandler) => source.ConfigureAddStepHandler(_ => addStepHandler);

        public static IGremlinQueryEnvironment UseModel(this IGremlinQueryEnvironment source, IGraphModel model) => source.ConfigureModel(_ => model);

        public static IGremlinQueryEnvironment UseLogger(this IGremlinQueryEnvironment source, ILogger logger) => source.ConfigureLogger(_ => logger);

        public static IGremlinQueryEnvironment UseSerializer(this IGremlinQueryEnvironment environment, IGremlinQuerySerializer serializer) => environment.ConfigureSerializer(_ => serializer);

        public static IGremlinQueryEnvironment UseDeserializer(this IGremlinQueryEnvironment environment, IGremlinQueryExecutionResultDeserializer deserializer) => environment.ConfigureDeserializer(_ => deserializer);

        public static IGremlinQueryEnvironment UseExecutor(this IGremlinQueryEnvironment environment, IGremlinQueryExecutor executor) => environment.ConfigureExecutor(_ => executor);

        public static IGremlinQueryEnvironment UseDebugger(this IGremlinQueryEnvironment environment, IGremlinQueryDebugger debugger) => environment.ConfigureDebugger(_ => debugger);

        public static IGremlinQueryEnvironment EchoGraphsonString(this IGremlinQueryEnvironment environment)
        {
            return environment
                .UseSerializer(GremlinQuerySerializer.Default)
                .UseExecutor(GremlinQueryExecutor.Identity)
                .ConfigureDeserializer(_ => _
                    .ConfigureFragmentDeserializer(_ => _
                        .ToGraphsonString()));
        }

        public static IGremlinQueryEnvironment EchoGroovyGremlinQuery(this IGremlinQueryEnvironment environment, GroovyFormatting formatting = GroovyFormatting.WithBindings)
        {
            return environment
                .ConfigureSerializer(serializer => serializer.ToGroovy(formatting))
                .UseExecutor(GremlinQueryExecutor.Identity)
                .UseDeserializer(GremlinQueryExecutionResultDeserializer.Default);
        }

        public static IGremlinQueryEnvironment StoreTimeSpansAsNumbers(this IGremlinQueryEnvironment environment)
        {
            return environment
                .ConfigureSerializer(serializer => serializer
                    .ConfigureFragmentSerializer(fragmentSerializer =>  fragmentSerializer
                        .Override<TimeSpan>((t, env, _, recurse) => recurse.Serialize(t.TotalMilliseconds, env))))
                .ConfigureDeserializer(deserializer => deserializer
                    .ConfigureFragmentDeserializer(fragmentDeserializer => fragmentDeserializer
                        .Override<JValue>((jValue, type, env, overridden, recurse) => type == typeof(TimeSpan)
                            ? TimeSpan.FromMilliseconds(jValue.Value<double>())
                            : overridden(jValue, type, env, recurse))));
        }

        public static IGremlinQueryEnvironment StoreByteArraysAsBase64String(this IGremlinQueryEnvironment environment)
        {
            return environment
                .ConfigureModel(static model => model
                    .ConfigureNativeTypes(static types => types
                        .Remove(typeof(byte[]))))
                .ConfigureSerializer(static _ => _
                    .ConfigureFragmentSerializer(static _ => _
                        .Override<byte[]>(static (bytes, env, overridden, recurse) => recurse.Serialize(Convert.ToBase64String(bytes), env))));
        }

        public static IGremlinQueryEnvironment RegisterNativeType<TNative>(this IGremlinQueryEnvironment environment, GremlinQueryFragmentSerializerDelegate<TNative> serializerDelegate, GremlinQueryFragmentDeserializerDelegate<JValue> deserializer)
        {
            return environment
                .RegisterNativeType(
                    serializerDelegate,
                    _ => _
                        .Override<JValue, TNative>(deserializer));
        }

        public static IGremlinQueryEnvironment RegisterNativeType<TNative>(this IGremlinQueryEnvironment environment, GremlinQueryFragmentSerializerDelegate<TNative> serializerDelegate, Func<IGremlinQueryFragmentDeserializer, IGremlinQueryFragmentDeserializer> fragmentDeserializerTransformation)
        {
            return environment
                .ConfigureModel(static _ => _
                    .ConfigureNativeTypes(static _ => _
                        .Add(typeof(TNative))))
                .ConfigureSerializer(_ => _
                    .ConfigureFragmentSerializer(_ => _
                        .Override(serializerDelegate)))
                .ConfigureDeserializer(_ => _
                    .ConfigureFragmentDeserializer(fragmentDeserializerTransformation));
        }
    }
}
