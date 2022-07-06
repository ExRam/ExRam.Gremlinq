﻿using System.Collections.Immutable;
using ExRam.Gremlinq.Core.Steps;
using Gremlin.Net.Process.Traversal;

namespace ExRam.Gremlinq.Core
{
    internal static class TraversalExtensions
    {
        public static SideEffectSemanticsChange GetSideEffectSemanticsChange(this ImmutableArray<Traversal> traversals)
        {
            for (var i = 0;  i < traversals.Length; i++)
            {
                if (traversals[i].SideEffectSemantics == SideEffectSemantics.Write)
                    return SideEffectSemanticsChange.Write;
            }

            return SideEffectSemanticsChange.None;
        }

        public static SideEffectSemanticsChange GetSideEffectSemanticsChange(this Traversal traversal)
        {
            return (SideEffectSemanticsChange)traversal.SideEffectSemantics;
        }

        public static SideEffectSemanticsChange GetSideEffectSemanticsChange(this Traversal? maybeTraversal)
        {
            return maybeTraversal is { } traversal
                ? traversal.GetSideEffectSemanticsChange()
                : SideEffectSemanticsChange.None;
        }

        public static Traversal Rewrite(this Traversal traversal, ContinuationFlags flags)
        {
            if (flags.HasFlag(ContinuationFlags.Filter))
                traversal = traversal.RewriteForFilterContext();

            return traversal;
        }

        private static Traversal RewriteForFilterContext(this Traversal traversal, P? maybeExistingPredicate = null)
        {
            if (traversal.Count >= 2)
            {
                if (traversal[^1] is IsStep { Predicate: { } isPredicate })
                {
                    if (maybeExistingPredicate is { } existingPredicate)
                        isPredicate = isPredicate.And(existingPredicate);

                    if (traversal[^2] is IsStep)
                        return traversal.Pop().RewriteForFilterContext(isPredicate);

                    var newStep = traversal[^2] switch
                    {
                        IdStep => new HasPredicateStep(T.Id, isPredicate),
                        LabelStep => new HasPredicateStep(T.Label, isPredicate),
                        ValuesStep { Keys.Length: 1 } valuesStep => isPredicate.GetFilterStep(valuesStep.Keys[0]),
                        _ => default
                    };

                    if (newStep != null)
                    {
                        if (traversal.Count == 2)
                            return newStep;

                        return Traversal.Create(
                            traversal.Count - 1,
                            (traversal, newStep),
                            static (steps, state) =>
                            {
                                var (traversal, newStep) = state;

                                traversal
                                    .AsSpan()[..^2]
                                    .CopyTo(steps);

                                steps[^1] = newStep;
                            });
                    }
                }
            }
            else if (traversal.Count == 1)
            {
                if (traversal[0] is FilterStep.ByTraversalStep filterStep)
                    return filterStep.Traversal.RewriteForFilterContext();
            }

            return traversal;
        }

        public static IEnumerable<Traversal> Fuse(
            this ArraySegment<Traversal> traversals,
            Func<P, P, P> fuse)
        {
            if (traversals.Count > 0)
            {
                var isCount1 = true;
                var isFirstHasPredicateStep = true;
                var isFirstIsStep = true;

                for (var i = 0; i < traversals.Count; i++)
                {
                    if (traversals.Array![i].Count == 1)
                    {
                        if (traversals.Array[i][0] is not HasPredicateStep)
                            isFirstHasPredicateStep = false;

                        if (traversals.Array[i][0] is not IsStep)
                            isFirstIsStep = false;
                    }
                    else
                        isCount1 = false;
                }

                if (isCount1)
                {
                    if (isFirstHasPredicateStep)
                    {
                        var groups = traversals
                            .GroupBy(
                                static x => ((HasPredicateStep)x[0]).Key,
                                static x => ((HasPredicateStep)x[0]).Predicate);

                        foreach (var group in groups)
                        {
                            var effective = group
                                .Aggregate(fuse);

                            yield return new HasPredicateStep(group.Key, effective);
                        }

                        yield break;
                    }

                    if (isFirstIsStep)
                    {
                        var effective = traversals
                            .Select(static x => ((IsStep)x[0]).Predicate)
                            .Aggregate(fuse);

                        yield return new IsStep(effective);
                        yield break;
                    }
                }

                for (var i = 0; i < traversals.Count; i++)
                {
                    yield return traversals.Array![i];
                }
            }
        }

        public static bool IsIdentity(this Traversal traversal) => traversal.Count == 0 || (traversal.Count == 1 && traversal[0] is IdentityStep);

        public static bool IsNone(this Traversal traversal) => traversal.PeekOrDefault() is NoneStep;

        public static Step Peek(this Traversal traversal) => traversal.PeekOrDefault() ?? throw new InvalidOperationException($"{nameof(Traversal)} is Empty.");

        public static Step? PeekOrDefault(this Traversal traversal) => traversal.Count > 0 ? traversal[traversal.Count - 1] : null;
    }
}
