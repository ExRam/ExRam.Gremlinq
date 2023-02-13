﻿using System.Diagnostics.CodeAnalysis;

namespace ExRam.Gremlinq.Core.Deserialization
{
    public interface IGremlinQueryFragmentDeserializer
    {
        bool TryDeserialize<TSerializedData>(TSerializedData serializedData, Type requestedType, IGremlinQueryEnvironment environment, [NotNullWhen(true)] out object? value);

        IGremlinQueryFragmentDeserializer Override(GremlinQueryFragmentDeserializerDelegate deserializer);
    }
}
