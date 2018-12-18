﻿using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace ExRam.Gremlinq.Providers
{
    public sealed class DefaultGraphsonSerializerFactory : IGraphsonSerializerFactory
    {
        private readonly ConditionalWeakTable<IGraphModel, GraphsonDeserializer> _serializers = new ConditionalWeakTable<IGraphModel, GraphsonDeserializer>();

        public JsonSerializer Get(IGraphModel model)
        {
            return _serializers.GetValue(
                model,
                closureModel => new GraphsonDeserializer(closureModel));
        }
    }
}
