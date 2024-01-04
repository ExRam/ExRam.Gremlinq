﻿using System.Collections.Immutable;
using Gremlin.Net.Process.Traversal;

namespace ExRam.Gremlinq.Core.Steps
{
    public sealed class ProjectStep : Step
    {
        public abstract class ByStep : Step
        {
            protected ByStep(SideEffectSemanticsChange sideEffectSemanticsChange = SideEffectSemanticsChange.None) : base(sideEffectSemanticsChange)
            {
            }

            public abstract ByTraversalStep ToByTraversalStep();
        }

        public sealed class ByTraversalStep : ByStep
        {
            public ByTraversalStep(Traversal traversal) : base(traversal.GetSideEffectSemanticsChange())
            {
                Traversal = traversal;
            }

            public override ByTraversalStep ToByTraversalStep() => this;

            public Traversal Traversal { get; }
        }

        public sealed class ByKeyStep : ByStep
        {
            public ByKeyStep(Key key)
            {
                Key = key;
            }

            public override ByTraversalStep ToByTraversalStep() => new (Key.RawKey switch
            {
                T t => t.TryToStep() ?? throw ConversionFailed(),
                string key => new ValuesStep([key]),
                _ => throw ConversionFailed(),
            });

            public Key Key { get; }

            private NotSupportedException ConversionFailed() => new($"Failed to convert {nameof(ByKeyStep)}.{nameof(Key)} to a {nameof(ByTraversalStep)}.");

        }

        public ProjectStep(ImmutableArray<string> projections)
        {
            Projections = projections;
        }

        public ImmutableArray<string> Projections { get; }
    }
}
