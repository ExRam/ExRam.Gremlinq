﻿using Newtonsoft.Json.Linq;
using ExRam.Gremlinq.Core.GraphElements;
using System.Diagnostics.CodeAnalysis;
using ExRam.Gremlinq.Core.Transformation;
using ExRam.Gremlinq.Core;

namespace ExRam.Gremlinq.Support.NewtonsoftJson
{
    internal sealed class VertexOrEdgeConverterFactory : IConverterFactory
    {
        private sealed class VertexOrEdgeConverter<TTarget> : IConverter<JObject, TTarget>
        {
            public bool TryConvert(JObject jObject, IGremlinQueryEnvironment environment, ITransformer recurse, [NotNullWhen(true)] out TTarget? value)
            {
                if (jObject.TryGetValue("id", StringComparison.OrdinalIgnoreCase, out var idToken) && jObject.TryGetValue("label", StringComparison.OrdinalIgnoreCase, out var labelToken) && labelToken.Type == JTokenType.String && jObject.TryGetValue("properties", out var propertiesToken) && propertiesToken is JObject propertiesObject)
                    if (recurse.TryTransform(propertiesObject, environment, out value))
                    {
                        value.SetIdAndLabel(idToken, labelToken, environment, recurse);
                        return true;
                    }

                value = default;
                return false;
            }
        }

        public IConverter<TSource, TTarget>? TryCreate<TSource, TTarget>()
        {
            return typeof(TSource) == typeof(JObject) && !typeof(TTarget).IsAssignableFrom(typeof(TSource)) && !typeof(Property).IsAssignableFrom(typeof(TTarget))
                ? (IConverter<TSource, TTarget>)(object)new VertexOrEdgeConverter<TTarget>()
                : default;
        }
    }
}
