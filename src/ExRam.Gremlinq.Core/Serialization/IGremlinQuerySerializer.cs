﻿using System;

namespace ExRam.Gremlinq.Core.Serialization
{
    public interface IGremlinQuerySerializer
    {
        object Serialize(IGremlinQueryBase query);

        IGremlinQuerySerializer ConfigureFragmentSerializer(Func<IGremlinQueryFragmentSerializer, IGremlinQueryFragmentSerializer> transformation);
    }
}
