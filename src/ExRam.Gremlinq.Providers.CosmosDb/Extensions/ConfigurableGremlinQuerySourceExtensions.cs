﻿using System.Diagnostics.CodeAnalysis;
using ExRam.Gremlinq.Core.Serialization;
using ExRam.Gremlinq.Core.Steps;
using ExRam.Gremlinq.Core.Transformation;
using ExRam.Gremlinq.Providers.CosmosDb;
using ExRam.Gremlinq.Providers.WebSocket;
using Gremlin.Net.Process.Traversal;
using static ExRam.Gremlinq.Core.Transformation.ConverterFactory;

namespace ExRam.Gremlinq.Core
{
    public static class ConfigurableGremlinQuerySourceExtensions
    {
        private sealed class CosmosDbConfigurator : ICosmosDbConfigurator
        {
            private readonly string? _authKey;
            private readonly string? _graphName;
            private readonly string? _databaseName;
            private readonly IWebSocketConfigurator _webSocketConfigurator;

            public CosmosDbConfigurator(IWebSocketConfigurator webSocketConfigurator, string? databaseName, string? graphName, string? authKey)
            {
                _authKey = authKey;
                _graphName = graphName;
                _databaseName = databaseName;
                _webSocketConfigurator = webSocketConfigurator;
            }

            public ICosmosDbConfigurator OnDatabase(string databaseName) => new CosmosDbConfigurator(
                _webSocketConfigurator,
                databaseName,
                _graphName,
                _authKey);

            public ICosmosDbConfigurator OnGraph(string graphName) => new CosmosDbConfigurator(
                _webSocketConfigurator,
                _databaseName,
                graphName,
                _authKey);

            public ICosmosDbConfigurator AuthenticateBy(string authKey) => new CosmosDbConfigurator(
                _webSocketConfigurator,
                _databaseName,
                _graphName,
                authKey);

            public ICosmosDbConfigurator ConfigureWebSocket(Func<IWebSocketConfigurator, IWebSocketConfigurator> transformation) => new CosmosDbConfigurator(
                transformation(_webSocketConfigurator),
                _databaseName,
                _graphName,
                _authKey);

            public IGremlinQuerySource Transform(IGremlinQuerySource source)
            {
                var webSocketConfigurator = _webSocketConfigurator
                    .ConfigureMessageSerializer(_ => JsonNetMessageSerializer.GraphSON2);

                if (_databaseName is { } databaseName && _graphName is { } graphName && _authKey is { } authKey)
                    webSocketConfigurator = webSocketConfigurator.AuthenticateBy($"/dbs/{databaseName}/colls/{graphName}", authKey);

                return webSocketConfigurator
                    .Transform(source);
            }
        }

        private class WorkaroundOrder : EnumWrapper, IComparator
        {
            public static readonly WorkaroundOrder Incr = new("incr");
            public static readonly WorkaroundOrder Decr = new("decr");

            private WorkaroundOrder(string enumValue)  : base("Order", enumValue)
            {
            }
        }

        private sealed class CosmosDbLimitationFilterConverterFactory<TStaticSource> : IConverterFactory
        {
            public sealed class CosmosDbLimitationFilterConverter<TSource, TTarget> : IConverter<TSource, TTarget>
            {
                private readonly Action<TStaticSource> _filter;

                public CosmosDbLimitationFilterConverter(Action<TStaticSource> filter)
                {
                    _filter = filter;
                }

                public bool TryConvert(TSource source, IGremlinQueryEnvironment environment, ITransformer recurse, [NotNullWhen(true)] out TTarget? value)
                {
                    if (source is TStaticSource staticSource)
                        _filter(staticSource);

                    value = default;
                    return false;
                }
            }

            private readonly Action<TStaticSource> _filter;

            public CosmosDbLimitationFilterConverterFactory(Action<TStaticSource> filter)
            {
                _filter = filter;
            }

            public IConverter<TSource, TTarget>? TryCreate<TSource, TTarget>() => typeof(TStaticSource).IsAssignableFrom(typeof(TSource)) || typeof(TSource).IsAssignableFrom(typeof(TStaticSource))
                ? new CosmosDbLimitationFilterConverter<TSource, TTarget>(_filter)
                : null;
        }

        private static readonly NotStep NoneWorkaround = new NotStep(IdentityStep.Instance);

