﻿#pragma warning disable IDE0003
// ReSharper disable ArrangeThisQualifier
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using ExRam.Gremlinq.Core.ExpressionParsing;
using ExRam.Gremlinq.Core.GraphElements;
using Gremlin.Net.Process.Traversal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ExRam.Gremlinq.Core
{
    internal static class GremlinQuery
    {
        public static IGremlinQuerySource Create(IGremlinQueryEnvironment environment)
        {
            return Create(
                StepStack.Empty(typeof(IGremlinQuerySource)),
                environment,
                QueryFlags.SurfaceVisible);
        }

        public static IGremlinQuerySource Create(StepStack steps, IGremlinQueryEnvironment environment, QueryFlags flags)
        {
            return new GremlinQuery<object, object, object, object, object, object>(
                steps,
                environment,
                ImmutableDictionary<StepLabel, QuerySemantics>.Empty,
                flags);
        }
    }

    internal sealed partial class GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> : GremlinQueryBase
    {
        private sealed class OrderBuilder : IOrderBuilderWithBy<TElement, GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>>
        {
            private readonly GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> _query;

            public OrderBuilder(GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> query)
            {
                _query = query;
            }

            GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> IOrderBuilderWithBy<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>>.Build() => _query;

            IOrderBuilderWithBy<TElement, GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>> IOrderBuilder<TElement, GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>>.By(Expression<Func<TElement, object?>> projection) => By(projection, Gremlin.Net.Process.Traversal.Order.Asc);

            IOrderBuilderWithBy<TElement, GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>> IOrderBuilder<TElement, GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>>.By(Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, IGremlinQueryBase> traversal) => By(traversal, Gremlin.Net.Process.Traversal.Order.Asc);

            IOrderBuilderWithBy<TElement, GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>> IOrderBuilder<TElement, GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>>.By(ILambda lambda) => By(lambda);

            IOrderBuilderWithBy<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>> IOrderBuilder<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>>.By(Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, IGremlinQueryBase> traversal) => By(traversal, Gremlin.Net.Process.Traversal.Order.Asc);

            IOrderBuilderWithBy<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>> IOrderBuilder<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>>.By(ILambda lambda) => By(lambda);

            IOrderBuilderWithBy<TElement, GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>> IOrderBuilder<TElement, GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>>.ByDescending(Expression<Func<TElement, object?>> projection) => By(projection, Gremlin.Net.Process.Traversal.Order.Desc);

            IOrderBuilderWithBy<TElement, GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>> IOrderBuilder<TElement, GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>>.ByDescending(Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, IGremlinQueryBase> traversal) => By(traversal, Gremlin.Net.Process.Traversal.Order.Desc);

            IOrderBuilderWithBy<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>> IOrderBuilder<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>>.ByDescending(Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, IGremlinQueryBase> traversal) => By(traversal, Gremlin.Net.Process.Traversal.Order.Desc);

            private OrderBuilder By(Expression<Func<TElement, object?>> projection, Order order) => new(_query.AddStep(new OrderStep.ByMemberStep(_query.GetKey(projection), order)));

            private OrderBuilder By(Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, IGremlinQueryBase> traversal, Order order) => new(_query.AddStep(new OrderStep.ByTraversalStep(_query.Continue(traversal).ToTraversal(), order)));

            private OrderBuilder By(ILambda lambda) => new(_query.AddStep(new OrderStep.ByLambdaStep(lambda)));
        }

        private sealed class ChooseBuilder<TTargetQuery, TPickElement> :
            IChooseBuilder<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>>,
            IChooseBuilderWithCondition<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TPickElement>,
            IChooseBuilderWithCase<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TPickElement, TTargetQuery>
            where TTargetQuery : IGremlinQueryBase
        {
            private readonly GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> _sourceQuery;

            public ChooseBuilder(GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> sourceQuery, TTargetQuery targetQuery)
            {
                _sourceQuery = sourceQuery;
                TargetQuery = targetQuery;
            }

            public IChooseBuilderWithCondition<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TNewPickElement> On<TNewPickElement>(Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, IGremlinQueryBase<TNewPickElement>> chooseTraversal)
            {
                return new ChooseBuilder<TTargetQuery, TNewPickElement>(
                    _sourceQuery,
                    TargetQuery.AsAdmin().AddStep<TTargetQuery>(new ChooseOptionTraversalStep(_sourceQuery.Continue(chooseTraversal).ToTraversal())));
            }

            public IChooseBuilderWithCase<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TPickElement, TNewTargetQuery> Case<TNewTargetQuery>(TPickElement element, Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TNewTargetQuery> continuation) where TNewTargetQuery : IGremlinQueryBase
            {
                return new ChooseBuilder<TNewTargetQuery, TPickElement>(
                    _sourceQuery,
                    TargetQuery.AsAdmin().AddStep<TNewTargetQuery>(new OptionTraversalStep(element, _sourceQuery.Continue(continuation).ToTraversal())));
            }

            public IChooseBuilderWithCaseOrDefault<TNewTargetQuery> Default<TNewTargetQuery>(Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TNewTargetQuery> continuation) where TNewTargetQuery : IGremlinQueryBase
            {
                return new ChooseBuilder<TNewTargetQuery, TPickElement>(
                    _sourceQuery,
                    TargetQuery.AsAdmin().AddStep<TNewTargetQuery>(new OptionTraversalStep(default, _sourceQuery.Continue(continuation).ToTraversal())));
            }

            public IChooseBuilderWithCase<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TPickElement, TTargetQuery> Case(TPickElement element, Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TTargetQuery> continuation) => Case<TTargetQuery>(element, continuation);

            public IChooseBuilderWithCaseOrDefault<TTargetQuery> Default(Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TTargetQuery> continuation) => Default<TTargetQuery>(continuation);

            public TTargetQuery TargetQuery
            {
                get;
            }
        }

        private sealed class GroupBuilder<TKey, TValue> :
            IGroupBuilder<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>>,
            IGroupBuilderWithKeyAndValue<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TKey, TValue>
        {
            private readonly IGremlinQueryBase<TKey>? _keyQuery;
            private readonly IGremlinQueryBase<TValue>? _valueQuery;
            private readonly GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> _sourceQuery;

            public GroupBuilder(GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> sourceQuery, IGremlinQueryBase<TKey>? keyQuery = default, IGremlinQueryBase<TValue>? valueQuery = default)
            {
                _keyQuery = keyQuery;
                _valueQuery = valueQuery;
                _sourceQuery = sourceQuery;
            }

            IGroupBuilderWithKey<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TNewKey> IGroupBuilder<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>>.ByKey<TNewKey>(Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, IGremlinQueryBase<TNewKey>> keySelector)
            {
                return new GroupBuilder<TNewKey, object>(
                    _sourceQuery,
                    _sourceQuery.Continue(keySelector));
            }

            IGroupBuilderWithKeyAndValue<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TKey, TNewValue> IGroupBuilderWithKey<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TKey>.ByValue<TNewValue>(Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, IGremlinQueryBase<TNewValue>> valueSelector)
            {
                return new GroupBuilder<TKey, TNewValue>(
                    _sourceQuery,
                    KeyQuery,
                    _sourceQuery.Continue(valueSelector));
            }

            public IGremlinQueryBase<TKey> KeyQuery
            {
                get => _keyQuery is { } keyQuery
                    ? keyQuery
                    : throw new InvalidOperationException();
            }

            public IGremlinQueryBase<TValue> ValueQuery
            {
                get => _valueQuery is { } valueQuery
                    ? valueQuery
                    : throw new InvalidOperationException();
            }
        }

        private sealed partial class ProjectBuilder<TProjectElement, TItem1, TItem2, TItem3, TItem4, TItem5, TItem6, TItem7, TItem8, TItem9, TItem10, TItem11, TItem12, TItem13, TItem14, TItem15, TItem16> :
            IProjectBuilder<GremlinQuery<TProjectElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TProjectElement>,
            IProjectDynamicBuilder<GremlinQuery<TProjectElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TProjectElement>
        {
            private readonly GremlinQuery<TProjectElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> _sourceQuery;

            public ProjectBuilder(GremlinQuery<TProjectElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> sourceQuery) : this(sourceQuery, ImmutableDictionary<string, ProjectStep.ByStep>.Empty)
            {
            }

            private ProjectBuilder(GremlinQuery<TProjectElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> sourceQuery, IImmutableDictionary<string, ProjectStep.ByStep> projections)
            {
                _sourceQuery = sourceQuery;
                Projections = projections;
            }

            private ProjectBuilder<TProjectElement, TNewItem1, TNewItem2, TNewItem3, TNewItem4, TNewItem5, TNewItem6, TNewItem7, TNewItem8, TNewItem9, TNewItem10, TNewItem11, TNewItem12, TNewItem13, TNewItem14, TNewItem15, TNewItem16> By<TNewItem1, TNewItem2, TNewItem3, TNewItem4, TNewItem5, TNewItem6, TNewItem7, TNewItem8, TNewItem9, TNewItem10, TNewItem11, TNewItem12, TNewItem13, TNewItem14, TNewItem15, TNewItem16>(Func<GremlinQuery<TProjectElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, IGremlinQueryBase> projection, string? name = default)
            {
                return By<TNewItem1, TNewItem2, TNewItem3, TNewItem4, TNewItem5, TNewItem6, TNewItem7, TNewItem8, TNewItem9, TNewItem10, TNewItem11, TNewItem12, TNewItem13, TNewItem14, TNewItem15, TNewItem16>(new ProjectStep.ByTraversalStep(_sourceQuery.Continue(projection, true).ToTraversal()), name);
            }
           
            private ProjectBuilder<TProjectElement, TNewItem1, TNewItem2, TNewItem3, TNewItem4, TNewItem5, TNewItem6, TNewItem7, TNewItem8, TNewItem9, TNewItem10, TNewItem11, TNewItem12, TNewItem13, TNewItem14, TNewItem15, TNewItem16> By<TNewItem1, TNewItem2, TNewItem3, TNewItem4, TNewItem5, TNewItem6, TNewItem7, TNewItem8, TNewItem9, TNewItem10, TNewItem11, TNewItem12, TNewItem13, TNewItem14, TNewItem15, TNewItem16>(Expression projection, string? name = default)
            {
                return projection is LambdaExpression lambdaExpression && lambdaExpression.IsIdentityExpression()
                    ? By<TNewItem1, TNewItem2, TNewItem3, TNewItem4, TNewItem5, TNewItem6, TNewItem7, TNewItem8, TNewItem9, TNewItem10, TNewItem11, TNewItem12, TNewItem13, TNewItem14, TNewItem15, TNewItem16>(__ => __.Identity(), name)
                    : By<TNewItem1, TNewItem2, TNewItem3, TNewItem4, TNewItem5, TNewItem6, TNewItem7, TNewItem8, TNewItem9, TNewItem10, TNewItem11, TNewItem12, TNewItem13, TNewItem14, TNewItem15, TNewItem16>(new ProjectStep.ByKeyStep(_sourceQuery.GetKey(projection)), name);
            }

            private ProjectBuilder<TProjectElement, TNewItem1, TNewItem2, TNewItem3, TNewItem4, TNewItem5, TNewItem6, TNewItem7, TNewItem8, TNewItem9, TNewItem10, TNewItem11, TNewItem12, TNewItem13, TNewItem14, TNewItem15, TNewItem16> By<TNewItem1, TNewItem2, TNewItem3, TNewItem4, TNewItem5, TNewItem6, TNewItem7, TNewItem8, TNewItem9, TNewItem10, TNewItem11, TNewItem12, TNewItem13, TNewItem14, TNewItem15, TNewItem16>(ProjectStep.ByStep step, string? name = default)
            {
                return new(
                    _sourceQuery,
                    Projections.SetItem(name ?? $"Item{Projections.Count + 1}", step));
            }

            IProjectTupleBuilder<GremlinQuery<TProjectElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TProjectElement> IProjectBuilder<GremlinQuery<TProjectElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TProjectElement>.ToTuple()
            {
                return this;
            }

            IProjectDynamicBuilder<GremlinQuery<TProjectElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TProjectElement> IProjectBuilder<GremlinQuery<TProjectElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TProjectElement>.ToDynamic()
            {
                return this;
            }

            IProjectDynamicBuilder<GremlinQuery<TProjectElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TProjectElement> IProjectDynamicBuilder<GremlinQuery<TProjectElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TProjectElement>.By(Func<GremlinQuery<TProjectElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, IGremlinQueryBase> projection)
            {
                return By<object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object>(projection);
            }

            IProjectDynamicBuilder<GremlinQuery<TProjectElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TProjectElement> IProjectDynamicBuilder<GremlinQuery<TProjectElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TProjectElement>.By(string name, Func<GremlinQuery<TProjectElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, IGremlinQueryBase> projection)
            {
                return By<object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object>(projection, name);
            }

            IProjectDynamicBuilder<GremlinQuery<TProjectElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TProjectElement> IProjectDynamicBuilder<GremlinQuery<TProjectElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TProjectElement>.By(string name, Expression<Func<TProjectElement, object>> projection)
            {
                return By<object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object>(projection, name);
            }

            IProjectDynamicBuilder<GremlinQuery<TProjectElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TProjectElement> IProjectDynamicBuilder<GremlinQuery<TProjectElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TProjectElement>.By(Expression<Func<TProjectElement, object>> projection)
            {
                return projection.IsIdentityExpression()
                    ? By<object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object>(__ => __.Identity())
                    : projection.Body.Strip() is MemberExpression memberExpression
                        ? By<object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object>(memberExpression, memberExpression.Member.Name)
                        : throw new ExpressionNotSupportedException(projection);
            }

            public IImmutableDictionary<string, ProjectStep.ByStep> Projections { get; }
        }

        public GremlinQuery(
            StepStack steps,
            IGremlinQueryEnvironment environment,
            IImmutableDictionary<StepLabel, QuerySemantics> stepLabelSemantics,
            QueryFlags flags) : base(steps, environment, stepLabelSemantics, flags)
        {

        }

        private GremlinQuery<TEdge, TElement, object, object, object, object> AddE<TEdge>(TEdge newEdge, QuerySemantics semantics)
        {
            return this
                .AddStep<TEdge, TElement, object, object, object, object>(new AddEStep(Environment.Model.EdgesModel.GetCache().GetLabel(newEdge!.GetType()), semantics))
                .AddOrUpdate(newEdge, true);
        }

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> AddOrUpdate(TElement element, bool add)
        {
            var ret = this;
            var props = element.Serialize(
                Environment,
                add
                    ? SerializationBehaviour.IgnoreOnAdd
                    : SerializationBehaviour.IgnoreOnUpdate);

            if (!add)
            {
                ret = ret.SideEffect(_ => _
                    .Properties<object, object, object>(
                        props
                            .Select(p => p.key.RawKey)
                            .OfType<string>(),
                        Semantics)
                    .Drop());
            }

            foreach (var (key, value) in props)
            {
                if (!Environment.FeatureSet.Supports(VertexFeatures.UserSuppliedIds) && T.Id.Equals(key.RawKey))
                    Environment.Logger.LogWarning($"User supplied ids are not supported according to the environment's {nameof(Environment.FeatureSet)}.");
                else
                    ret = ret.AddSteps(GetPropertySteps(key, value, Semantics.IsVertex));
            }

            return ret;
        }

        private IEnumerable<PropertyStep> GetPropertySteps(Key key, object value, bool allowExplicitCardinality)
        {
            if (value is IEnumerable enumerable && !Environment.GetCache().FastNativeTypes.ContainsKey(value.GetType()))
            {
                if (!allowExplicitCardinality)
                    throw new NotSupportedException($"A value of type {value.GetType()} is not supported for property '{key}'.");

                foreach (var item in enumerable)
                {
                    if (TryGetPropertyStep(key, item, Cardinality.List) is { } step)
                        yield return step;
                }
            }
            else
            {
                if (TryGetPropertyStep(key, value, allowExplicitCardinality ? Cardinality.Single : default) is { } step)
                    yield return step;
            }
        }

        private PropertyStep? TryGetPropertyStep(Key key, object value, Cardinality? cardinality)
        {
            object? actualValue = value;
            var metaProperties = ImmutableArray<KeyValuePair<string, object>>.Empty;

            if (actualValue is Property property)
            {
                if (property is IVertexProperty vertexProperty)
                {
                    metaProperties = vertexProperty.GetProperties(Environment)
                        .ToImmutableArray();
                }

                actualValue = property.GetValue();
            }

            return actualValue != null
                ? new PropertyStep.ByKeyStep(key, actualValue, metaProperties, cardinality)
                : null;
        }

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> AddSteps(IEnumerable<Step> steps, IImmutableDictionary<StepLabel, QuerySemantics>? stepLabelSemantics = null, QueryFlags additionalFlags = QueryFlags.None) => AddSteps<TElement>(steps, stepLabelSemantics, additionalFlags);

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> AddStep(Step step, IImmutableDictionary<StepLabel, QuerySemantics>? stepLabelSemantics = null, QueryFlags additionalFlags = QueryFlags.None) => AddStep<TElement>(step, stepLabelSemantics, additionalFlags);

        private GremlinQuery<TNewElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> AddSteps<TNewElement>(IEnumerable<Step> steps, IImmutableDictionary<StepLabel, QuerySemantics>? stepLabelSemantics = null, QueryFlags additionalFlags = QueryFlags.None) => AddSteps<TNewElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>(steps, stepLabelSemantics, additionalFlags);

        private GremlinQuery<TNewElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> AddStep<TNewElement>(Step step, IImmutableDictionary<StepLabel, QuerySemantics>? stepLabelSemantics = null, QueryFlags additionalFlags = QueryFlags.None) => AddStep<TNewElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>(step, stepLabelSemantics, additionalFlags);

        private GremlinQuery<TNewElement, object, object, object, object, object> AddStepWithObjectTypes<TNewElement>(Step step) => AddStep<TNewElement, object, object, object, object, object>(step);

        private GremlinQuery<TNewElement, TNewOutVertex, TNewInVertex, TNewPropertyValue, TNewMeta, TNewFoldedQuery> AddStep<TNewElement, TNewOutVertex, TNewInVertex, TNewPropertyValue, TNewMeta, TNewFoldedQuery>(Step step, IImmutableDictionary<StepLabel, QuerySemantics>? stepLabelSemantics = null, QueryFlags additionalFlags = QueryFlags.None)
        {
            var newSteps = Steps;

            if ((Flags & QueryFlags.IsMuted) == 0)
                newSteps = Environment.AddStepHandler.AddStep(newSteps, step, Environment);

            return new GremlinQuery<TNewElement, TNewOutVertex, TNewInVertex, TNewPropertyValue, TNewMeta, TNewFoldedQuery>(newSteps, Environment, stepLabelSemantics ?? StepLabelSemantics, Flags | additionalFlags);
        }

        private GremlinQuery<TNewElement, TNewOutVertex, TNewInVertex, TNewPropertyValue, TNewMeta, TNewFoldedQuery> AddSteps<TNewElement, TNewOutVertex, TNewInVertex, TNewPropertyValue, TNewMeta, TNewFoldedQuery>(IEnumerable<Step> steps, IImmutableDictionary<StepLabel, QuerySemantics>? stepLabelSemantics = null, QueryFlags additionalFlags = QueryFlags.None)
        {
            var newSteps = Steps;

            if ((Flags & QueryFlags.IsMuted) == 0)
            {
                foreach (var step in steps)
                {
                    newSteps = Environment.AddStepHandler.AddStep(newSteps, step, Environment);
                }
            }

            return new GremlinQuery<TNewElement, TNewOutVertex, TNewInVertex, TNewPropertyValue, TNewMeta, TNewFoldedQuery>(newSteps, Environment, stepLabelSemantics ?? StepLabelSemantics, Flags | additionalFlags);
        }

        private GremlinQuery<TVertex, object, object, object, object, object> AddV<TVertex>(TVertex vertex, QuerySemantics semantics)
        {
            return this
                .AddStepWithObjectTypes<TVertex>(new AddVStep(Environment.Model.VerticesModel.GetCache().GetLabel(vertex!.GetType()), semantics))
                .AddOrUpdate(vertex, true);
        }

        private TTargetQuery Aggregate<TStepLabel, TTargetQuery>(Scope scope, TStepLabel stepLabel, Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TStepLabel, TTargetQuery> continuation)
            where TStepLabel : StepLabel
            where TTargetQuery : IGremlinQueryBase
        {
            return continuation(
                AddStep(new AggregateStep(scope, stepLabel)),
                stepLabel);
        }

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> And(params Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, IGremlinQueryBase>[] andTraversalTransformations)
        {
            if (andTraversalTransformations.Length == 0)
                return AddStep(AndStep.Infix);

            List<IGremlinQueryBase>? subQueries = default;

            foreach (var transformation in andTraversalTransformations)
            {
                var transformed = Continue(transformation);

                if (transformed.IsNone())
                    return None();

                if (!transformed.IsIdentity())
                    (subQueries ??= new List<IGremlinQueryBase>()).Add(transformed);
            }

            var fusedTraversals = subQueries?
                .Select(x => x.ToTraversal().RewriteForWhereContext())
                .Fuse(
                    (p1, p2) => p1.And(p2))
                .ToArray();

            return fusedTraversals?.Length switch
            {
                null or 0 => this,
                1 => Where(fusedTraversals[0]),
                _ => AddStep(new AndStep(fusedTraversals!))
            };
        }

        private TTargetQuery Continue<TTargetQuery>(Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TTargetQuery> transformation, bool surfaceVisible = false)
        {
            var targetQuery = transformation(new GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>(StepStack.Empty(Semantics), Environment, StepLabelSemantics, (surfaceVisible ? Flags | QueryFlags.SurfaceVisible : Flags & ~QueryFlags.SurfaceVisible) | QueryFlags.IsAnonymous));

            if (targetQuery is GremlinQueryBase queryBase && (queryBase.Flags & QueryFlags.IsAnonymous) == QueryFlags.None)
                throw new InvalidOperationException("A query continuation must originate from the query that was passed to the continuation function. Did you accidentally use 'g' in the continuation?");

            return targetQuery;
        }

        private TTargetQuery As<TTargetQuery>(Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, StepLabel<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TElement>, TTargetQuery> continuation)
            where TTargetQuery : IGremlinQueryBase
        {
            return As<StepLabel<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TElement>, TTargetQuery>(continuation);
        }

        private TTargetQuery As<TStepLabel, TTargetQuery>(Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TStepLabel, TTargetQuery> continuation)
            where TStepLabel : StepLabel, new()
            where TTargetQuery : IGremlinQueryBase
        {
            TStepLabel stepLabel;
            var toContinue = this;

            if (Steps.PeekOrDefault() is AsStep { StepLabel: TStepLabel existingStepLabel })
                stepLabel = existingStepLabel;
            else
            {
                stepLabel = new TStepLabel();
                toContinue = As(stepLabel);
            }

            return continuation(
                toContinue,
                stepLabel);
        }

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> As(StepLabel stepLabel)
        {
            return AddStep(
                new AsStep(stepLabel),
                StepLabelSemantics.SetItem(stepLabel, Semantics));
        }

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> Barrier() => AddStep(BarrierStep.Instance);

        private GremlinQuery<TTarget, object, object, object, object, object> BothV<TTarget>(QuerySemantics semantics) => AddStepWithObjectTypes<TTarget>(new BothVStep(semantics));

        private GremlinQuery<TNewElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> Cast<TNewElement>()
        {
            if (typeof(TNewElement) == typeof(TElement))
                return (GremlinQuery<TNewElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>)(object)this;

            var newSemantics = Semantics.Cast<TNewElement>();

            var newSteps = newSemantics != Semantics
                ? Steps.Push(new ChangeQuerySemanticsStep(newSemantics))
                : Steps;

             return new(newSteps, Environment, StepLabelSemantics, Flags);
        }

        private TTargetQuery Choose<TTrueQuery, TFalseQuery, TTargetQuery>(Expression<Func<TElement, bool>> predicate, Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TTrueQuery> trueChoice, Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TFalseQuery>? maybeFalseChoice = default)
            where TTrueQuery : IGremlinQueryBase
            where TFalseQuery : IGremlinQueryBase
            where TTargetQuery : IGremlinQueryBase
        {
            var trueQuery = Continue(trueChoice);
            var maybeFalseQuery = maybeFalseChoice is { } falseChoice
                ? Continue(falseChoice)
                : default;

            var query = Continue(__ => __.Where(predicate));

            if (query.Steps.Count == 1 && query.Steps[0] is IsStep isStep)
            {
                return this
                    .AddStep(
                        new ChoosePredicateStep(
                            isStep.Predicate,
                            trueQuery.ToTraversal(),
                            maybeFalseQuery?.ToTraversal()))
                    .ChangeQueryType<TTargetQuery>();
            }

            return this
                .AddStep(
                    new ChooseTraversalStep(
                        query.ToTraversal(),
                        trueQuery.ToTraversal(),
                        maybeFalseQuery?.ToTraversal()))
                .ChangeQueryType<TTargetQuery>();
        }

        private TTargetQuery Choose<TTrueQuery, TFalseQuery, TTargetQuery>(Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, IGremlinQueryBase> traversalPredicate, Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TTrueQuery> trueChoice, Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TFalseQuery>? maybeFalseChoice = default)
            where TTrueQuery : IGremlinQueryBase
            where TFalseQuery : IGremlinQueryBase
            where TTargetQuery : IGremlinQueryBase
        {
            var trueQuery = Continue(trueChoice);
            var maybeFalseQuery = maybeFalseChoice is { } falseChoice
                ? Continue(falseChoice)
                : default;

            return AddStep(new ChooseTraversalStep(Continue(traversalPredicate).ToTraversal(), trueQuery.ToTraversal(), maybeFalseQuery?.ToTraversal()))
                .ChangeQueryType<TTargetQuery>();
        }

        private TTargetQuery Choose<TTargetQuery>(Func<IChooseBuilder<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>>, IChooseBuilderWithCaseOrDefault<TTargetQuery>> continuation)
            where TTargetQuery : IGremlinQueryBase
        {
            return continuation(new ChooseBuilder<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, object>(this, this)).TargetQuery;
        }

        private TReturnQuery Coalesce<TTargetQuery, TReturnQuery>(params Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TTargetQuery>[] traversals)
            where TTargetQuery : IGremlinQueryBase
            where TReturnQuery : IGremlinQueryBase
        {
            if (traversals.Length == 0)
                throw new ArgumentException("Coalesce must have at least one subquery.");

            var coalesceQueries = traversals
                .Select(traversal => Continue(traversal))
                .ToArray();

            if (coalesceQueries.All(x => x.IsIdentity()))
                return this.ChangeQueryType<TReturnQuery>();

            var aggregatedSemantics = coalesceQueries
                .Select(x => x.AsAdmin().Semantics)
                .Aggregate((x, y) => x.HighestCommon(y));

            return this
                .AddStep(new CoalesceStep(coalesceQueries.Select(x => x.ToTraversal()).ToImmutableArray()))
                .ChangeQueryType<TReturnQuery>(aggregatedSemantics);
        }

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> Coin(double probability) => AddStep(new CoinStep(probability));

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> ConfigureEnvironment(Func<IGremlinQueryEnvironment, IGremlinQueryEnvironment> transformation) => Configure<TElement>(_ => _, transformation);

        private GremlinQuery<TNewElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> ConfigureSteps<TNewElement>(Func<StepStack, StepStack> transformation) => Configure<TNewElement>(transformation, _ => _);

        private GremlinQuery<TNewElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> Configure<TNewElement>(
            Func<StepStack, StepStack> stepsTransformation,
            Func<IGremlinQueryEnvironment, IGremlinQueryEnvironment> environmentTransformation) => new(stepsTransformation(Steps), environmentTransformation(Environment), StepLabelSemantics, Flags);

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> CyclicPath() => AddStep(new CyclicPathStep());

        private string Debug(GroovyFormatting groovyFormatting, bool indented)
        {
            return JsonConvert.SerializeObject(
                Environment.Serializer
                    .ToGroovy(groovyFormatting)
                    .Serialize(this),
                indented
                    ? Formatting.Indented
                    : Formatting.None);
        }

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> DedupGlobal() => AddStep(DedupStep.Global);

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> DedupLocal() => AddStep(DedupStep.Local);

        private GremlinQuery<object, object, object, object, object, object> Drop() => AddStepWithObjectTypes<object>(new DropStep(typeof(IValueGremlinQuery<object>)));

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> DropProperties(string key)
        {
            return SideEffect(_ => _
                .Properties<object, object, object>(new[] { key }, typeof(IPropertyGremlinQueryBase))
                .Drop());
        }

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> Emit() => AddStep(EmitStep.Instance);

        private TTargetQuery FlatMap<TTargetQuery>(Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TTargetQuery> mapping) where TTargetQuery : IGremlinQueryBase
        {
            var mappedTraversal = Continue(mapping);

            return this
                .AddStep(new FlatMapStep(mappedTraversal.ToTraversal()))
                .ChangeQueryType<TTargetQuery>();
        }

        private GremlinQuery<TElement[], object, object, TElement, object, TNewFoldedQuery> Fold<TNewFoldedQuery>(QuerySemantics semantics) => AddStep<TElement[], object, object, TElement, object, TNewFoldedQuery>(new FoldStep(semantics));

        private GremlinQuery<IDictionary<TKey, TValue>, object, object, object, object, object> Group<TKey, TValue>(Func<IGroupBuilder<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>>, IGroupBuilderWithKeyAndValue<IGremlinQueryBase, TKey, TValue>> projection, QuerySemantics semantics)
        {
            var group = projection(new GroupBuilder<object, object>(this));

            return this
                .AddStep<IDictionary<TKey, TValue>, object, object, object, object, object>(new GroupStep(semantics))
                .AddStep(new GroupStep.ByTraversalStep(group.KeyQuery.ToTraversal()))
                .AddStep(new GroupStep.ByTraversalStep(group.ValueQuery.ToTraversal()));
        }

        private GremlinQuery<IDictionary<TKey, object>, object, object, object, object, object> Group<TKey>(Func<IGroupBuilder<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>>, IGroupBuilderWithKey<IGremlinQueryBase, TKey>> projection, QuerySemantics semantics)
        {
            var group = projection(new GroupBuilder<object, object>(this));

            return this
                .AddStep<IDictionary<TKey, object>, object, object, object, object, object>(new GroupStep(semantics))
                .AddStep(new GroupStep.ByTraversalStep(group.KeyQuery.ToTraversal()));
        }

        private IEnumerable<string> GetStringKeys(Expression[] projections)
        {
            foreach (var projection in projections)
            {
                if (GetKey(projection).RawKey is string str)
                    yield return str;
            }
        }

        private Key GetKey(Expression projection)
        {
            return Environment.GetKey(projection);
        }

        // ReSharper disable once SuggestBaseTypeForParameter
        private static IEnumerable<Step> GetStepsForKeys(IEnumerable<Key> keys, QuerySemantics semantics)
        {
            var hasYielded = false;
            var stringKeys = default(List<string>?);

            foreach (var key in keys)
            {
                switch (key.RawKey)
                {
                    case T t:
                    {
                        if (T.Id.Equals(t))
                            yield return new IdStep(semantics);
                        else if (T.Label.Equals(t))
                            yield return new LabelStep(semantics);
                        else
                            throw new ExpressionNotSupportedException($"Can't find an appropriate Gremlin step for {t}.");

                        hasYielded = true;
                        break;
                    }
                    case string str:
                    {
                        (stringKeys ??= new List<string>()).Add(str);
                        break;
                    }
                }
            }

            if (stringKeys?.Count > 0 || !hasYielded)
                yield return new ValuesStep(stringKeys?.ToImmutableArray() ?? ImmutableArray<string>.Empty, semantics);
        }
        
        private GremlinQuery<object, object, object, object, object, object> Id(QuerySemantics semantics) => AddStepWithObjectTypes<object>(new IdStep(semantics));

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> Identity() => this;

        private GremlinQuery<TNewElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> Inject<TNewElement>(IEnumerable<TNewElement> elements, QuerySemantics semantics) => AddStep<TNewElement>(new InjectStep(elements.Cast<object>().Where(x => x is not null).Select(x => x!).ToImmutableArray(), semantics));

        private GremlinQuery<TNewElement, object, object, object, object, object> InV<TNewElement>(QuerySemantics semantics) => AddStepWithObjectTypes<TNewElement>(new InVStep(semantics));

        private GremlinQuery<string, object, object, object, object, object> Key(QuerySemantics semantics) => AddStepWithObjectTypes<string>(new KeyStep(semantics));

        private GremlinQuery<string, object, object, object, object, object> Label(QuerySemantics semantics) => AddStepWithObjectTypes<string>(new LabelStep(semantics));

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> LimitGlobal(long count)
        {
            return AddStep(
                count == 1
                    ? LimitStep.LimitGlobal1
                    : new LimitStep(count, Scope.Global));
        }

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> LimitLocal(long count)
        {
            return count == 1
                ? AddStep(LimitStep.LimitLocal1)
                : AddStep(new LimitStep(count, Scope.Local));
        }

        private TTargetQuery Local<TTargetQuery>(Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TTargetQuery> localTraversal)
            where TTargetQuery : IGremlinQueryBase
        {
            var localTraversalQuery = Continue(localTraversal);

            return (localTraversalQuery.IsIdentity()
                ? this
                : AddStep(new LocalStep(localTraversalQuery.ToTraversal()))).ChangeQueryType<TTargetQuery>();
        }

        private TTargetQuery Map<TTargetQuery>(Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TTargetQuery> mapping) where TTargetQuery : IGremlinQueryBase
        {
            var mappedTraversal = Continue(mapping);

            return (mappedTraversal.IsIdentity()
                ? this
                : AddStep(new MapStep(mappedTraversal.ToTraversal()))).ChangeQueryType<TTargetQuery>();
        }

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> None()
        {
            return this.IsIdentity()
                ? ConfigureSteps<TElement>(_ => StepStack.Empty(_.Semantics).Push(NoneStep.Instance))
                : AddStep(NoneStep.Instance);
        }

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> Not(Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, IGremlinQueryBase> notTraversal)
        {
            var transformed = Continue(notTraversal);
            
            return transformed.IsIdentity()
                ? None()
                : transformed.IsNone()
                    ? this
                    : AddStep(new NotStep(transformed.ToTraversal()));
        }

        private GremlinQuery<TTarget, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> OfType<TTarget>(IGraphElementModel model, QuerySemantics semantics)
        {
            if (typeof(TTarget).IsAssignableFrom(typeof(TElement)))
                return Cast<TTarget>();

            var labels = model
                .TryGetFilterLabels(typeof(TTarget), Environment.Options.GetValue(GremlinqOption.FilterLabelsVerbosity)) ?? ImmutableArray.Create(typeof(TTarget).Name);

            return labels.Length > 0
                ? AddStep<TTarget>(new HasLabelStep(labels, semantics))
                : Cast<TTarget>();
        }

        private TTargetQuery Optional<TTargetQuery>(Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TTargetQuery> optionalTraversal) where TTargetQuery : IGremlinQueryBase
        {
            var optionalQuery = Continue(optionalTraversal);

            return this
                .AddStep(new OptionalStep(optionalQuery.ToTraversal()))
                .ChangeQueryType<TTargetQuery>();
        }

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> Or(params Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, IGremlinQueryBase>[] orTraversalTransformations)
        {
            return Or(orTraversalTransformations.Select(transformation => Continue(transformation)).ToArray());
        }

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> Or(params IGremlinQueryBase[] orTraversals)
        {
            if (orTraversals.Length == 0)
                return AddStep(OrStep.Infix);

            List<IGremlinQueryBase>? subQueries = default;

            foreach (var transformed in orTraversals)
            {
                if (transformed.IsIdentity())
                    return this;

                if (!transformed.IsNone())
                    (subQueries ??= new List<IGremlinQueryBase>()).Add(transformed);
            }

            var fusedTraversals = subQueries?
                .Select(x => x.ToTraversal().RewriteForWhereContext())
                .Fuse(
                    (p1, p2) => p1.Or(p2))
                .ToArray();

            return fusedTraversals?.Length switch
            {
                null or 0 => None(),
                1 => Where(fusedTraversals[0]),
                _ => AddStep(new OrStep(fusedTraversals))
            };
        }

        private TTargetQuery OrderGlobal<TTargetQuery>(Func<OrderBuilder, IOrderBuilderWithBy<TTargetQuery>> projection) where TTargetQuery : IGremlinQueryBase<TElement> => Order(projection, OrderStep.Global);

        private TTargetQuery OrderLocal<TTargetQuery>(Func<OrderBuilder, IOrderBuilderWithBy<TTargetQuery>> projection) where TTargetQuery : IGremlinQueryBase<TElement> => Order(projection, OrderStep.Local);

        private TTargetQuery Order<TTargetQuery>(Func<OrderBuilder, IOrderBuilderWithBy<TTargetQuery>> projection, OrderStep orderStep) where TTargetQuery : IGremlinQueryBase<TElement> => projection(new OrderBuilder(AddStep(orderStep))).Build();

        private GremlinQuery<TTarget, object, object, object, object, object> OtherV<TTarget>(QuerySemantics semantics) => AddStepWithObjectTypes<TTarget>(new OtherVStep(semantics));

        private GremlinQuery<TTarget, object, object, object, object, object> OutV<TTarget>(QuerySemantics semantics) => AddStepWithObjectTypes<TTarget>(new OutVStep(semantics));

        private GremlinQuery<TResult, object, object, object, object, object> Project<TResult>(Func<IProjectBuilder<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TElement>, IProjectResult> continuation, QuerySemantics semantics)
        {
            var projections = continuation(new ProjectBuilder<TElement, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object>(this))
                .Projections
                .OrderBy(x => x.Key)
                .ToArray();

            var ret = this
                .AddStepWithObjectTypes<TResult>(new ProjectStep(projections.Select(x => x.Key).ToImmutableArray(), semantics));

            foreach (var projection in projections)
            {
                ret = ret.AddStep(projection.Value);
            }

            return ret;
        }

        private GremlinQuery<TNewElement, object, object, TNewPropertyValue, TNewMeta, object> Properties<TNewElement, TNewPropertyValue, TNewMeta>(QuerySemantics querySemantics, params Expression[] projections)
        {
            return Properties<TNewElement, TNewPropertyValue, TNewMeta>(
                projections
                    .Select(projection => GetKey(projection).RawKey)
                    .OfType<string>(),
                querySemantics);
        }

        private GremlinQuery<TNewElement, object, object, TNewPropertyValue, TNewMeta, object> Properties<TNewElement, TNewPropertyValue, TNewMeta>(IEnumerable<string> keys, QuerySemantics querySemantics) => AddStep<TNewElement, object, object, TNewPropertyValue, TNewMeta, object>(new PropertiesStep(keys.ToImmutableArray(), querySemantics));

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> Property(LambdaExpression projection, object? value) => Property(GetKey(projection), value);

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> Property(Key key, object? value)
        {
            if (value == null)
            {
                if (key.RawKey is string stringKey)
                    return DropProperties(stringKey);

                throw new InvalidOperationException("Can't set a special property to null.");
            }

            var ret = this;

            foreach (var propertyStep in GetPropertySteps(key, value, Semantics.IsVertex))
            {
                ret = ret.AddStep(propertyStep);
            }

            return ret;
        }

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> Property(Key key, Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, IGremlinQueryBase> valueTraversal) => Property(key, Continue(valueTraversal));

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> Property(LambdaExpression projection, Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, IGremlinQueryBase> valueTraversal) => Property(projection, Continue(valueTraversal));

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> RangeGlobal(long low, long high) => AddStep(new RangeStep(low, high, Scope.Global));

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> RangeLocal(long low, long high)
        {
            return AddStep(new RangeStep(low, high, Scope.Local));
        }

        private TTargetQuery Repeat<TTargetQuery>(Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TTargetQuery> repeatTraversal)
            where TTargetQuery : IGremlinQueryBase
        {
            var repeatQuery = Continue(repeatTraversal);

            return this
                .AddStep(new RepeatStep(repeatQuery.ToTraversal()))
                .ChangeQueryType<TTargetQuery>();
        }

        private TTargetQuery RepeatUntil<TTargetQuery>(Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TTargetQuery> repeatTraversal, Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, IGremlinQueryBase> untilTraversal)
            where TTargetQuery : IGremlinQueryBase
        {
            var repeatQuery = Continue(repeatTraversal);
            var untilQuery = Continue(untilTraversal);

            var ret = this
                .AddStep(new RepeatStep(repeatQuery.ToTraversal()));

            if (!untilQuery.IsNone())
                ret = ret.AddStep(new UntilStep(untilQuery.ToTraversal()));

            return ret
                .ChangeQueryType<TTargetQuery>();
        }

        private TTargetQuery UntilRepeat<TTargetQuery>(Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TTargetQuery> repeatTraversal, Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, IGremlinQueryBase> untilTraversal)
            where TTargetQuery : IGremlinQueryBase
        {
            var repeatQuery = Continue(repeatTraversal);
            var untilQuery = Continue(untilTraversal);

            var ret = this;

            if (!untilQuery.IsNone())
                ret = ret.AddStep(new UntilStep(Continue(untilTraversal).ToTraversal()));

            return ret
                .AddStep(new RepeatStep(repeatQuery.ToTraversal()))
                .ChangeQueryType<TTargetQuery>();
        }

        private GremlinQuery<TSelectedElement, object, object, object, object, object> Select<TSelectedElement>(StepLabel<TSelectedElement> stepLabel)
        {
            if (StepLabelSemantics.TryGetValue(stepLabel, out var stepLabelSemantics))
                return AddStepWithObjectTypes<TSelectedElement>(new SelectStep(ImmutableArray.Create<StepLabel>(stepLabel), stepLabelSemantics));

            throw new InvalidOperationException($"Invalid use of unknown {nameof(StepLabel)} in {nameof(Select)}. Make sure you only pass in a {nameof(StepLabel)} that comes from a previous {nameof(As)}- or {nameof(IGremlinQuerySource.WithSideEffect)}-continuation or has previously been passed to an appropriate overload of {nameof(As)} or {nameof(IGremlinQuerySource.WithSideEffect)}.");
        }

        private TTargetQuery Select<TTargetQuery>(params Expression[] projections)
        {
            return AddStep(new SelectKeysStep(projections
                    .Select(GetKey)
                    .ToImmutableArray()))
                .ChangeQueryType<TTargetQuery>();
        }

        private GremlinQuery<TSelectedElement, object, object, TArrayItem, object, TQuery> Cap<TSelectedElement, TArrayItem, TQuery>(StepLabel<IArrayGremlinQuery<TSelectedElement, TArrayItem, TQuery>, TSelectedElement> stepLabel, QuerySemantics semantics) where TQuery : IGremlinQueryBase => AddStep<TSelectedElement, object, object, TArrayItem, object, TQuery>(new CapStep(stepLabel, semantics));

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> SideEffect(Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, IGremlinQueryBase> sideEffectTraversal) => AddStep(new SideEffectStep(Continue(sideEffectTraversal).ToTraversal()));

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> SimplePath() => AddStep(new SimplePathStep());

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> Skip(long count, Scope scope) => AddStep(new SkipStep(count, scope));

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> SumGlobal(QuerySemantics semantics) => AddStep(new SumStep(Scope.Global, semantics));

        private GremlinQuery<object, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> SumLocal(QuerySemantics semantics) => AddStep<object>(new SumStep(Scope.Local, semantics));

        private GremlinQuery<object, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> MinLocal(QuerySemantics semantics) => AddStep<object>(new MinStep(Scope.Local, semantics));

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> Mute() => AddStep(NoneStep.Instance, additionalFlags: QueryFlags.IsMuted);

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> MinGlobal(QuerySemantics semantics) => AddStep(new MinStep(Scope.Global, semantics));

        private GremlinQuery<object, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> MaxLocal(QuerySemantics semantics) => AddStep<object>(new MaxStep(Scope.Local, semantics));

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> MaxGlobal(QuerySemantics semantics) => AddStep(new MaxStep(Scope.Global, semantics));

        private GremlinQuery<object, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> MeanLocal(QuerySemantics semantics) => AddStep<object>(new MeanStep(Scope.Local, semantics));

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> MeanGlobal(QuerySemantics semantics) => AddStep(new MeanStep(Scope.Global, semantics));

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> TailGlobal(long count) => AddStep(new TailStep(count, Scope.Global));

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> TailLocal(long count)
        {
            return count == 1
                ? AddStep(TailStep.TailLocal1)
                : AddStep(new TailStep(count, Scope.Local));
        }

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> Times(int count) => AddStep(new TimesStep(count));

        private GremlinQuery<TNewElement, TNewOutVertex, TNewInVertex, object, object, object> To<TNewElement, TNewOutVertex, TNewInVertex>(StepLabel stepLabel) => AddStep<TNewElement, TNewOutVertex, TNewInVertex, object, object, object>(new AddEStep.ToLabelStep(stepLabel));

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> Unfold() => AddStep(UnfoldStep.Instance);

        private TTargetQuery Unfold<TTargetQuery>() => Unfold().ChangeQueryType<TTargetQuery>();

        private TReturnQuery Union<TTargetQuery, TReturnQuery>(params Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, TTargetQuery>[] unionTraversals)
            where TTargetQuery : IGremlinQueryBase
            where TReturnQuery : IGremlinQueryBase
        {
            var unionQueries = unionTraversals
                .Select(unionTraversal => Continue(unionTraversal))
                .ToArray();

            var aggregatedSemantics = unionQueries
                .Select(x => x.AsAdmin().Semantics)
                .Aggregate((x, y) => x.HighestCommon(y));

            return this
                .AddStep(new UnionStep(unionQueries.Select(x => x.ToTraversal()).ToImmutableArray()))
                .ChangeQueryType<TReturnQuery>(aggregatedSemantics);
        }

        private IValueGremlinQuery<TNewPropertyValue> Value<TNewPropertyValue>(QuerySemantics semantics) => AddStepWithObjectTypes<TNewPropertyValue>(new ValueStep(semantics));

        private GremlinQuery<TNewElement, object, object, object, object, object> ValueMap<TNewElement>(ImmutableArray<string> keys, QuerySemantics semantics) => AddStepWithObjectTypes<TNewElement>(new ValueMapStep(keys, semantics));

        private GremlinQuery<TNewElement, object, object, object, object, object> ValueMap<TNewElement>(IEnumerable<LambdaExpression> projections, QuerySemantics semantics)
        {
            var projectionsArray = projections
                .ToArray<Expression>();

            var stringKeys = GetStringKeys(projectionsArray)
                .ToImmutableArray();

            if (stringKeys.Length != projectionsArray.Length)
                throw new ExpressionNotSupportedException($"One of the expressions in {nameof(ValueMap)} maps to a {nameof(T)}-token. Can't have special tokens in {nameof(ValueMap)}.");

            return AddStepWithObjectTypes<TNewElement>(new ValueMapStep(stringKeys, semantics));
        }

        private GremlinQuery<TValue, object, object, object, object, object> ValuesForKeys<TValue>(IEnumerable<Key> keys, QuerySemantics semantics)
        {
            var stepsArray = GetStepsForKeys(keys, semantics)
                .ToArray();

            return stepsArray.Length switch
            {
                0 => throw new ExpressionNotSupportedException(),
                1 => AddStepWithObjectTypes<TValue>(stepsArray[0]),
                _ => AddStepWithObjectTypes<TValue>(new UnionStep(stepsArray.Select(step => Continue(__ => __.AddStep(step).ToTraversal())).ToImmutableArray(), semantics))
            };
        }

        private GremlinQuery<TValue, object, object, object, object, object> ValuesForProjections<TValue>(IEnumerable<LambdaExpression> projections, QuerySemantics semantics) => ValuesForKeys<TValue>(projections.Select(projection => GetKey(projection)), semantics);

        private GremlinQuery<VertexProperty<TNewPropertyValue>, object, object, TNewPropertyValue, object, object> VertexProperties<TNewPropertyValue>(Expression[] projections, QuerySemantics semantics) => Properties<VertexProperty<TNewPropertyValue>, TNewPropertyValue, object>(semantics, projections);
        
        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> Where(ILambda lambda) => AddStep(new FilterStep(lambda));

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> Where(Func<GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>, IGremlinQueryBase> filterTraversal)
        {
            var filtered = Continue(filterTraversal);

            return filtered.IsIdentity()
                ? this
                : filtered.IsNone()
                    ? None()
                    : Where(filtered.ToTraversal());
        }

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> Where(Traversal traversal)
        {
            traversal = traversal.RewriteForWhereContext();

            return traversal.Count > 0 && traversal.All(x => x is IIsOptimizableInWhere)
                ? AddSteps(traversal)
                : AddStep(new WhereTraversalStep(traversal));
        }

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> Where(Expression<Func<TElement, bool>> expression) => Where((Expression)expression);

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> Where(Expression expression)
        {
            try
            {
                switch (expression)
                {
                    case ConstantExpression constantExpression when constantExpression.GetValue() is bool value:
                    {
                        return value
                            ? this
                            : None();
                    }
                    case LambdaExpression lambdaExpression:
                    {
                        return Where(lambdaExpression.Body);
                    }
                    case UnaryExpression { NodeType: ExpressionType.Not } unaryExpression:
                    {
                        return Not(_ => _.Where(unaryExpression.Operand));
                    }
                    case BinaryExpression { NodeType: ExpressionType.OrElse } binary:
                    {
                        return Or(
                            Continue(__ => __.Where(binary.Left)),
                            Continue(__ => __.Where(binary.Right)));
                    }
                    case BinaryExpression { NodeType: ExpressionType.AndAlso } binary:
                    {
                        return this
                            .Where(binary.Left)
                            .Where(binary.Right);
                    }
                }

                if (expression.TryToGremlinExpression(Environment.Model) is { } gremlinExpression)
                {
                    return gremlinExpression.Equals(GremlinExpression.True)
                        ? this
                        : gremlinExpression.Equals(GremlinExpression.False)
                            ? None()
                            : AddSteps(Where(gremlinExpression));
                }
            }
            catch (ExpressionNotSupportedException ex)
            {
                throw new ExpressionNotSupportedException(expression, ex);
            }

            throw new ExpressionNotSupportedException(expression);
        }

        private GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> Where<TProjection>(Expression<Func<TElement, TProjection>> predicate, Func<IGremlinQueryBase<TProjection>, IGremlinQueryBase> propertyTraversal)
        {
            return predicate.TryGetReferredParameter() is not null && predicate.Body is MemberExpression memberExpression
                ? AddStep(new HasTraversalStep(GetKey(memberExpression), Cast<TProjection>().Continue(propertyTraversal).ToTraversal()))
                : throw new ExpressionNotSupportedException(predicate);
        }

        private IEnumerable<Step> Where(GremlinExpression gremlinExpression)
        {
            return Where(
                gremlinExpression.Left,
                gremlinExpression.LeftWellKnownMember,
                gremlinExpression.Semantics,
                gremlinExpression.Right);
        }

        private IEnumerable<Step> Where(ExpressionFragment left, WellKnownMember? leftWellKnownMember, ExpressionSemantics semantics, ExpressionFragment right)
        {
            if (right.Type == ExpressionFragmentType.Constant)
            {
                var maybeEffectivePredicate = Environment.Options
                    .GetValue(PFactory.PFactoryOption)
                    .TryGetP(semantics, right.GetValue(), Environment)
                    ?.WorkaroundLimitations(Environment);

                if (maybeEffectivePredicate is { } effectivePredicate)
                { 
                    if (left.Type == ExpressionFragmentType.Parameter)
                    {
                        switch (left.Expression)
                        {
                            case MemberExpression leftMemberExpression:
                            {
                                var leftMemberExpressionExpression = leftMemberExpression.Expression?.Strip();

                                if (leftMemberExpressionExpression is ParameterExpression parameterExpression)
                                {
                                    if (leftWellKnownMember == WellKnownMember.ArrayLength)
                                    {
                                        if (Environment.GetCache().ModelTypes.Contains(parameterExpression.Type))
                                        {
                                            if (GetKey(leftMemberExpression).RawKey is string stringKey)
                                            {
                                                if (!Environment.GetCache().FastNativeTypes.ContainsKey(leftMemberExpression.Type))
                                                {
                                                    yield return new WhereTraversalStep(ImmutableArray.Create<Step>(
                                                        new PropertiesStep(ImmutableArray.Create(stringKey)),
                                                        CountStep.Global,
                                                        new IsStep(effectivePredicate)));

                                                    yield break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            yield return new WhereTraversalStep(ImmutableArray.Create<Step>(
                                                new SelectKeysStep(
                                                    ImmutableArray.Create(GetKey(leftMemberExpression))),
                                                CountStep.Local,
                                                new IsStep(effectivePredicate)));

                                            yield break;
                                        }
                                        
                                        break;
                                    }
                                }
                                else if (leftMemberExpressionExpression is MemberExpression leftLeftMemberExpression)
                                {
                                    // x => x.Name.Value == P.xy(...)
                                    if (leftWellKnownMember == WellKnownMember.PropertyValue)
                                        leftMemberExpression = leftLeftMemberExpression;
                                }
                                else
                                    break;

                                // x => x.Name == P.xy(...)
                                if (right.GetValue() is StepLabel)
                                {
                                    if (right.Expression is MemberExpression memberExpression)
                                    {
                                        yield return new WherePredicateStep(effectivePredicate);
                                        yield return new WherePredicateStep.ByMemberStep(GetKey(leftMemberExpression));

                                        if (memberExpression.Member != leftMemberExpression.Member)
                                            yield return new WherePredicateStep.ByMemberStep(GetKey(memberExpression));

                                        yield break;
                                    }

                                    yield return new HasTraversalStep(
                                        GetKey(leftMemberExpression),
                                        this
                                            .Continue(__ => __
                                                .AddStep(new WherePredicateStep(effectivePredicate)))
                                            .ToTraversal());

                                    yield break;
                                }

                                yield return effectivePredicate.EqualsConstant(false)
                                    ? NoneStep.Instance
                                    : new HasPredicateStep(GetKey(leftMemberExpression), effectivePredicate);

                                yield break;
                            }
                            case ParameterExpression parameterExpression:
                            {
                                switch (leftWellKnownMember)
                                {
                                    // x => x.Value == P.xy(...)
                                    case WellKnownMember.PropertyValue when right.GetValue() is not StepLabel:
                                    {
                                        yield return new HasValueStep(effectivePredicate);
                                        yield break;
                                    }
                                    case WellKnownMember.PropertyKey:
                                    {
                                        yield return new WhereTraversalStep(new Traversal(this
                                            .Where(
                                                ExpressionFragment.Create(parameterExpression, Environment.Model),
                                                default,
                                                semantics,
                                                right)
                                            .Prepend(KeyStep.Instance)));

                                        yield break;
                                    }
                                    case WellKnownMember.VertexPropertyLabel when right.GetValue() is StepLabel:
                                    {
                                        yield return new WhereTraversalStep(new Traversal(this
                                            .Where(
                                                ExpressionFragment.Create(parameterExpression, Environment.Model),
                                                default,
                                                semantics,
                                                right)
                                            .Prepend(LabelStep.Instance)));

                                        yield break;
                                    }
                                    case WellKnownMember.VertexPropertyLabel:
                                    {
                                        yield return new HasKeyStep(effectivePredicate);
                                        yield break;
                                    }
                                }

                                // x => x == P.xy(...)
                                if (right.GetValue() is StepLabel)
                                {
                                    yield return new WherePredicateStep(effectivePredicate);

                                    if (right.Expression is MemberExpression memberExpression)
                                        yield return new WherePredicateStep.ByMemberStep(GetKey(memberExpression));

                                    yield break;
                                }

                                if (effectivePredicate.EqualsConstant(false))
                                    yield return NoneStep.Instance;
                                else if (!effectivePredicate.EqualsConstant(true))
                                    yield return new IsStep(effectivePredicate);

                                yield break;
                            }
                            case MethodCallExpression methodCallExpression:
                            {
                                var targetExpression = methodCallExpression.Object?.Strip();

                                if (targetExpression != null && typeof(IDictionary<string, object>).IsAssignableFrom(targetExpression.Type) && methodCallExpression.Method.Name == "get_Item")
                                {
                                    if (methodCallExpression.Arguments[0].Strip()!.GetValue() is string key)
                                    {
                                        yield return new HasPredicateStep((Key) key, effectivePredicate);
                                        yield break;
                                    }
                                }

                                break;
                            }
                        }
                    }
                    else if (left.Type == ExpressionFragmentType.Constant && left.GetValue() is StepLabel leftStepLabel && right.GetValue() is StepLabel)
                    {
                        yield return new WhereStepLabelAndPredicateStep(leftStepLabel, effectivePredicate);

                        if (left.Expression is MemberExpression leftStepValueExpression)
                            yield return new WherePredicateStep.ByMemberStep(GetKey(leftStepValueExpression));

                        if (right.Expression is MemberExpression rightStepValueExpression)
                            yield return new WherePredicateStep.ByMemberStep(GetKey(rightStepValueExpression));

                        yield break;
                    }
                }
            }
            else if (right.Type == ExpressionFragmentType.Parameter)
            {
                if (left.Type == ExpressionFragmentType.Parameter)
                {
                    if (left.Expression is MemberExpression && right.Expression is MemberExpression rightMember)
                    {
                        var newStepLabel = new StepLabel<TElement>();

                        yield return new AsStep(newStepLabel);

                        var subSteps = Where(
                            left,
                            default,
                            semantics,
                            ExpressionFragment.StepLabel(newStepLabel, rightMember));

                        foreach (var step in subSteps)
                        {
                            yield return step;
                        }

                        yield break;
                    }
                }
            }

            throw new ExpressionNotSupportedException();
        }
    }
}
