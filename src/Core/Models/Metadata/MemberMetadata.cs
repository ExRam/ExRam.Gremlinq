﻿namespace ExRam.Gremlinq.Core.Models
{
    public readonly struct MemberMetadata
    {
        private readonly Key? _key;

        public MemberMetadata(Key key, SerializationBehaviour serializationBehaviour = SerializationBehaviour.Default)
        {
            _key = key;
            SerializationBehaviour = serializationBehaviour;
        }

        public Key Key => _key is { } key
            ? key
            : throw new InvalidOperationException($"Cannot retrieve the {nameof(Key)} property of an uninitialized {nameof(MemberMetadata)} struct.");

        public SerializationBehaviour SerializationBehaviour { get; }
    }
}
