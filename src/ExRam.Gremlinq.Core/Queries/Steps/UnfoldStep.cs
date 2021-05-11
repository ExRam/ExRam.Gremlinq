﻿namespace ExRam.Gremlinq.Core
{
    public sealed class UnfoldStep : Step
    {
        public static readonly UnfoldStep Instance = new();

        public UnfoldStep(QuerySemantics? semantics = default) : base(semantics)
        {
        }
    }
}
