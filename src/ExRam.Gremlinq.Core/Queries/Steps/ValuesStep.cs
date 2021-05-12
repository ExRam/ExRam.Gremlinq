﻿using System.Collections.Immutable;

namespace ExRam.Gremlinq.Core
{
    public sealed class ValuesStep : Step
    {
        public ValuesStep(ImmutableArray<string> keys, QuerySemantics? semantics = default) : base(semantics)
        {
            Keys = keys;
        }

        public override Step OverrideQuerySemantics(QuerySemantics semantics) => new ValuesStep(Keys, semantics);

        public ImmutableArray<string> Keys { get; }
    }
}
