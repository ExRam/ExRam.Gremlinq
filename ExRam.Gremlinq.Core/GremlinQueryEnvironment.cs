﻿using System;
using System.Collections.Generic;
using System.Linq;
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
                IGremlinQuerySerializer serializer,
                IGremlinQueryExecutor executor,
                IGremlinQueryExecutionResultDeserializer deserializer,
                FeatureSet featureSet,
                GremlinqOptions options,
                ILogger logger)
            {
                Model = model;
                Logger = logger;
                Options = options;
                Executor = executor;
                FeatureSet = featureSet;
                Serializer = serializer;
                Deserializer = deserializer;
            }

            public IGremlinQueryEnvironment ConfigureModel(Func<IGraphModel, IGraphModel> modelTransformation) => new GremlinQueryEnvironmentImpl(modelTransformation(Model), Serializer, Executor, Deserializer, FeatureSet, Options, Logger);

            public IGremlinQueryEnvironment ConfigureOptions(Func<GremlinqOptions, GremlinqOptions> optionsTransformation) => new GremlinQueryEnvironmentImpl(Model, Serializer, Executor, Deserializer, FeatureSet, optionsTransformation(Options), Logger);

            public IGremlinQueryEnvironment ConfigureFeatureSet(Func<FeatureSet, FeatureSet> featureSetTransformation) => new GremlinQueryEnvironmentImpl(Model, Serializer, Executor, Deserializer, featureSetTransformation(FeatureSet), Options, Logger);

            public IGremlinQueryEnvironment ConfigureLogger(Func<ILogger, ILogger> loggerTransformation) => new GremlinQueryEnvironmentImpl(Model, Serializer, Executor, Deserializer, FeatureSet, Options, loggerTransformation(Logger));

            public IGremlinQueryEnvironment ConfigureDeserializer(Func<IGremlinQueryExecutionResultDeserializer, IGremlinQueryExecutionResultDeserializer> configurator) => new GremlinQueryEnvironmentImpl(Model, Serializer, Executor, configurator(Deserializer), FeatureSet, Options, Logger);

            public IGremlinQueryEnvironment ConfigureSerializer(Func<IGremlinQuerySerializer, IGremlinQuerySerializer> configurator) => new GremlinQueryEnvironmentImpl(Model, configurator(Serializer), Executor, Deserializer, FeatureSet, Options, Logger);

            public IGremlinQueryEnvironment ConfigureExecutor(Func<IGremlinQueryExecutor, IGremlinQueryExecutor> configurator) => new GremlinQueryEnvironmentImpl(Model, Serializer, configurator(Executor), Deserializer, FeatureSet, Options, Logger);

            public ILogger Logger { get; }
            public IGraphModel Model { get; }
            public FeatureSet FeatureSet { get; }
            public IGremlinQueryExecutor Executor { get; }
            public IGremlinQuerySerializer Serializer { get; }
            public IGremlinQueryExecutionResultDeserializer Deserializer { get; }
            public GremlinqOptions Options { get; }
        }

        public static readonly IGremlinQueryEnvironment Empty = new GremlinQueryEnvironmentImpl(
            GraphModel.Empty,
            GremlinQuerySerializer.Identity,
            GremlinQueryExecutor.Empty,
            GremlinQueryExecutionResultDeserializer.Identity,
            FeatureSet.Full,
            default,
            NullLogger.Instance);

        public static readonly IGremlinQueryEnvironment Default = Empty
            .UseModel(GraphModel.Default(lookup => lookup
                .IncludeAssembliesFromAppDomain()))
            .UseSerializer(GremlinQuerySerializer.Default)
            .UseExecutor(GremlinQueryExecutor.Invalid);

        internal static readonly Step NoneWorkaround = new NotStep(IdentityStep.Instance);

        public static IGremlinQueryEnvironment UseModel(this IGremlinQueryEnvironment source, IGraphModel model) => source.ConfigureModel(_ => model);

        public static IGremlinQueryEnvironment UseLogger(this IGremlinQueryEnvironment source, ILogger logger) => source.ConfigureLogger(_ => logger);

        public static IGremlinQueryEnvironment UseSerializer(this IGremlinQueryEnvironment environment, IGremlinQuerySerializer serializer) => environment.ConfigureSerializer(_ => serializer);

        public static IGremlinQueryEnvironment UseDeserializer(this IGremlinQueryEnvironment environment, IGremlinQueryExecutionResultDeserializer deserializer) => environment.ConfigureDeserializer(_ => deserializer);

        public static IGremlinQueryEnvironment UseExecutor(this IGremlinQueryEnvironment environment, IGremlinQueryExecutor executor) => environment.ConfigureExecutor(_ => executor);

        public static IAsyncEnumerable<TElement> Execute<TElement>(this IGremlinQueryEnvironment environment, IGremlinQueryBase<TElement> query)
        {
            var serialized = environment.Serializer
                .Serialize(query);

            return environment.Executor
                .Execute(serialized)
                .SelectMany(executionResult => environment.Deserializer
                    .Deserialize<TElement>(executionResult, query.AsAdmin().Environment));
        }

        public static IGremlinQueryEnvironment EchoGraphsonString(this IGremlinQueryEnvironment environment)
        {
            return environment
                .UseSerializer(GremlinQuerySerializer.Default)
                .UseExecutor(GremlinQueryExecutor.Identity)
                .UseDeserializer(GremlinQueryExecutionResultDeserializer.ToGraphsonString);
        }

        public static IGremlinQueryEnvironment EchoGroovyString(this IGremlinQueryEnvironment environment)
        {
            return environment
                .ConfigureSerializer(serializer => serializer.ToGroovy())
                .UseExecutor(GremlinQueryExecutor.Identity)
                .UseDeserializer(GremlinQueryExecutionResultDeserializer.ToString);
        }

        public static IGremlinQueryEnvironment StoreTimeSpansAsNumbers(this IGremlinQueryEnvironment environment)
        {
            return environment
                .ConfigureSerializer(serializer => serializer
                    .ConfigureFragmentSerializer(fragmentSerializer =>  fragmentSerializer
                        .Override<TimeSpan>((t, overridden, recurse) =>
                        {
                            return recurse.Serialize(t.TotalMilliseconds);
                        })))
                .ConfigureDeserializer(deserializer => deserializer
                    .ConfigureFragmentDeserializer(fragmentDeserializer => fragmentDeserializer
                        .Override<JToken>((jToken, type, env, overridden, recurse) =>
                        {
                            return type == typeof(TimeSpan) && recurse.TryDeserialize(jToken, typeof(double), env) is double value
                                ? TimeSpan.FromMilliseconds(value)
                                : overridden(jToken);
                        })));
        }
    }
}
