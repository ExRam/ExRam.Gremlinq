﻿namespace ExRam.Gremlinq.Core
{
    public sealed class ExplainStep : Step
    {
        public static readonly ExplainStep Instance = new();

        public ExplainStep(QuerySemantics? semantics = default) : base(semantics)
        {
        }
    }
}
