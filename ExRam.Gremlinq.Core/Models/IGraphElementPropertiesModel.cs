﻿using System.Collections.Immutable;
using System.Reflection;

namespace ExRam.Gremlinq.Core
{
    public interface IGraphElementPropertyModel
    {
        IImmutableDictionary<MemberInfo, MemberMetadata> MetaData { get; }
    }
}
