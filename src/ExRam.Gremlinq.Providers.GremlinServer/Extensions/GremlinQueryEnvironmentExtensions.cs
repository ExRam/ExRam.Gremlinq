﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ExRam.Gremlinq.Providers.GremlinServer;
using ExRam.Gremlinq.Providers.WebSocket;
using Gremlin.Net.Process.Traversal;

namespace ExRam.Gremlinq.Core
{
    public static class GremlinQueryEnvironmentExtensions
    {
        private sealed class GremlinServerConfigurator : IGremlinServerConfigurator
        {
            private readonly IWebSocketConfigurator _baseConfigurator;

            public GremlinServerConfigurator(IWebSocketConfigurator baseConfigurator)
            {
                _baseConfigurator = baseConfigurator;
            }

            public IGremlinServerConfigurator At(Uri uri)
            {
                return new GremlinServerConfigurator(_baseConfigurator.At(uri));
            }

            public IGremlinServerConfigurator ConfigureWebSocket(Func<IWebSocketConfigurator, IWebSocketConfigurator> transformation)
            {
                return new GremlinServerConfigurator(
                    transformation(_baseConfigurator));
            }

            public IGremlinQuerySource Transform(IGremlinQuerySource source)
            {
                return _baseConfigurator
                    .Transform(source);
            }
        }

        public static IGremlinQuerySource UseGremlinServer(this IConfigurableGremlinQuerySource source, Func<IGremlinServerConfigurator, IGremlinQuerySourceTransformation> builderAction)
        {
            return source
                .UseWebSocket(configurator => builderAction(new GremlinServerConfigurator(configurator)))
                .ConfigureEnvironment(environment => environment
                    .ConfigureFeatureSet(featureSet => featureSet
                        .ConfigureGraphFeatures(graphFeatures => graphFeatures & ~(GraphFeatures.Transactions | GraphFeatures.ThreadedTransactions | GraphFeatures.ConcurrentAccess))
                        .ConfigureVertexFeatures(vertexFeatures => vertexFeatures & ~(VertexFeatures.Upsert | VertexFeatures.CustomIds))
                        .ConfigureVertexPropertyFeatures(vPropertiesFeatures => vPropertiesFeatures & ~(VertexPropertyFeatures.CustomIds))
                        .ConfigureEdgeFeatures(edgeProperties => edgeProperties & ~(EdgeFeatures.Upsert | EdgeFeatures.CustomIds)))
                    .ConfigureSerializer(s => s
                        .ConfigureFragmentSerializer(fragmentSerializer => fragmentSerializer
                            .Override<IGremlinQueryBase>((query, env, overridden, recurse) =>
                            {
                                if (env.Options.GetValue(GremlinServerGremlinqOptions.WorkaroundTinkerpop2112))
                                    query = query.AsAdmin().ConfigureSteps<IGremlinQueryBase>(steps => ImmutableStack.Create(steps.Reverse().WorkaroundTINKERPOP_2112().ToArray()));

                                return overridden(query, env, recurse);
                            }))));
        }

        //https://issues.apache.org/jira/browse/TINKERPOP-2112.
        internal static IEnumerable<Step> WorkaroundTINKERPOP_2112(this IEnumerable<Step> steps)
        {
            var propertySteps = default(List<PropertyStep>);

            using (var e = steps.GetEnumerator())
            {
                while (true)
                {
                    var hasNext = e.MoveNext();

                    if (hasNext && e.Current is PropertyStep propertyStep)
                    {
                        propertySteps ??= new List<PropertyStep>();

                        propertySteps.Add(propertyStep);
                    }
                    else
                    {
                        if (propertySteps != null && propertySteps.Count > 0)
                        {
                            propertySteps.Sort((x, y) => -(x.Key.RawKey is T).CompareTo(y.Key.RawKey is T));

                            foreach (var replayPropertyStep in propertySteps)
                            {
                                yield return replayPropertyStep;
                            }

                            propertySteps.Clear();
                        }

                        if (hasNext)
                            yield return e.Current!;
                    }

                    if (!hasNext)
                        break;
                }
            }
        }
    }
}
