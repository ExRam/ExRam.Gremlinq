﻿using ExRam.Gremlinq.Core.Serialization;

namespace ExRam.Gremlinq.Core
{
    public abstract class GremlinEnum<TEnum> : IGremlinQueryAtom where TEnum : GremlinEnum<TEnum>
    {
        protected GremlinEnum(string name)
        {
            Name = name;
        }

        public void Accept(IGremlinQueryElementVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override bool Equals(object obj)
        {
            return obj is TEnum variable && Name == variable.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public string Name { get; }
    }
}
