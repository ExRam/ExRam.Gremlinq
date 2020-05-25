﻿using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;

namespace ExRam.Gremlinq.Core
{
    public static class AddStepHandler
    {
        private sealed class AddStepHandlerImpl : IAddStepHandler
        {
            private readonly IImmutableDictionary<Type, Delegate> _dict;
            private readonly ConcurrentDictionary<(Type staticType, Type actualType), Delegate?> _fastDict = new ConcurrentDictionary<(Type staticType, Type actualType), Delegate?>();

            public AddStepHandlerImpl(IImmutableDictionary<Type, Delegate> dict)
            {
                _dict = dict;
            }

            public IImmutableStack<Step> AddStep<TStep>(IImmutableStack<Step> steps, TStep step) where TStep : Step
            {
                return TryGetAddHandler(typeof(TStep), step.GetType()) is Func<IImmutableStack<Step>, TStep, IImmutableStack<Step>> del
                    ? del(steps, step)
                    : steps.Push(step);
            }

            public IAddStepHandler Override<TStep>(Func<IImmutableStack<Step>, TStep, Func<IImmutableStack<Step>, TStep, IImmutableStack<Step>>, IAddStepHandler, IImmutableStack<Step>> addStepHandler) where TStep : Step
            {
                return new AddStepHandlerImpl(
                    _dict.SetItem(
                        typeof(TStep),
                        InnerLookup(typeof(TStep)) is Func<IImmutableStack<Step>, Step, Func<IImmutableStack<Step>, Step, IImmutableStack<Step>>, IAddStepHandler, IImmutableStack<Step>> existingAddHandler
                            ? new Func<IImmutableStack<Step>, Step, Func<IImmutableStack<Step>, Step, IImmutableStack<Step>>, IAddStepHandler, IImmutableStack<Step>>((steps, step, baseHandler, recurse) => addStepHandler(steps, (TStep)step, (_, __) => existingAddHandler(_, __!, baseHandler, recurse), recurse))
                            : (steps, step, baseHandler, recurse) => addStepHandler(steps, (TStep)step, (_, __) => baseHandler(_, __!), recurse)));
            }

            private Delegate? TryGetAddHandler(Type staticType, Type actualType)
            {
                return _fastDict
                    .GetOrAdd(
                        (staticType, actualType),
                        (typeTuple, @this) =>
                        {
                            var (staticType, actualType) = typeTuple;

                            if (@this.InnerLookup(actualType) is { } del)
                            {
                                //return (IImmutableStack<Step> steps, TStatic step) => del(steps, (TActualType)step, (_, TStatic __) => _.Push(__), @this);

                                var effectiveType = del.GetType().GetGenericArguments()[1];

                                var argument3Parameter1 = Expression.Parameter(typeof(IImmutableStack<Step>));
                                var argument3Parameter2 = Expression.Parameter(staticType);

                                var stepsParameterExpression = Expression.Parameter(typeof(IImmutableStack<Step>));
                                var stepParameterExpression = Expression.Parameter(staticType);
                                var staticTypeFunc = typeof(Func<,,>).MakeGenericType(typeof(IImmutableStack<Step>), staticType, typeof(IImmutableStack<Step>));

                                var pushCall = Expression.Call(
                                    argument3Parameter1,
                                    typeof(IImmutableStack<Step>).GetMethod(nameof(IImmutableStack<Step>.Push)),
                                    argument3Parameter2);

                                var retCall = Expression.Invoke(
                                    Expression.Constant(del),
                                    stepsParameterExpression,
                                    Expression.Convert(
                                        stepParameterExpression,
                                        effectiveType),
                                    Expression.Lambda(
                                        pushCall,
                                        argument3Parameter1,
                                        argument3Parameter2),
                                    Expression.Constant(@this));

                                return Expression
                                    .Lambda(
                                        staticTypeFunc,
                                        retCall,
                                        stepsParameterExpression,
                                        stepParameterExpression)
                                    .Compile();
                            }

                            return null;
                        },
                        this);
            }

            private Delegate? InnerLookup(Type actualType)
            {
                if (_dict.TryGetValue(actualType, out var ret))
                    return ret;

                if (actualType.BaseType is { } baseType)
                {
                    if (InnerLookup(baseType) is { } baseHandler)
                        return baseHandler;
                }

                return null;
            }
        }

        public static IAddStepHandler Empty = new AddStepHandlerImpl(ImmutableDictionary<Type, Delegate>.Empty);

        public static IAddStepHandler Default = Empty
            .Override<HasLabelStep>((steps, step, overridden, recurse) => steps.PeekOrDefault() is HasLabelStep hasLabelStep
                ? steps
                    .Pop()
                    .Push(new HasLabelStep(step.Labels.Intersect(hasLabelStep.Labels).ToImmutableArray()))
                : overridden(steps, step))
            .Override<HasPredicateStep>((steps, step, overridden, recurse) =>
            {
                if (steps.PeekOrDefault() is HasPredicateStep hasStep && hasStep.Key == step.Key)
                {
                    var newPredicate = step.Predicate;

                    if (newPredicate == null)
                        return steps;

                    newPredicate = hasStep.Predicate is { } otherPredicate
                        ? otherPredicate.And(newPredicate)
                        : newPredicate;

                    return steps
                        .Pop()
                        .Push(new HasPredicateStep(hasStep.Key, newPredicate));
                }

                return overridden(steps, step);
            })
            .Override<WithoutStrategiesStep>((steps, step, overridden, recurse) => (steps.PeekOrDefault() is WithoutStrategiesStep withoutStrategies)
                ? steps
                    .Pop()
                    .Push(new WithoutStrategiesStep(withoutStrategies.StrategyTypes.Concat(step.StrategyTypes).Distinct().ToImmutableArray()))
                : overridden(steps, step))
            .Override<SelectStep>((steps, step, overridden, recurse) => steps.PeekOrDefault() is AsStep asStep && step.StepLabels.Length == 1 && ReferenceEquals(asStep.StepLabel, step.StepLabels[0])
                ? steps
                : overridden(steps, step))
            .Override<IsStep>((steps, step, overridden, recurse) => steps.PeekOrDefault() is IsStep isStep
                ? steps
                    .Pop()
                    .Push(new IsStep(isStep.Predicate.And(step.Predicate)))
                : overridden(steps, step))
            .Override<NoneStep>((steps, step, overridden, recurse) => steps.PeekOrDefault() is NoneStep ? steps : overridden(steps, step));
    }
}


