﻿using System;
using ExRam.Gremlinq.Serialization;

namespace ExRam.Gremlinq
{
    public sealed class SkipStep : Step
    {
        public SkipStep(long count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            Count = count;
        }

        public override void Accept(IGremlinQueryElementVisitor visitor)
        {
            visitor.Visit(this);
        }

        public long Count { get; }
    }
}
