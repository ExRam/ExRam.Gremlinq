﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace ExRam.Gremlinq.Core
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> source, StepLabel<TSource[]> stepLabel)
        {
            throw new InvalidOperationException($"{nameof(EnumerableExtensions)}.{nameof(Intersect)} is not intended to be executed. It's use is only valid within expressions.");
        }

        internal static bool InternalAny(this IEnumerable enumerable)
        {
            var enumerator = enumerable.GetEnumerator();

            return enumerator.MoveNext();
        }
    }
}
