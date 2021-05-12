﻿using System;
using Gremlin.Net.Process.Traversal;

namespace ExRam.Gremlinq.Core
{
    public sealed class SkipStep : Step
    {
        public SkipStep(long count, Scope scope, QuerySemantics? semantics = default) : base(semantics)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            Count = count;
            Scope = scope;
        }

        public override Step OverrideQuerySemantics(QuerySemantics semantics) => new SkipStep(Count, Scope, semantics);

        public long Count { get; }
        public Scope Scope { get; }
    }
}
