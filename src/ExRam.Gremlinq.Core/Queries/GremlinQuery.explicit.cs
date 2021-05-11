﻿// ReSharper disable ArrangeThisQualifier
// ReSharper disable CoVariantArrayConversion
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ExRam.Gremlinq.Core.GraphElements;

namespace ExRam.Gremlinq.Core
{
    internal partial class GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery> :
        IGremlinQueryAdmin,

        IGremlinQuerySource,
        IGremlinQuery<TElement>,

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>,

        IElementGremlinQuery<TElement>,

        IValueGremlinQuery<TElement>,
        IValueTupleGremlinQuery<TElement>,

        IEdgeOrVertexGremlinQuery<TElement>,
        IVertexGremlinQuery<TElement>,
        IEdgeGremlinQuery<TElement>,
        IInOrOutEdgeGremlinQuery<TElement, TOutVertex>,
        IBothEdgeGremlinQuery<TElement, TOutVertex, TInVertex>,

        IInEdgeGremlinQuery<TElement, TInVertex>,
        IOutEdgeGremlinQuery<TElement, TOutVertex>,

        IVertexPropertyGremlinQuery<TElement, TScalar>,
        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>,
        IPropertyGremlinQuery<TElement>
    {
        TFoldedQuery IArrayGremlinQueryBase<TElement, TScalar, TFoldedQuery>.MeanLocal() => MeanLocal(typeof(TFoldedQuery)).ChangeQueryType<TFoldedQuery>();

        TFoldedQuery IArrayGremlinQueryBase<TElement, TScalar, TFoldedQuery>.Unfold() => Unfold<TFoldedQuery>();

        IGremlinQuery<TElement> IArrayGremlinQueryBase<TElement, TScalar>.Lower() => this;

        IValueGremlinQuery<object> IArrayGremlinQueryBase.Unfold() => Unfold<IValueGremlinQuery<object>>();

        IValueTupleGremlinQuery<TElement> IGremlinQueryBase<TElement>.ForceValueTuple() => ChangeQueryType<IValueTupleGremlinQuery<TElement>>();

        IArrayGremlinQuery<TElement[], TElement, IGremlinQueryBase<TElement>> IGremlinQueryBase<TElement>.ForceArray() => ChangeQueryType<IArrayGremlinQuery<TElement[], TElement, IGremlinQueryBase<TElement>>>();

        IGremlinQuery<TScalar[]> IArrayGremlinQueryBase<TScalar>.Lower() => Cast<TScalar[]>(typeof(IGremlinQuery<TScalar[]>));

        IValueGremlinQuery<object[]> IArrayGremlinQueryBase.Lower() => Cast<object[]>(typeof(IValueGremlinQuery<object[]>));

        IGremlinQuery<TElement> IArrayGremlinQueryBase<TElement, TScalar, TFoldedQuery>.Lower() => this;

        TFoldedQuery IArrayGremlinQueryBase<TElement, TScalar, TFoldedQuery>.SumLocal() => SumLocal(typeof(TFoldedQuery)).ChangeQueryType<TFoldedQuery>();

        TFoldedQuery IArrayGremlinQueryBase<TElement, TScalar, TFoldedQuery>.MinLocal() => MinLocal(typeof(TFoldedQuery)).ChangeQueryType<TFoldedQuery>();

        TFoldedQuery IArrayGremlinQueryBase<TElement, TScalar, TFoldedQuery>.MaxLocal() => MaxLocal(typeof(TFoldedQuery)).ChangeQueryType<TFoldedQuery>();

        IValueGremlinQuery<TElement> IValueGremlinQueryBase<TElement>.Sum() => SumGlobal(typeof(IValueGremlinQuery<TElement>));

        IValueGremlinQuery<object> IValueGremlinQueryBase<TElement>.SumLocal() => SumLocal(typeof(IValueGremlinQuery<object>));

        IValueGremlinQuery<TElement> IValueGremlinQueryBase<TElement>.Min() => MinGlobal(typeof(IValueGremlinQuery<TElement>));

        IValueGremlinQuery<object> IValueGremlinQueryBase<TElement>.MinLocal() => MinLocal(typeof(IValueGremlinQuery<object>));

        IValueGremlinQuery<TElement> IValueGremlinQueryBase<TElement>.Max() => MaxGlobal(typeof(IValueGremlinQuery<TElement>));

        IValueGremlinQuery<object> IValueGremlinQueryBase<TElement>.MaxLocal() => MaxLocal(typeof(IValueGremlinQuery<object>));

        IValueGremlinQuery<TElement> IValueGremlinQueryBase<TElement>.Mean() => MeanGlobal(typeof(IValueGremlinQuery<TElement>));

        IValueGremlinQuery<object> IValueGremlinQueryBase<TElement>.MeanLocal() => MeanLocal(typeof(IValueGremlinQuery<object>));

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Mute() => Mute();

        IBothEdgeGremlinQuery<TElement, TNewOutVertex, TInVertex> IInEdgeGremlinQueryBase<TElement, TInVertex>.From<TNewOutVertex>(Func<IVertexGremlinQuery<TInVertex>, IVertexGremlinQueryBase<TNewOutVertex>> fromVertexTraversal) => AddStep<TElement, TNewOutVertex, TInVertex, object, object, object>(new AddEStep.FromTraversalStep(Cast<TInVertex>(Semantics).Continue(fromVertexTraversal).ToTraversal()), typeof(IBothEdgeGremlinQuery<TElement, TNewOutVertex, TInVertex>));

        IVertexGremlinQuery<TInVertex> IInEdgeGremlinQueryBase<TElement, TInVertex>.InV() => InV<TInVertex>(typeof(IVertexGremlinQuery<TInVertex>));

        IEdgeOrVertexGremlinQuery<TElement> IBothEdgeGremlinQueryBase<TElement, TOutVertex, TInVertex>.Lower() => this;

        IEdgeGremlinQuery<TElement> IInEdgeGremlinQueryBase<TElement, TInVertex>.Lower() => this;

        IEdgeGremlinQuery<TElement> IOutEdgeGremlinQueryBase<TElement, TOutVertex>.Lower() => this;

        IVertexGremlinQuery<TOutVertex> IOutEdgeGremlinQueryBase<TElement, TOutVertex>.OutV() => AddStepWithObjectTypes<TOutVertex>(OutVStep.Instance, typeof(IVertexGremlinQuery<TOutVertex>));

        IBothEdgeGremlinQuery<TElement, TOutVertex, TNewInVertex> IOutEdgeGremlinQueryBase<TElement, TOutVertex>.To<TNewInVertex>(Func<IVertexGremlinQuery<TOutVertex>, IVertexGremlinQueryBase<TNewInVertex>> toVertexTraversal) => AddStep<TElement, TOutVertex, TNewInVertex, object, object, object>(new AddEStep.ToTraversalStep(Cast<TOutVertex>(Semantics).Continue(toVertexTraversal).ToTraversal()), typeof(IBothEdgeGremlinQuery<TElement, TOutVertex, TNewInVertex>));

        IEdgeOrVertexGremlinQuery<object> IBothEdgeGremlinQueryBase.Lower() => Cast<object>(typeof(IEdgeOrVertexGremlinQuery<object>));

        IEdgeGremlinQuery<object> IInEdgeGremlinQueryBase.Lower() => Cast<object>(typeof(IEdgeGremlinQuery<object>));

        IEdgeGremlinQuery<object> IOutEdgeGremlinQueryBase.Lower() => Cast<object>(typeof(IEdgeGremlinQuery<object>));

        IBothEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IBothEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Mute() => Mute();

        IVertexGremlinQuery<object> IEdgeGremlinQueryBase.BothV() => BothV<object>(typeof(IVertexGremlinQuery<object>));

        IVertexGremlinQuery<TVertex> IEdgeGremlinQueryBase.BothV<TVertex>() => ((IEdgeGremlinQueryBase)this)
            .BothV()
            .OfType<TVertex>();

        IOutEdgeGremlinQuery<TElement, TNewOutVertex> IEdgeGremlinQueryBase<TElement>.From<TNewOutVertex>(StepLabel<TNewOutVertex> stepLabel) => AddStep<TElement, TNewOutVertex, object, object, object, object>(new AddEStep.FromLabelStep(stepLabel), typeof(IOutEdgeGremlinQuery<TElement, TNewOutVertex>));

        IEdgeOrVertexGremlinQuery<TElement> IEdgeGremlinQueryBase<TElement>.Lower() => this;

        IOutEdgeGremlinQuery<TElement, TNewOutVertex> IEdgeGremlinQueryBase<TElement>.From<TNewOutVertex>(Func<IVertexGremlinQueryBase, IVertexGremlinQueryBase<TNewOutVertex>> fromVertexTraversal) => AddStep<TElement, TNewOutVertex, object, object, object, object>(new AddEStep.FromTraversalStep(Continue(fromVertexTraversal).ToTraversal()), typeof(IOutEdgeGremlinQuery<TElement, TNewOutVertex>));

        IVertexGremlinQuery<object> IEdgeGremlinQueryBase.InV() => InV<object>(typeof(IVertexGremlinQuery<object>));

        IVertexGremlinQuery<TVertex> IEdgeGremlinQueryBase.InV<TVertex>() => ((IEdgeGremlinQueryBase)this)
            .InV()
            .OfType<TVertex>();

        IEdgeOrVertexGremlinQuery<object> IEdgeGremlinQueryBase.Lower() => Cast<object>(typeof(IEdgeOrVertexGremlinQuery<object>));

        IVertexGremlinQuery<object> IEdgeGremlinQueryBase.OtherV() => OtherV<object>(typeof(IVertexGremlinQuery<object>));

        IVertexGremlinQuery<TVertex> IEdgeGremlinQueryBase.OtherV<TVertex>() => ((IEdgeGremlinQueryBase)this)
            .OtherV()
            .OfType<TVertex>();

        IVertexGremlinQuery<object> IEdgeGremlinQueryBase.OutV() => OutV<object>(typeof(IVertexGremlinQuery<object>));

        IVertexGremlinQuery<TVertex> IEdgeGremlinQueryBase.OutV<TVertex>() => ((IEdgeGremlinQueryBase)this)
            .OutV()
            .OfType<TVertex>();

        IPropertyGremlinQuery<Property<TValue>> IEdgeGremlinQueryBase<TElement>.Properties<TValue>(params Expression<Func<TElement, TValue>>[] projections) => Properties<Property<TValue>, TValue, object>(typeof(IPropertyGremlinQuery<Property<TValue>>), projections);

        IPropertyGremlinQuery<Property<TValue>> IEdgeGremlinQueryBase<TElement>.Properties<TValue>(params Expression<Func<TElement, Property<TValue>>>[] projections) => Properties<Property<TValue>, TValue, object>(typeof(IPropertyGremlinQuery<Property<TValue>>), projections);

        IPropertyGremlinQuery<Property<object>> IEdgeGremlinQueryBase<TElement>.Properties(params Expression<Func<TElement, Property<object>>>[] projections) => Properties<Property<object>, object, object>(typeof(IPropertyGremlinQuery<Property<object>>), projections);

        IPropertyGremlinQuery<Property<object>> IEdgeGremlinQueryBase.Properties() => Properties<Property<object>, object, object>(Array.Empty<string>(), typeof(IPropertyGremlinQuery<Property<object>>));

        IPropertyGremlinQuery<Property<TValue>> IEdgeGremlinQueryBase.Properties<TValue>() => Properties<Property<TValue>, object, object>(Array.Empty<string>(), typeof(IPropertyGremlinQuery<Property<TValue>>));

        IInEdgeGremlinQuery<TElement, TNewInVertex> IEdgeGremlinQueryBase<TElement>.To<TNewInVertex>(StepLabel<TNewInVertex> stepLabel) => To<TElement, object, TNewInVertex>(stepLabel);

        IInEdgeGremlinQuery<TElement, TNewInVertex> IEdgeGremlinQueryBase<TElement>.To<TNewInVertex>(Func<IVertexGremlinQueryBase, IVertexGremlinQueryBase<TNewInVertex>> toVertexTraversal) => AddStep<TElement, object, TNewInVertex, object, object, object>(new AddEStep.ToTraversalStep(Continue(toVertexTraversal).ToTraversal()), typeof(IInEdgeGremlinQuery<TElement, TNewInVertex>));

        IValueGremlinQuery<TValue> IEdgeGremlinQueryBase<TElement>.Values<TValue>(params Expression<Func<TElement, Property<TValue>>>[] projections) => ValuesForProjections<TValue>(projections, typeof(IValueGremlinQuery<TValue>));

        IValueGremlinQuery<object> IEdgeGremlinQueryBase<TElement>.Values(params Expression<Func<TElement, Property<object>>>[] projections) => ValuesForProjections<object>(projections, typeof(IValueGremlinQuery<object>));

        IEdgeGremlinQuery<TElement> IEdgeGremlinQueryBase<TElement>.Update(TElement element) => AddOrUpdate(element, false);

        IValueGremlinQuery<TTarget> IEdgeGremlinQueryBase<TElement>.Values<TTarget>(params Expression<Func<TElement, TTarget>>[] projections) => ValuesForProjections<TTarget>(projections, typeof(IValueGremlinQuery<TTarget>));

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Mute() => Mute();

        IElementGremlinQuery<TElement> IEdgeOrVertexGremlinQueryBase<TElement>.Lower() => this;

        IElementGremlinQuery<object> IEdgeOrVertexGremlinQueryBase.Lower() => Cast<object>(typeof(IElementGremlinQuery<object>));

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Mute() => Mute();

        IValueGremlinQuery<object> IElementGremlinQueryBase.Id() => Id(typeof(IValueGremlinQuery<object>));

        IValueGremlinQuery<string> IElementGremlinQueryBase.Label() => Label(typeof(IValueGremlinQuery<string>));

        IValueGremlinQuery<IDictionary<string, TTarget>> IElementGremlinQueryBase.ValueMap<TTarget>() => ValueMap<IDictionary<string, TTarget>>(ImmutableArray<string>.Empty, typeof(IValueGremlinQuery<IDictionary<string, TTarget>>));

        IValueGremlinQuery<IDictionary<string, object>> IElementGremlinQueryBase.ValueMap() => ValueMap<IDictionary<string, object>>(ImmutableArray<string>.Empty, typeof(IValueGremlinQuery<IDictionary<string, object>>));

        IValueGremlinQuery<TValue> IElementGremlinQueryBase.Values<TValue>() => ValuesForKeys<TValue>(Array.Empty<Key>(), typeof(IValueGremlinQuery<TValue>));

        IValueGremlinQuery<object> IElementGremlinQueryBase.Values() => ValuesForKeys<object>(Array.Empty<Key>(), typeof(IValueGremlinQuery<object>));

        IElementGremlinQuery<TElement> IElementGremlinQueryBase<TElement>.Update(TElement element) => AddOrUpdate(element, false);

        IValueGremlinQuery<TTarget> IElementGremlinQueryBase<TElement>.Values<TTarget>(params Expression<Func<TElement, TTarget>>[] projections) => ValuesForProjections<TTarget>(projections, typeof(IValueGremlinQuery<TTarget>));

        IValueGremlinQuery<TTarget> IElementGremlinQueryBase<TElement>.Values<TTarget>(params Expression<Func<TElement, TTarget[]>>[] projections) => ValuesForProjections<TTarget>(projections, typeof(IValueGremlinQuery<TTarget>));

        IValueGremlinQuery<IDictionary<string, TTarget>> IElementGremlinQueryBase<TElement>.ValueMap<TTarget>(params Expression<Func<TElement, TTarget>>[] keys) => ValueMap<IDictionary<string, TTarget>>(keys, typeof(IValueGremlinQuery<IDictionary<string, TTarget>>));

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Mute() => Mute();

        IGremlinQueryAdmin IGremlinQueryBase.AsAdmin() => this;

        IValueGremlinQuery<TValue> IGremlinQueryBase.Constant<TValue>(TValue constant) => AddStepWithObjectTypes<TValue>(new ConstantStep(constant!), typeof(IValueGremlinQuery<TValue>));
        
        string IGremlinQueryBase.Debug(GroovyFormatting groovyFormatting, bool indented) => Debug(groovyFormatting, indented);

        IValueGremlinQuery<long> IGremlinQueryBase.Count() => AddStepWithObjectTypes<long>(CountStep.Global, typeof(IValueGremlinQuery<long>));

        IValueGremlinQuery<long> IGremlinQueryBase.CountLocal() => AddStepWithObjectTypes<long>(CountStep.Local, typeof(IValueGremlinQuery<long>));

        IValueGremlinQuery<string> IGremlinQueryBase.Explain() => AddStepWithObjectTypes<string>(ExplainStep.Instance, typeof(IValueGremlinQuery<string>));

        TaskAwaiter IGremlinQueryBase.GetAwaiter() => ((Task)((IGremlinQuery<TElement>)this).ToAsyncEnumerable().LastOrDefaultAsync().AsTask()).GetAwaiter();

        IGremlinQuery<TElement> IGremlinQueryBase<TElement>.ForceBase() => ChangeQueryType<IGremlinQuery<TElement>>();

        IElementGremlinQuery<TElement> IGremlinQueryBase<TElement>.ForceElement() => ChangeQueryType<IElementGremlinQuery<TElement>>();

        IVertexGremlinQuery<TElement> IGremlinQueryBase<TElement>.ForceVertex() => ChangeQueryType<IVertexGremlinQuery<TElement>>();

        IVertexPropertyGremlinQuery<TElement, TNewValue> IGremlinQueryBase<TElement>.ForceVertexProperty<TNewValue>() => ChangeQueryType<IVertexPropertyGremlinQuery<TElement, TNewValue>>();

        IVertexPropertyGremlinQuery<TElement, TNewValue, TNewMeta> IGremlinQueryBase<TElement>.ForceVertexProperty<TNewValue, TNewMeta>() => ChangeQueryType<IVertexPropertyGremlinQuery<TElement, TNewValue, TNewMeta>>();

        IPropertyGremlinQuery<TElement> IGremlinQueryBase<TElement>.ForceProperty() => ChangeQueryType<IPropertyGremlinQuery<TElement>>();

        IEdgeGremlinQuery<TElement> IGremlinQueryBase<TElement>.ForceEdge() => ChangeQueryType<IEdgeGremlinQuery<TElement>>();

        IInEdgeGremlinQuery<TElement, TNewInVertex> IGremlinQueryBase<TElement>.ForceInEdge<TNewInVertex>() => ChangeQueryType<IInEdgeGremlinQuery<TElement, TNewInVertex>>();

        IOutEdgeGremlinQuery<TElement, TNewOutVertex> IGremlinQueryBase<TElement>.ForceOutEdge<TNewOutVertex>() => ChangeQueryType<IOutEdgeGremlinQuery<TElement, TNewOutVertex>>();

        IBothEdgeGremlinQuery<TElement, TNewOutVertex, TNewInVertex> IGremlinQueryBase<TElement>.ForceBothEdge<TNewOutVertex, TNewInVertex>() => ChangeQueryType<IBothEdgeGremlinQuery<TElement, TNewOutVertex, TNewInVertex>>();

        IValueGremlinQuery<TElement> IGremlinQueryBase<TElement>.ForceValue() => ChangeQueryType<IValueGremlinQuery<TElement>>();

        GremlinQueryAwaiter<TElement> IGremlinQueryBase<TElement>.GetAwaiter() => new((this).ToArrayAsync().AsTask().GetAwaiter());

        IAsyncEnumerable<TElement> IGremlinQueryBase<TElement>.ToAsyncEnumerable() => Environment.Executor
            .Execute(
                Environment.Serializer
                    .Serialize(this),
                Environment)
            .SelectMany(executionResult => Environment.Deserializer
                .Deserialize<TElement>(executionResult, Environment));

        IValueGremlinQuery<Path> IGremlinQueryBase.Path() => AddStepWithObjectTypes<Path>(PathStep.Instance, typeof(IValueGremlinQuery<Path>));

        IValueGremlinQuery<string> IGremlinQueryBase.Profile() => AddStepWithObjectTypes<string>(ProfileStep.Instance, typeof(IValueGremlinQuery<string>));

        TQuery IGremlinQueryBase.Select<TQuery, TStepElement>(StepLabel<TQuery, TStepElement> label) => Select(label).ChangeQueryType<TQuery>(StepLabelSemantics[label]);

        IArrayGremlinQuery<TNewElement, TNewScalar, TQuery> IGremlinQueryBase.Cap<TNewElement, TNewScalar, TQuery>(StepLabel<IArrayGremlinQuery<TNewElement, TNewScalar, TQuery>, TNewElement> label) => Cap(label, typeof(IArrayGremlinQuery<TNewElement, TNewScalar, TQuery>));

        IValueGremlinQuery<TLabelledElement> IGremlinQueryBase.Select<TLabelledElement>(StepLabel<TLabelledElement> label) => Select(label);

        IGremlinQuery<TElement> IGremlinQueryBase<TElement>.Lower() => this;

        IGremlinQuery<object> IGremlinQueryBase.Lower() => Cast<object>(typeof(IGremlinQuery<object>));

        IValueGremlinQuery<object> IGremlinQueryBase.Drop() => Drop();

        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Mute() => Mute();

        QuerySemantics IGremlinQueryAdmin.Semantics => Semantics;

        TTargetQuery IGremlinQueryAdmin.ConfigureSteps<TTargetQuery>(Func<StepStack, StepStack> transformation) => ConfigureSteps<TElement>(transformation).ChangeQueryType<TTargetQuery>(Semantics);

        TTargetQuery IGremlinQueryAdmin.AddStep<TTargetQuery>(Step step) => AddStep(step).ChangeQueryType<TTargetQuery>();

        TTargetQuery IGremlinQueryAdmin.ChangeQueryType<TTargetQuery>() => ChangeQueryType<TTargetQuery>();

        IGremlinQuerySource IGremlinQueryAdmin.GetSource() => GremlinQuery.Create(Environment);

        StepStack IGremlinQueryAdmin.Steps => Steps;

        IGremlinQueryEnvironment IGremlinQueryAdmin.Environment => Environment;

        Traversal IGremlinQueryAdmin.ToTraversal()
        {
            var steps = Steps;
            var querySize = Math.Max(1, steps.Count);
            var projection = ImmutableArray<Step>.Empty;

            if ((Flags & QueryFlags.SurfaceVisible) == QueryFlags.SurfaceVisible)
            {
                if (Semantics.IsVertex)
                {
                    projection = Environment.Options.GetValue(Environment.FeatureSet.Supports(VertexFeatures.MetaProperties)
                        ? GremlinqOption.VertexProjectionSteps
                        : GremlinqOption.VertexProjectionWithoutMetaPropertiesSteps);
                }
                else if (Semantics.IsEdge)
                {
                    projection = Environment.Options.GetValue(GremlinqOption.EdgeProjectionSteps);
                }
            }

            var ret = new Step[querySize + projection.Length];

            if (steps.IsEmpty)
                ret[0] = IdentityStep.Instance;
            else
                steps.CopyTo(ret);

            projection.CopyTo(ret, querySize);

            return new Traversal(ret, true);
        }

        Type IGremlinQueryAdmin.ElementType { get => typeof(TElement); }

        IEdgeGremlinQuery<TEdge> IStartGremlinQuery.AddE<TEdge>(TEdge edge) => AddE(edge, typeof(IEdgeGremlinQuery<TEdge>));

        IValueGremlinQuery<TScalar> IArrayGremlinQueryBase<TScalar>.Unfold() => Unfold<IValueGremlinQuery<TScalar>>();

        IVertexGremlinQuery<TVertex> IStartGremlinQuery.AddV<TVertex>(TVertex vertex) => AddV(vertex, typeof(IVertexGremlinQuery<TVertex>));

        IValueGremlinQuery<TNewElement> IStartGremlinQuery.Inject<TNewElement>(params TNewElement[] elements) => Inject(elements, typeof(IValueGremlinQuery<TNewElement>));

        IVertexGremlinQuery<object> IStartGremlinQuery.V(params object[] ids) => AddStepWithObjectTypes<object>(new VStep(ids.ToImmutableArray()), typeof(IVertexGremlinQuery<object>));

        IVertexGremlinQuery<TNewVertex> IStartGremlinQuery.ReplaceV<TNewVertex>(TNewVertex vertex) => ((IStartGremlinQuery)this).V<TNewVertex>(vertex!.GetId(Environment)).Update(vertex);

        IEdgeGremlinQuery<TEdge> IStartGremlinQuery.AddE<TEdge>() => AddE(new TEdge(), typeof(IEdgeGremlinQuery<TEdge>));

        IVertexGremlinQuery<TVertex> IStartGremlinQuery.AddV<TVertex>() => AddV(new TVertex(), typeof(IVertexGremlinQuery<TVertex>));

        IVertexGremlinQuery<TVertex> IStartGremlinQuery.V<TVertex>(params object[] ids) => ((IStartGremlinQuery)this).V(ids).OfType<TVertex>();

        IGremlinQueryEnvironment IGremlinQuerySource.Environment => Environment;

        IEdgeGremlinQuery<object> IGremlinQuerySource.E(params object[] ids) => AddStepWithObjectTypes<object>(new EStep(ids.ToImmutableArray()), typeof(IEdgeGremlinQuery<object>));

        IEdgeGremlinQuery<TEdge> IGremlinQuerySource.E<TEdge>(params object[] ids) => ((IGremlinQuerySource)this).E(ids).OfType<TEdge>();

        IEdgeGremlinQuery<TNewEdge> IGremlinQuerySource.ReplaceE<TNewEdge>(TNewEdge edge) => ((IGremlinQuerySource)this).E<TNewEdge>(edge!.GetId(Environment)).Update(edge);

        IGremlinQuerySource IConfigurableGremlinQuerySource.ConfigureEnvironment(Func<IGremlinQueryEnvironment, IGremlinQueryEnvironment> transformation) => ConfigureEnvironment(transformation);

        IGremlinQuerySource IGremlinQuerySource.WithoutStrategies(params Type[] strategyTypes) => AddStep(new WithoutStrategiesStep(strategyTypes.ToImmutableArray()));

        IGremlinQuerySource IGremlinQuerySource.WithSideEffect<TSideEffect>(StepLabel<TSideEffect> label, TSideEffect value) => AddStep(new WithSideEffectStep(label, value!));

        TQuery IGremlinQuerySource.WithSideEffect<TSideEffect, TQuery>(TSideEffect value, Func<IGremlinQuerySource, StepLabel<TSideEffect>, TQuery> continuation)
        {
            var stepLabel = new StepLabel<TSideEffect>();

            return continuation(
                ((IGremlinQuerySource)this).WithSideEffect(stepLabel, value),
                stepLabel);
        }

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Mute() => Mute();

        IBothEdgeGremlinQuery<TElement, TTargetVertex, TOutVertex> IInOrOutEdgeGremlinQueryBase<TElement, TOutVertex>.From<TTargetVertex>(StepLabel<TTargetVertex> stepLabel) => AddStep<TElement, TTargetVertex, TOutVertex, object, object, object>(new AddEStep.FromLabelStep(stepLabel), typeof(IBothEdgeGremlinQuery<TElement, TTargetVertex, TOutVertex>));

        IBothEdgeGremlinQuery<TElement, TTargetVertex, TOutVertex> IInOrOutEdgeGremlinQueryBase<TElement, TOutVertex>.From<TTargetVertex>(Func<IVertexGremlinQuery<TOutVertex>, IVertexGremlinQueryBase<TTargetVertex>> fromVertexTraversal) => AddStep<TElement, TTargetVertex, TOutVertex, object, object, object>(new AddEStep.FromTraversalStep(Cast<TOutVertex>(typeof(IVertexGremlinQuery<TOutVertex>)).Continue(fromVertexTraversal).ToTraversal()), typeof(IBothEdgeGremlinQuery<TElement, TTargetVertex, TOutVertex>));

        IBothEdgeGremlinQuery<TElement, TOutVertex, TTargetVertex> IInOrOutEdgeGremlinQueryBase<TElement, TOutVertex>.To<TTargetVertex>(StepLabel<TTargetVertex> stepLabel) => To<TElement, TOutVertex, TTargetVertex>(stepLabel);

        IBothEdgeGremlinQuery<TElement, TOutVertex, TTargetVertex> IInOrOutEdgeGremlinQueryBase<TElement, TOutVertex>.To<TTargetVertex>(Func<IVertexGremlinQuery<TOutVertex>, IVertexGremlinQueryBase<TTargetVertex>> toVertexTraversal) => AddStep<TElement, TOutVertex, TTargetVertex, object, object, object>(new AddEStep.ToTraversalStep(Cast<TOutVertex>(typeof(IVertexGremlinQuery<TOutVertex>)).Continue(toVertexTraversal).ToTraversal()), typeof(IBothEdgeGremlinQuery<TElement, TOutVertex, TTargetVertex>));

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Mute() => Mute();

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Mute() => Mute();

        IValueGremlinQuery<string> IPropertyGremlinQueryBase<TElement>.Key() => Key(typeof(IValueGremlinQuery<string>));

        IValueGremlinQuery<TValue> IPropertyGremlinQueryBase<TElement>.Value<TValue>() => Value<TValue>(typeof(IValueGremlinQuery<TValue>));

        IValueGremlinQuery<object> IPropertyGremlinQueryBase<TElement>.Value() => Value<object>(typeof(IValueGremlinQuery<object>));

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Mute() => Mute();

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Mute() => Mute();

        IEdgeOrVertexGremlinQuery<TElement> IVertexGremlinQueryBase<TElement>.Lower() => this;

        IEdgeOrVertexGremlinQuery<object> IVertexGremlinQueryBase.Lower() => Cast<object>(typeof(IEdgeOrVertexGremlinQuery<object>));

        IInOrOutEdgeGremlinQuery<TEdge, TElement> IVertexGremlinQueryBase<TElement>.AddE<TEdge>(TEdge edge) => AddE(edge, typeof(IInOrOutEdgeGremlinQuery<TEdge, TElement>));

        IInOrOutEdgeGremlinQuery<TEdge, TElement> IVertexGremlinQueryBase<TElement>.AddE<TEdge>() => AddE(new TEdge(), typeof(IInOrOutEdgeGremlinQuery<TEdge, TElement>));

        IVertexGremlinQuery<object> IVertexGremlinQueryBase.Both() => AddStepWithObjectTypes<object>(BothStep.NoLabels, typeof(IVertexGremlinQuery<object>));

        IVertexGremlinQuery<object> IVertexGremlinQueryBase.Both<TEdge>() => AddStepWithObjectTypes<object>(new BothStep(Environment.Model.EdgesModel.GetFilterLabelsOrDefault(typeof(TEdge), Environment.Options.GetValue(GremlinqOption.FilterLabelsVerbosity))), typeof(IVertexGremlinQuery<object>));

        IEdgeGremlinQuery<object> IVertexGremlinQueryBase.BothE() => AddStepWithObjectTypes<object>(BothEStep.NoLabels, typeof(IEdgeGremlinQuery<object>));

        IEdgeGremlinQuery<TEdge> IVertexGremlinQueryBase.BothE<TEdge>() => AddStepWithObjectTypes<TEdge>(new BothEStep(Environment.Model.EdgesModel.GetFilterLabelsOrDefault(typeof(TEdge), Environment.Options.GetValue(GremlinqOption.FilterLabelsVerbosity))), typeof(IEdgeGremlinQuery<TEdge>));

        IVertexGremlinQuery<object> IVertexGremlinQueryBase.In() => AddStepWithObjectTypes<object>(InStep.Empty, typeof(IVertexGremlinQuery<object>));

        IVertexGremlinQuery<object> IVertexGremlinQueryBase.In<TEdge>() => AddStepWithObjectTypes<object>(new InStep(Environment.Model.EdgesModel.GetFilterLabelsOrDefault(typeof(TEdge), Environment.Options.GetValue(GremlinqOption.FilterLabelsVerbosity))), typeof(IVertexGremlinQuery<object>));

        IEdgeGremlinQuery<object> IVertexGremlinQueryBase.InE() => AddStepWithObjectTypes<object>(InEStep.Empty, typeof(IEdgeGremlinQuery<object>));

        IEdgeGremlinQuery<TEdge> IVertexGremlinQueryBase.InE<TEdge>() => AddStepWithObjectTypes<TEdge>(new InEStep(Environment.Model.EdgesModel.GetFilterLabelsOrDefault(typeof(TEdge), Environment.Options.GetValue(GremlinqOption.FilterLabelsVerbosity))), typeof(IEdgeGremlinQuery<TEdge>));

        IInEdgeGremlinQuery<TEdge, TElement> IVertexGremlinQueryBase<TElement>.InE<TEdge>() => AddStep<TEdge, object, TElement, object, object, object>(new InEStep(Environment.Model.EdgesModel.GetFilterLabelsOrDefault(typeof(TEdge), Environment.Options.GetValue(GremlinqOption.FilterLabelsVerbosity))), typeof(IInEdgeGremlinQuery<TEdge, TElement>));

        IVertexGremlinQuery<object> IVertexGremlinQueryBase.Out() => AddStepWithObjectTypes<object>(OutStep.Empty, typeof(IVertexGremlinQuery<object>));

        IVertexGremlinQuery<object> IVertexGremlinQueryBase.Out<TEdge>() => AddStepWithObjectTypes<object>(new OutStep(Environment.Model.EdgesModel.GetFilterLabelsOrDefault(typeof(TEdge), Environment.Options.GetValue(GremlinqOption.FilterLabelsVerbosity))), typeof(IVertexGremlinQuery<object>));

        IEdgeGremlinQuery<TEdge> IVertexGremlinQueryBase.OutE<TEdge>() => AddStepWithObjectTypes<TEdge>(new OutEStep(Environment.Model.EdgesModel.GetFilterLabelsOrDefault(typeof(TEdge), Environment.Options.GetValue(GremlinqOption.FilterLabelsVerbosity))), typeof(IEdgeGremlinQuery<TEdge>));

        IEdgeGremlinQuery<object> IVertexGremlinQueryBase.OutE() => AddStepWithObjectTypes<object>(OutEStep.Empty, typeof(IEdgeGremlinQuery<object>));

        IOutEdgeGremlinQuery<TEdge, TElement> IVertexGremlinQueryBase<TElement>.OutE<TEdge>() => AddStep<TEdge, TElement, object, object, object, object>(new OutEStep(Environment.Model.EdgesModel.GetFilterLabelsOrDefault(typeof(TEdge), Environment.Options.GetValue(GremlinqOption.FilterLabelsVerbosity))), typeof(IOutEdgeGremlinQuery<TEdge, TElement>));

        IVertexPropertyGremlinQuery<VertexProperty<object>, object> IVertexGremlinQueryBase<TElement>.Properties() => Properties<VertexProperty<object>, object, object>(Array.Empty<string>(), typeof(IVertexPropertyGremlinQuery<VertexProperty<object>, object>));

        IVertexPropertyGremlinQuery<VertexProperty<TValue>, TValue> IVertexGremlinQueryBase<TElement>.Properties<TValue>(params Expression<Func<TElement, TValue>>[] projections) => VertexProperties<TValue>(projections, typeof(IVertexPropertyGremlinQuery<VertexProperty<TValue>, TValue>));

        IVertexPropertyGremlinQuery<VertexProperty<TValue>, TValue> IVertexGremlinQueryBase<TElement>.Properties<TValue>(params Expression<Func<TElement, TValue[]>>[] projections) => VertexProperties<TValue>(projections, typeof(IVertexPropertyGremlinQuery<VertexProperty<TValue>, TValue>));

        IVertexPropertyGremlinQuery<VertexProperty<TValue>, TValue> IVertexGremlinQueryBase<TElement>.Properties<TValue>(params Expression<Func<TElement, VertexProperty<TValue>>>[] projections) => VertexProperties<TValue>(projections, typeof(IVertexPropertyGremlinQuery<VertexProperty<TValue>, TValue>));

        IVertexPropertyGremlinQuery<VertexProperty<TValue, TNewMeta>, TValue, TNewMeta> IVertexGremlinQueryBase<TElement>.Properties<TValue, TNewMeta>(params Expression<Func<TElement, VertexProperty<TValue, TNewMeta>>>[] projections) => Properties<VertexProperty<TValue, TNewMeta>, TValue, TNewMeta>(typeof(IVertexPropertyGremlinQuery<VertexProperty<TValue, TNewMeta>, TValue, TNewMeta>), projections);

        IVertexPropertyGremlinQuery<VertexProperty<TValue>, TValue> IVertexGremlinQueryBase<TElement>.Properties<TValue>(params Expression<Func<TElement, VertexProperty<TValue>[]>>[] projections) => VertexProperties<TValue>(projections, typeof(IVertexPropertyGremlinQuery<VertexProperty<TValue>, TValue>));

        IVertexPropertyGremlinQuery<VertexProperty<TValue, TNewMeta>, TValue, TNewMeta> IVertexGremlinQueryBase<TElement>.Properties<TValue, TNewMeta>(params Expression<Func<TElement, VertexProperty<TValue, TNewMeta>[]>>[] projections) => Properties<VertexProperty<TValue, TNewMeta>, TValue, TNewMeta>(typeof(IVertexPropertyGremlinQuery<VertexProperty<TValue, TNewMeta>, TValue, TNewMeta>), projections);

        IVertexPropertyGremlinQuery<VertexProperty<TValue>, TValue> IVertexGremlinQueryBase<TElement>.Properties<TValue>() => Properties<VertexProperty<TValue>, TValue, object>(Array.Empty<string>(), typeof(IVertexPropertyGremlinQuery<VertexProperty<TValue>, TValue>));

        IVertexPropertyGremlinQuery<VertexProperty<object>, object> IVertexGremlinQueryBase<TElement>.Properties(params Expression<Func<TElement, VertexProperty<object>>>[] projections) => VertexProperties<object>(projections, typeof(IVertexPropertyGremlinQuery<VertexProperty<object>, object>));

        IValueGremlinQuery<TValue> IVertexGremlinQueryBase<TElement>.Values<TValue, TNewMeta>(params Expression<Func<TElement, VertexProperty<TValue, TNewMeta>>>[] projections) => ValuesForProjections<TValue>(projections, typeof(IValueGremlinQuery<TValue>));

        IValueGremlinQuery<TValue> IVertexGremlinQueryBase<TElement>.Values<TValue>(params Expression<Func<TElement, VertexProperty<TValue>>>[] projections) => ValuesForProjections<TValue>(projections, typeof(IValueGremlinQuery<TValue>));

        IValueGremlinQuery<TTarget> IVertexGremlinQueryBase<TElement>.Values<TTarget>(params Expression<Func<TElement, VertexProperty<TTarget>[]>>[] projections) => ValuesForProjections<TTarget>(projections, typeof(IValueGremlinQuery<TTarget>));

        IValueGremlinQuery<TTarget> IVertexGremlinQueryBase<TElement>.Values<TTarget, TTargetMeta>(params Expression<Func<TElement, VertexProperty<TTarget, TTargetMeta>[]>>[] projections) => ValuesForProjections<TTarget>(projections, typeof(IValueGremlinQuery<TTarget>));

        IValueGremlinQuery<object> IVertexGremlinQueryBase<TElement>.Values(params Expression<Func<TElement, VertexProperty<object>>>[] projections) => ValuesForProjections<object>(projections, typeof(IValueGremlinQuery<object>));

        IVertexGremlinQuery<TElement> IVertexGremlinQueryBase<TElement>.Update(TElement element) => AddOrUpdate(element, false);

        IVertexGremlinQuery<TElement> IVertexGremlinQuery<TElement>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue[]>> projection, TProjectedValue value) => Property(projection, value != null ? new[] { value } : null);

