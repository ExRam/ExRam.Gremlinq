﻿namespace ExRam.Gremlinq.Core
{
    public sealed class HasKeyStep : Step, IIsOptimizableInWhere
    {
        public HasKeyStep(object argument) : base()
        {
            Argument = argument;
        }

        public object Argument { get; }
    }
}
