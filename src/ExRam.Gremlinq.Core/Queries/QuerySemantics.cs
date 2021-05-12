﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExRam.Gremlinq.Core
{
    public readonly struct QuerySemantics : IEquatable<QuerySemantics>
    {
        private sealed class TypeComparer : IComparer<Type>
        {
            public static readonly TypeComparer Instance = new();

            private TypeComparer()
            {

            }

            public int Compare(Type? x, Type? y)
            {
                return x is null
                    ? y is null
                        ? 0
                        : -1
                    : y is null
                        ? 1
                        : x.IsAssignableFrom(y)
                            ? -1
                            : y.IsAssignableFrom(x) ? 1 : 0;
            }
        }

        private static readonly ConcurrentDictionary<Type, Type> BaseTypes = new();
        private static readonly ConcurrentDictionary<(QuerySemantics, QuerySemantics), QuerySemantics> HighestCommonSemantics = new();

        private readonly Type? _queryType;

        public QuerySemantics(Type semantics)
        {
            _queryType = BaseTypes
                .GetOrAdd(
                    semantics,
                    semantics => semantics.GetTypeInfo().ImplementedInterfaces
                        .Prepend(semantics)
                        .Where(x => x.IsGenericType && x.Name.EndsWith("Base`1"))
                        .OrderByDescending(x => x, TypeComparer.Instance)
                        .FirstOrDefault() ?? semantics);
        }

        public static implicit operator QuerySemantics(Type type) => new(type);

        public static bool operator ==(QuerySemantics left, QuerySemantics right) => left.Equals(right);

        public static bool operator !=(QuerySemantics left, QuerySemantics right) => !(left == right);

        public override bool Equals(object? obj) => obj is QuerySemantics semantics && Equals(semantics);

        public bool Equals(QuerySemantics other) => QueryType == other.QueryType;

        public override int GetHashCode() => QueryType.GetHashCode();

        public override string ToString() => QueryType.ToString();

        internal QuerySemantics MostSpecific(QuerySemantics maybeHigher) => QueryType.IsAssignableFrom(maybeHigher.QueryType) ? maybeHigher : this;

        internal QuerySemantics HighestCommon(QuerySemantics other)
        {
            return HighestCommonSemantics
                .GetOrAdd(
                    (this, other),
                    tuple =>
                    {
                        var (@this, other) = tuple;

                        var impl1 = @this.QueryType
                            .GetTypeInfo().ImplementedInterfaces
                            .Prepend(@this.QueryType);

                        var impl2 = other.QueryType
                            .GetTypeInfo().ImplementedInterfaces
                            .Prepend(other.QueryType);

                        var highest = impl1
                            .Intersect(impl2)
                            .OrderByDescending(x => x, TypeComparer.Instance)
                            .FirstOrDefault();

                        return highest ?? typeof(IGremlinQueryBase);
                    });
        }

        internal QuerySemantics Cast<TResult>()
        {
            if (QueryType.IsGenericType && QueryType.GetGenericArguments() is { } arguments)
            {
                if (arguments[0].IsAssignableFrom(typeof(TResult)))
                {
                    arguments[0] = typeof(TResult);

                    return QueryType
                        .GetGenericTypeDefinition()
                        .MakeGenericType(arguments);
                }
            }

            return this;
        }

        internal bool IsVertex
        {
            get => typeof(IVertexGremlinQueryBase).IsAssignableFrom(QueryType);
        }

        internal bool IsEdge
        {
            get => typeof(IEdgeGremlinQueryBase).IsAssignableFrom(QueryType);
        }

        internal bool IsElement
        {
            get => typeof(IElementGremlinQueryBase).IsAssignableFrom(QueryType);
        }

        internal bool IsProperty
        {
            get => typeof(IPropertyGremlinQueryBase).IsAssignableFrom(QueryType);
        }

        internal bool IsValue
        {
            get => typeof(IValueGremlinQueryBase).IsAssignableFrom(QueryType);
        }

        public Type QueryType
        {
            get => _queryType ?? typeof(IGremlinQueryBase<object>);
        }
    }
}
