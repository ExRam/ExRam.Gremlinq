﻿namespace ExRam.Gremlinq
{
    public sealed class ToLabelStep : Step
    {
        public ToLabelStep(StepLabel stepLabel)
        {
            StepLabel = stepLabel;
        }

        public override void Accept(IQueryElementVisitor visitor)
        {
            visitor.Visit(this);
        }

        public StepLabel StepLabel { get; }
    }
}