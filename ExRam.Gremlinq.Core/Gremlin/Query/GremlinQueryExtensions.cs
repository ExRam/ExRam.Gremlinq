﻿using System;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using LanguageExt;
using System.Threading;
using System.Threading.Tasks;
using ExRam.Gremlinq.Core.GraphElements;
using Microsoft.Extensions.Logging;

namespace ExRam.Gremlinq.Core
{
    public static class GremlinQueryExtensions
    {
        internal static IGremlinQuery AddStep(this IGremlinQuery query, Step step)
        {
            return query.AsAdmin().InsertStep(query.AsAdmin().Steps.Count, step);
        }

        public static IVertexGremlinQuery<TVertex> BothV<TVertex>(this IEdgeGremlinQuery query)
        {
            return query
                .BothV()
                .OfType<TVertex>();
        }

        public static IValueGremlinQuery<long> Count(this IGremlinQuery query)
        {
            return query.CountGlobal();
        }

        public static IGremlinQuery<TEdge> E<TEdge>(this IGremlinQuery query, params object[] ids)
        {
            return query
                .E(ids)
                .OfType<TEdge>();
        }

        public static Task<TElement> First<TElement>(this IGremlinQuery<TElement> query, CancellationToken ct = default)
        {
            return AsyncEnumerable.First(
                query.Limit(1),
                ct);
        }

        public static async Task<Option<TElement>> FirstOrNone<TElement>(this IGremlinQuery<TElement> query, CancellationToken ct = default)
        {
            var array = await query
                .Limit(1)
                .ToArray(ct)
                .ConfigureAwait(false);

            return array.Length > 0
                ? array[0]
                : Option<TElement>.None;
        }

        public static IVertexGremlinQuery<TVertex> InV<TVertex>(this IEdgeGremlinQuery query)
        {
            return query
                .InV()
                .OfType<TVertex>();
        }

        public static IVertexGremlinQuery<TVertex> OtherV<TVertex>(this IEdgeGremlinQuery query)
        {
            return query
                .OtherV()
                .OfType<TVertex>();
        }

        public static IVertexGremlinQuery<TVertex> OutV<TVertex>(this IEdgeGremlinQuery query)
        {
            return query
                .OutV()
                .OfType<TVertex>();
        }

        public static IVertexPropertyGremlinQuery<VertexProperty<object>, object> Properties<TVertex>(this IVertexGremlinQuery<TVertex> query, params Expression<Func<TVertex, object>>[] projections)
        {
            return query.Properties(projections);
        }

        public static IEdgePropertyGremlinQuery<Property<object>, object> Properties<TEdge>(this IEdgeGremlinQuery<TEdge> query, params Expression<Func<TEdge, object>>[] projections)
        {
            return query.Properties(projections);
        }

        public static IGremlinQuery<Property<object>> Properties<TProperty, TValue>(this IVertexPropertyGremlinQuery<TProperty, TValue> query, params string[] keys)
        {
            return query.Properties<object>(keys);
        }

        public static IVertexGremlinQuery<TVertex> V<TVertex>(this IGremlinQuery query, params object[] ids)
        {
            return query
                .V(ids)
                .OfType<TVertex>();
        }

        public static IValueGremlinQuery<object> Values(this IElementGremlinQuery query, params string[] keys)
        {
            return query.Values<object>(keys);
        }

        public static IValueGremlinQuery<object> Values<TProperty, TValue>(this IVertexPropertyGremlinQuery<TProperty, TValue> query)
        {
            return query.Values<object>();
        }

        public static IValueGremlinQuery<object> Values<TProperty, TValue, TMeta>(this IVertexPropertyGremlinQuery<TProperty, TValue, TMeta> query)
        {
            return query.Values<object>();
        }
    }
}
