﻿using System;
using System.Collections.Generic;

namespace ExRam.Gremlinq.Core
{
    internal static class TypeExtensions
    {
        public static IEnumerable<Type> GetTypeHierarchy(this Type type)
        {
            var currentType = (Type?)type;

            while (currentType != null)
            {
                yield return currentType;
                currentType = currentType.BaseType;
            }
        }

        public static QuerySemantics? GetQuerySemantics(this Type type)
        {
            var semantics = default(QuerySemantics?);
            var containsEdge = type.Name.Contains("Edge");
            var containsValue = type.Name.Contains("Value");
            var containsVertex = type.Name.Contains("Vertex");
            var containsProperty = type.Name.Contains("Property");

            if (containsValue)
                semantics = QuerySemantics.None;
            else if (containsVertex && containsProperty)
                semantics = QuerySemantics.VertexProperty;
            else if (containsVertex && !containsEdge)
                semantics = QuerySemantics.Vertex;
            else if (containsProperty)
                semantics = QuerySemantics.Property;
            else if (containsEdge && !containsVertex)
                semantics = QuerySemantics.Edge;

            return semantics;
        }
    }
}
