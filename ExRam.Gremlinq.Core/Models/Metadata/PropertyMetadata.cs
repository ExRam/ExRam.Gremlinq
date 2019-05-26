﻿using LanguageExt;

namespace ExRam.Gremlinq.Core
{
    public struct PropertyMetadata
    {
        public static readonly PropertyMetadata Default = new PropertyMetadata(default, default);
        public static readonly PropertyMetadata ReadOnly = new PropertyMetadata(default, SerializationBehaviour.IgnoreOnUpdate);

        public PropertyMetadata(Option<string> nameOverride, SerializationBehaviour serializationBehaviour)
        {
            SerializationBehaviour = serializationBehaviour;
            NameOverride = nameOverride;
        }

        public Option<string> NameOverride { get; }
        public SerializationBehaviour SerializationBehaviour { get; }
    }
}
