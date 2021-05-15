﻿namespace ExRam.Gremlinq.Core.Steps
{
    public sealed class WithSideEffectStep : Step
    {
        public WithSideEffectStep(StepLabel label, object value) : base()
        {
            Label = label;
            Value = value;
        }

        public object Value { get; }
        public StepLabel Label { get; }
    }
}
