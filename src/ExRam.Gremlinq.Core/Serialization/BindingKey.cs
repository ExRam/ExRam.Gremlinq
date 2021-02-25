﻿using System;

namespace ExRam.Gremlinq.Core
{
    internal readonly struct BindingKey
    {
        private readonly string? _stringKey;

        public BindingKey(int key)
        {
            var stringKey = string.Empty;

            do
            {
                stringKey = (char)('a' + key % 26) + stringKey;
                key /= 26;
            } while (key > 0);

            _stringKey = "_" + stringKey;
        }

        public static implicit operator BindingKey(int key)
        {
            return new(key);
        }

        public static implicit operator string(BindingKey key)
        {
            if (key._stringKey is { } stringKey)
                return stringKey;

            throw new ArgumentException();
        }

        public override string ToString()
        {
            if (_stringKey is { } stringKey)
                return stringKey;

            return "(invalid)";
        }
    }
}