        public static IGremlinQuerySource UseCosmosDb(this IConfigurableGremlinQuerySource source, Func<ICosmosDbConfigurator, IGremlinQuerySourceTransformation> transformation)
        {
            return source
                .UseWebSocket(builder => transformation(new CosmosDbConfigurator(builder, null, null, null)))
                .ConfigureEnvironment(environment => environment
                    .ConfigureFeatureSet(featureSet => featureSet
                        .ConfigureGraphFeatures(_ => GraphFeatures.Transactions | GraphFeatures.Persistence | GraphFeatures.ConcurrentAccess)
                        .ConfigureVariableFeatures(_ => VariableFeatures.BooleanValues | VariableFeatures.IntegerValues | VariableFeatures.ByteValues | VariableFeatures.DoubleValues | VariableFeatures.FloatValues | VariableFeatures.IntegerValues | VariableFeatures.LongValues | VariableFeatures.StringValues)
                        .ConfigureVertexFeatures(_ => VertexFeatures.RemoveVertices | VertexFeatures.MetaProperties | VertexFeatures.AddVertices | VertexFeatures.MultiProperties | VertexFeatures.StringIds | VertexFeatures.UserSuppliedIds | VertexFeatures.AddProperty | VertexFeatures.RemoveProperty)
                        .ConfigureVertexPropertyFeatures(_ => VertexPropertyFeatures.StringIds | VertexPropertyFeatures.UserSuppliedIds | VertexPropertyFeatures.RemoveProperty | VertexPropertyFeatures.BooleanValues | VertexPropertyFeatures.ByteValues | VertexPropertyFeatures.DoubleValues | VertexPropertyFeatures.FloatValues | VertexPropertyFeatures.IntegerValues | VertexPropertyFeatures.LongValues | VertexPropertyFeatures.StringValues)
                        .ConfigureEdgeFeatures(_ => EdgeFeatures.AddEdges | EdgeFeatures.RemoveEdges | EdgeFeatures.StringIds | EdgeFeatures.UserSuppliedIds | EdgeFeatures.AddProperty | EdgeFeatures.RemoveProperty)
                        .ConfigureEdgePropertyFeatures(_ => EdgePropertyFeatures.Properties | EdgePropertyFeatures.BooleanValues | EdgePropertyFeatures.ByteValues | EdgePropertyFeatures.DoubleValues | EdgePropertyFeatures.FloatValues | EdgePropertyFeatures.IntegerValues | EdgePropertyFeatures.LongValues | EdgePropertyFeatures.StringValues))
                    .ConfigureOptions(options => options
                        .SetValue(GremlinqOption.VertexProjectionSteps, Traversal.Empty)
                        .SetValue(GremlinqOption.EdgeProjectionSteps, Traversal.Empty)
                        .SetValue(GremlinqOption.VertexPropertyProjectionSteps, Traversal.Empty))
                    .StoreByteArraysAsBase64String()
                    .ConfigureSerializer(serializer => serializer
                        .Add(ConverterFactory
                            .Create<CosmosDbKey, object>((key, env, recurse) => key.PartitionKey != null
                                ? new[] { key.PartitionKey, key.Id }
                                : key.Id)
                            .AutoRecurse<object>())
                        .Add(ConverterFactory
                            .Create<FilterStep.ByTraversalStep, WhereTraversalStep>(static (step, env, recurse) => new WhereTraversalStep(
                                step.Traversal.Count > 0 && step.Traversal[0] is AsStep
                                    ? new MapStep(step.Traversal)
                                    : step.Traversal))
                            .AutoRecurse<WhereTraversalStep>())
                        .Add(ConverterFactory
                            .Create<HasKeyStep, WhereTraversalStep>((step, env, recurse) => step.Argument is P p && (!p.OperatorName.Equals("eq", StringComparison.OrdinalIgnoreCase))
                                ? new WhereTraversalStep(Traversal.Empty.Push(
                                    KeyStep.Instance,
                                    new IsStep(p)))
                                : default)
                            .AutoRecurse<WhereTraversalStep>())
                        .Add(ConverterFactory
                            .Create<NoneStep, NotStep>((step, env, recurse) => NoneWorkaround)
                            .AutoRecurse<NotStep>())
                        .Add(ConverterFactory
                            .Create<SkipStep, RangeStep>((step, env, recurse) => new RangeStep(step.Count, -1, step.Scope))
                            .AutoRecurse<RangeStep>())
                        .Add(new CosmosDbLimitationFilterConverterFactory<LimitStep>(step =>
                        {
                            if (step.Count > int.MaxValue)
                                throw new ArgumentOutOfRangeException(nameof(step), "CosmosDb doesn't currently support values for 'Limit' outside the range of a 32-bit-integer.");
                        }))
                        .Add(new CosmosDbLimitationFilterConverterFactory<TailStep>(step =>
                        {
                            if (step.Count > int.MaxValue)
                                throw new ArgumentOutOfRangeException(nameof(step), "CosmosDb doesn't currently support values for 'Tail' outside the range of a 32-bit-integer.");
                        }))
                        .Add(new CosmosDbLimitationFilterConverterFactory<RangeStep>(step =>
                        {
                            if (step.Lower > int.MaxValue || step.Upper > int.MaxValue)
                                throw new ArgumentOutOfRangeException(nameof(step), "CosmosDb doesn't currently support values for 'Range' outside the range of a 32-bit-integer.");
                        }))
                        .Add(ConverterFactory
                            .Create<Order, WorkaroundOrder>((order, env, recurse) => order.Equals(Order.Asc)
                                ? WorkaroundOrder.Incr
                                : order.Equals(Order.Desc)
                                    ? WorkaroundOrder.Decr
                                    : default)
                            .AutoRecurse<WorkaroundOrder>())
                        .ToGroovy())
                    .StoreTimeSpansAsNumbers());
        }
    }
}