        IValueGremlinQuery<TTarget> IVertexGremlinQueryBase<TElement>.Values<TTarget>(params Expression<Func<TElement, TTarget>>[] projections) => ValuesForProjections<TTarget>(projections, typeof(IValueGremlinQuery<TTarget>));

        IValueGremlinQuery<TTarget> IVertexGremlinQueryBase<TElement>.Values<TTarget>(params Expression<Func<TElement, TTarget[]>>[] projections) => ValuesForProjections<TTarget>(projections, typeof(IValueGremlinQuery<TTarget>));

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Mute() => Mute();

        IElementGremlinQuery<TElement> IVertexPropertyGremlinQueryBase<TElement, TScalar, TMeta>.Lower() => this;

        IPropertyGremlinQuery<Property<TValue>> IVertexPropertyGremlinQueryBase<TElement, TScalar, TMeta>.Properties<TValue>(params Expression<Func<TMeta, TValue>>[] projections) => Properties<Property<TValue>, TValue, object>(typeof(IPropertyGremlinQuery<Property<TValue>>), projections);

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IVertexPropertyGremlinQueryBase<TElement, TScalar, TMeta>.Property<TValue>(Expression<Func<TMeta, TValue>> projection, TValue value) => Property(projection, value);

        IValueGremlinQuery<TScalar> IVertexPropertyGremlinQueryBase<TElement, TScalar, TMeta>.Value() => Value<TScalar>(typeof(IValueGremlinQuery<TScalar>));

