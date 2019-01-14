﻿using System;
using System.Linq.Expressions;

namespace ExRam.Gremlinq.Core
{
    public partial interface IValueGremlinQuery<TElement> : IGremlinQuery<TElement>
    {
        IValueGremlinQuery<TElement> Where(Expression<Func<TElement, bool>> predicate);
    }
}
