﻿namespace ExRam.Gremlinq.Core
{
    public abstract class Step
    {
        protected Step(QuerySemantics? semantics = default)
        {
            Semantics = semantics;
        }

        public abstract Step OverrideQuerySemantics(QuerySemantics semantics);

        public QuerySemantics? Semantics { get; }
    }
}
