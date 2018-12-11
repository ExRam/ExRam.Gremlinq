﻿namespace ExRam.Gremlinq
{
    public sealed class TimesStep : Step
    {
        public TimesStep(int count)
        {
            Count = count;
        }

        public override void Accept(IQueryElementVisitor visitor)
        {
            visitor.Visit(this);
        }

        public int Count { get; }
    }
}
