﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ExRam.Gremlinq.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ExRam.Gremlinq.Providers
{
    public sealed class GraphsonDeserializerFactory : IGremlinQueryExecutionResultDeserializerFactory<JToken>
    {
        private sealed class DefaultGraphsonDeserializer : IGremlinQueryExecutionResultDeserializer<JToken>
        {
            private readonly GraphsonDeserializer _baseDeserializer;

            public DefaultGraphsonDeserializer(GraphsonDeserializer baseDeserializer)
            {
                _baseDeserializer = baseDeserializer;
            }

            public IAsyncEnumerable<TElement> Deserialize<TElement>(JToken result)
            {
                try
                {
                    return _baseDeserializer
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

        private readonly JsonConverter[] _additionalConverters;
        private readonly ConditionalWeakTable<IGremlinQueryEnvironment, DefaultGraphsonDeserializer> _serializers = new ConditionalWeakTable<IGremlinQueryEnvironment, DefaultGraphsonDeserializer>();

        public GraphsonDeserializerFactory(params JsonConverter[] additionalConverters)
        {
            _additionalConverters = additionalConverters;
        }

        public IGremlinQueryExecutionResultDeserializer<JToken> Get(IGremlinQueryEnvironment environment)
        {
            return _serializers.GetValue(
                environment,
                gremlinQueryEnvironment => new DefaultGraphsonDeserializer(new GraphsonDeserializer(gremlinQueryEnvironment, _additionalConverters)));
        }
    }
}
