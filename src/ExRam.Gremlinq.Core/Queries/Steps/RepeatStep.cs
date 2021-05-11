﻿namespace ExRam.Gremlinq.Core
{
    public sealed class RepeatStep : Step
    {
        public RepeatStep(Traversal traversal, QuerySemantics? semantics = default) : base(semantics)
        {
            Traversal = traversal;
        }

        public Traversal Traversal { get; }
    }
}
