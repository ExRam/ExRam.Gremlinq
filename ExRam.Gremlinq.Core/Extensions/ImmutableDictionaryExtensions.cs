﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Linq;

namespace ExRam.Gremlinq.Core
{
    public static class ImmutableDictionaryExtensions
    {
        internal static IImmutableDictionary<MemberInfo, PropertyMetadata> ConfigureNames(this IImmutableDictionary<MemberInfo, PropertyMetadata> metadata, Func<MemberInfo, string, string> transformation)
        {
            return metadata
                .SetItems(metadata
                    .Select(kvp => new KeyValuePair<MemberInfo, PropertyMetadata>(
                        kvp.Key,
                        new PropertyMetadata(
                            transformation(kvp.Key, kvp.Value.Name),
                            kvp.Value.SerializationBehaviour))));
        }
    }
}
