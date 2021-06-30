﻿using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Reflection;
using ExRam.Gremlinq.Core.Projections;

namespace ExRam.Gremlinq.Core
{
    internal abstract class GremlinQueryBase
    {
        private static readonly MethodInfo CreateFuncMethod = typeof(GremlinQueryBase).GetMethod(nameof(CreateFunc), BindingFlags.NonPublic | BindingFlags.Static)!;
        private static readonly ConcurrentDictionary<Type, Func<GremlinQueryBase, Projection?, IGremlinQueryBase>> QueryTypes = new();

        protected GremlinQueryBase(
            StepStack steps,
            Projection projection,
            IGremlinQueryEnvironment environment,
            IImmutableDictionary<StepLabel, Projection> stepLabelProjections,
            QueryFlags flags)
        {
            Steps = steps;
            Flags = flags;
            Projection = projection;
            Environment = environment;
            StepLabelProjections = stepLabelProjections;
        }

        protected TTargetQuery ChangeQueryType<TTargetQuery>(Projection? forcedProjection = null)
        {
            var targetQueryType = typeof(TTargetQuery);

            var constructor = QueryTypes.GetOrAdd(
                targetQueryType,
                closureType =>
                {
                    var elementType = GetMatchingType(closureType, "TElement", "TVertex", "TEdge", "TProperty", "TArray") ?? typeof(object);
                    var outVertexType = GetMatchingType(closureType, "TOutVertex", "TAdjacentVertex") ?? typeof(object);
                    var inVertexType = GetMatchingType(closureType, "TInVertex") ?? typeof(object);
                    var scalarType = GetMatchingType(closureType, "TValue", "TArrayItem");
                    var metaType = GetMatchingType(closureType, "TMeta") ?? typeof(object);
                    var queryType = GetMatchingType(closureType, "TOriginalQuery") ?? typeof(object);

                    return (Func<GremlinQueryBase, Projection?, IGremlinQueryBase>)CreateFuncMethod
                        .MakeGenericMethod(
                            elementType,
                            outVertexType,
                            inVertexType,
                            scalarType ?? (elementType.IsArray
                                ? elementType.GetElementType()!
                                : typeof(object)),
                            metaType,
                            queryType)
                        .Invoke(null, new object?[] { closureType })!;
                });

            return (TTargetQuery)constructor(this, forcedProjection);
        }

        private static Func<GremlinQueryBase, Projection?, IGremlinQueryBase> CreateFunc<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>(Type targetQueryType)
        {
            return (existingQuery, forcedProjection) =>
            {
                var actualProjection = forcedProjection ?? existingQuery.Projection;

                if (targetQueryType.IsInstanceOfType(existingQuery) && (actualProjection == existingQuery.Projection))
                    return (IGremlinQueryBase)existingQuery;

                if (!targetQueryType.IsAssignableFrom(typeof(GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>)))
                    throw new NotSupportedException($"Cannot change the query type to {targetQueryType}.");

                return new GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>(
                    existingQuery.Steps,
                    actualProjection,
                    existingQuery.Environment,
                    existingQuery.StepLabelProjections,
                    existingQuery.Flags);
            };
        }

        private static Type? GetMatchingType(Type interfaceType, params string[] argumentNames)
        {
            if (interfaceType.IsGenericType)
            {
                var genericArguments = interfaceType.GetGenericArguments();
                var genericTypeDefinitionArguments = interfaceType.GetGenericTypeDefinition().GetGenericArguments();

                foreach (var argumentName in argumentNames)
                {
                    for (var i = 0; i < genericTypeDefinitionArguments.Length; i++)
                    {
                        if (genericTypeDefinitionArguments[i].ToString() == argumentName)
                            return genericArguments[i];
                    }
                }
            }

            return default;
        }

        protected internal StepStack Steps { get; }
        protected internal QueryFlags Flags { get; }
        protected internal Projection Projection { get; }
        protected internal IGremlinQueryEnvironment Environment { get; }
        protected internal IImmutableDictionary<StepLabel, Projection> StepLabelProjections { get; }
    }
}
