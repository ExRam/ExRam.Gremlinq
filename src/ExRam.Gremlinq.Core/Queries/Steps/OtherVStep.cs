﻿namespace ExRam.Gremlinq.Core
{
    public sealed class OtherVStep : Step
    {
        public static readonly OtherVStep Instance = new();

        public OtherVStep(QuerySemantics? semantics = default) : base(semantics)
        {
        }

        public override Step OverrideQuerySemantics(QuerySemantics semantics) => new OtherVStep(semantics);
    }
}
