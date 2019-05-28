﻿using System.Collections.Immutable;
using System.Reflection;

namespace ExRam.Gremlinq.Core
{
    public interface IGraphElementPropertyModel
    {
        IImmutableDictionary<MemberInfo, PropertyMetadata> Metadata { get; }

        IImmutableDictionary<string, T> SpecialNames { get; }
    }
}