        IValueGremlinQuery<TMeta> IVertexPropertyGremlinQueryBase<TElement, TScalar, TMeta>.ValueMap() => ValueMap<TMeta>(ImmutableArray<string>.Empty, typeof(IValueGremlinQuery<TMeta>));

        IValueGremlinQuery<TTarget> IVertexPropertyGremlinQueryBase<TElement, TScalar, TMeta>.Values<TTarget>(params Expression<Func<TMeta, TTarget>>[] projections) => ValuesForProjections<TTarget>(projections, typeof(IValueGremlinQuery<TTarget>));

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IVertexPropertyGremlinQueryBase<TElement, TScalar, TMeta>.Where(Expression<Func<VertexProperty<TScalar, TMeta>, bool>> predicate) => Where(predicate);

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Mute() => Mute();

        IElementGremlinQuery<TElement> IVertexPropertyGremlinQueryBase<TElement, TScalar>.Lower() => this;

        IElementGremlinQuery<object> IVertexPropertyGremlinQueryBase.Lower() => Cast<object>(typeof(IElementGremlinQuery<object>));

        IValueGremlinQuery<IDictionary<string, TTarget>> IVertexPropertyGremlinQueryBase.ValueMap<TTarget>() => ValueMap<IDictionary<string, TTarget>>(ImmutableArray<string>.Empty, typeof(IValueGremlinQuery<IDictionary<string, TTarget>>));

