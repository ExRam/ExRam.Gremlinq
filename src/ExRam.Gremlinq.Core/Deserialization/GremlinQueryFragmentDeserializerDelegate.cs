﻿using System;

namespace ExRam.Gremlinq.Core.Deserialization
{
    public delegate object? BaseGremlinQueryFragmentDeserializerDelegate<TSerialized>(TSerialized serializedData, Type requestedType, IGremlinQueryEnvironment environment, IGremlinQueryFragmentDeserializer recurse);

    public delegate object? GremlinQueryFragmentDeserializerDelegate<TSerialized>(TSerialized serializedData, Type requestedType, IGremlinQueryEnvironment environment, BaseGremlinQueryFragmentDeserializerDelegate<TSerialized> overridden, IGremlinQueryFragmentDeserializer recurse);
}
