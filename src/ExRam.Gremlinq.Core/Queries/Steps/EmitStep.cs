﻿namespace ExRam.Gremlinq.Core
{
    public sealed class EmitStep : Step
    {
        public static readonly EmitStep Instance = new();

        public EmitStep(QuerySemantics? semantics = default) : base(semantics)
        {
        }
    }
}
