﻿namespace ExRam.Gremlinq.Core.Steps
{
    public sealed class GroupStep : Step
    {
        public abstract class ByStep : Step
        {
            protected ByStep(SideEffectSemanticsChange sideEffectSemanticsChange = SideEffectSemanticsChange.None) : base(sideEffectSemanticsChange)
            {
            }
        }

        public sealed class ByTraversalStep : ByStep
        {
            public ByTraversalStep(Traversal traversal) : base(traversal.GetSideEffectSemanticsChange())
            {
                Traversal = traversal;
            }

            public Traversal Traversal { get; }
        }

        public sealed class ByKeyStep : ByStep
        {
            public ByKeyStep(Key key) : base()
            {
                Key = key;
            }

            public Key Key { get; }
        }

        public static readonly GroupStep Instance = new();

        public GroupStep() : base()
        {
        }
    }
}