        IValueGremlinQuery<IDictionary<string, TTarget>> IVertexPropertyGremlinQueryBase.ValueMap<TTarget>(params string[] keys) => ValueMap<IDictionary<string, TTarget>>(keys.ToImmutableArray(), typeof(IValueGremlinQuery<IDictionary<string, TTarget>>));

        IValueGremlinQuery<IDictionary<string, object>> IVertexPropertyGremlinQueryBase.ValueMap(params string[] keys) => ValueMap<IDictionary<string, object>>(keys.ToImmutableArray(), typeof(IValueGremlinQuery<IDictionary<string, object>>));

        IValueGremlinQuery<TValue> IVertexPropertyGremlinQueryBase.Values<TValue>() => ValuesForKeys<TValue>(Array.Empty<Key>(),typeof(IValueGremlinQuery<TValue>));

        IValueGremlinQuery<TValue> IVertexPropertyGremlinQueryBase.Values<TValue>(params string[] keys) => ValuesForKeys<TValue>(keys.Select(x => (Key)x), typeof(IValueGremlinQuery<TValue>));

        IValueGremlinQuery<object> IVertexPropertyGremlinQueryBase.Values(params string[] keys) => ValuesForKeys<object>(keys.Select(x => (Key)x), typeof(IValueGremlinQuery<object>));

