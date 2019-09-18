﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ExRam.Gremlinq.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ExRam.Gremlinq.Providers
{
    public sealed class DefaultGraphsonDeserializer : IGremlinQueryExecutionResultDeserializer<JToken>
    {
        private readonly ConditionalWeakTable<IGremlinQueryEnvironment, GraphsonDeserializer> _serializers = new ConditionalWeakTable<IGremlinQueryEnvironment, GraphsonDeserializer>();

        private readonly JsonConverter[] _additionalConverters;

        public DefaultGraphsonDeserializer(params JsonConverter[] additionalConverters)
        {
            _additionalConverters = additionalConverters;
        }

        public IAsyncEnumerable<TElement> Deserialize<TElement>(JToken result, IGremlinQueryEnvironment environment)
        {
            var baseDeserializer = _serializers.GetValue(
                environment,
                gremlinQueryEnvironment => new GraphsonDeserializer(gremlinQueryEnvironment, _additionalConverters));

            try
            {
                return baseDeserializer
                    .Deserialize<TElement[]>(new JTokenReader(result)
                        .ToTokenEnumerable()
                        .Apply(JsonTransform
                            .Identity()
                            .GraphElements()
                            .NestedValues())
                        .ToJsonReader())
                    .ToAsyncEnumerable();
            }
            catch (JsonReaderException ex)
            {
                throw new GraphsonMappingException($"Error mapping\r\n\r\n{result}\r\n\r\nto an object of type {typeof(TElement[])}.", ex);
            }
        }
    }
}
