﻿using ExRam.Gremlinq.Core.Serialization;

namespace ExRam.Gremlinq.Core
{
    public sealed class PropertyStep : Step
    {
        public PropertyStep(string key, object value)
        {
            Key = key;
            Value = value;
        }

        public override void Accept(IGremlinQueryElementVisitor visitor)
        {
            visitor.Visit(this);
        }

        public string Key { get; }
        public object Value { get; }
    }
}
