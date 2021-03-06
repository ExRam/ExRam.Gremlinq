﻿using System.Collections.Immutable;

namespace ExRam.Gremlinq.Core.Steps
{
    public sealed class MatchStep : MultiTraversalArgumentStep
    {
        public MatchStep(ImmutableArray<Traversal> traversals) : base(traversals)
        {
        }
    }
}