        IPropertyGremlinQuery<Property<object>> IVertexPropertyGremlinQueryBase.Properties(params string[] keys) => Properties<Property<object>, object, object>(keys, typeof(IPropertyGremlinQuery<Property<object>>));

        IVertexPropertyGremlinQuery<VertexProperty<TScalar, TNewMeta>, TScalar, TNewMeta> IVertexPropertyGremlinQueryBase<TElement, TScalar>.Meta<TNewMeta>() => new GremlinQuery<VertexProperty<TScalar, TNewMeta>, object, object, TScalar, TNewMeta, object>(Steps, Environment, typeof(IVertexPropertyGremlinQuery<VertexProperty<TScalar, TNewMeta>, TScalar, TNewMeta>), StepLabelSemantics, Flags);

        IPropertyGremlinQuery<Property<TValue>> IVertexPropertyGremlinQueryBase<TElement, TScalar>.Properties<TValue>(params string[] keys) => Properties<Property<TValue>, object, object>(keys, typeof(IPropertyGremlinQuery<Property<TValue>>));

        IValueGremlinQuery<TScalar> IVertexPropertyGremlinQueryBase<TElement, TScalar>.Value() => Value<TScalar>(typeof(IValueGremlinQuery<TScalar>));

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Mute() => Mute();
              
        IValueTupleGremlinQuery<TElement> IGremlinQueryBaseRec<IValueTupleGremlinQuery<TElement>>.Mute() => Mute();

        IValueGremlinQuery<TTargetValue> IValueTupleGremlinQueryBase<TElement>.Select<TTargetValue>(Expression<Func<TElement, TTargetValue>> projection) => Select<IValueGremlinQuery<TTargetValue>>(projection);
    }
}
