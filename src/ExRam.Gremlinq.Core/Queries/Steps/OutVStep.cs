﻿namespace ExRam.Gremlinq.Core
{
    public sealed class OutVStep : Step
    {
        public static readonly OutVStep Instance = new();

        public OutVStep(QuerySemantics? semantics = default) : base(semantics)
        {
        }
    }
}
