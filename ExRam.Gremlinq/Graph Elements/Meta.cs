﻿using System.Collections.Generic;

namespace ExRam.Gremlinq
{
    public sealed class Meta<T>
    {
        private readonly Dictionary<string, object> _properties = new Dictionary<string, object>();

        public Meta(T value)
        {
            Value = value;
        }

        private Meta()
        {

        }

        public IDictionary<string, object> Properties => _properties;

        public static implicit operator T(Meta<T> meta)
        {
            return meta.Value;
        }

        public static implicit operator Meta<T>(T value)
        {
            return new Meta<T>(value);
        }

        public T Value { get; set; }
    }
}
