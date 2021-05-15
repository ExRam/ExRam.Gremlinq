﻿using Gremlin.Net.Process.Traversal;

namespace ExRam.Gremlinq.Core.Steps
{
    public sealed class WherePredicateStep : Step, IIsOptimizableInWhere
    {
        public sealed class ByMemberStep : Step
        {
            public ByMemberStep(Key? key = default) : base()
            {
                Key = key;
            }

            public Key? Key { get; }
        }

        public WherePredicateStep(P predicate) : base()
        {
            Predicate = predicate;
        }

        public P Predicate { get; }
    }
}
