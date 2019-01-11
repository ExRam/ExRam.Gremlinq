﻿using System;
using ExRam.Gremlinq.Core.Serialization;

namespace ExRam.Gremlinq.Core
{
    public abstract class StepLabel : IGremlinQueryElement
    {
        public void Accept(IGremlinQueryElementVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

#pragma warning disable 660,661
    public class StepLabel<TElement> : StepLabel
#pragma warning restore 660,661
    {
        public static bool operator ==(TElement a, StepLabel<TElement> b)
        {
            throw new NotImplementedException("Only for expressions.");
        }

        public static bool operator !=(TElement a, StepLabel<TElement> b)
        {
            throw new NotImplementedException("Only for expressions.");
        }
    }

#pragma warning disable 660, 661
    public class StepLabel<TQuery, TElement> : StepLabel<TElement> where TQuery : IGremlinQuery
#pragma warning restore 660,661
    {
        public static bool operator ==(TElement a, StepLabel<TQuery, TElement> b)
        {
            throw new NotImplementedException("Only for expressions.");
        }

        public static bool operator !=(TElement a, StepLabel<TQuery, TElement> b)
        {
            throw new NotImplementedException("Only for expressions.");
        }
    }
}
