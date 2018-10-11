using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using LanguageExt;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ExRam.Gremlinq
{
    internal sealed class GremlinQueryImpl<TElement, TOutVertex, TInVertex> :
        IVGremlinQuery<TElement>,
        IEGremlinQuery<TElement, TOutVertex, TInVertex>
    {
        public static readonly GremlinQueryImpl<TElement, TOutVertex, TInVertex> Anonymous = Create("__");

        public GremlinQueryImpl(IImmutableList<GremlinStep> steps, IImmutableDictionary<StepLabel, string> stepLabelBindings)
        {
            Steps = steps;
            StepLabelMappings = stepLabelBindings;
        }

        public static GremlinQueryImpl<TElement, TOutVertex, TInVertex> Create(string graphName = "g")
        {
            return new GremlinQueryImpl<TElement, TOutVertex, TInVertex>(ImmutableList<GremlinStep>.Empty.Add(new IdentifierGremlinStep(graphName)), ImmutableDictionary<StepLabel, string>.Empty);
        }

        #region AddV
        IVGremlinQuery<TNewVertex> IGremlinQuery.AddV<TNewVertex>(TNewVertex vertex) => AddV(vertex);

        IVGremlinQuery<TNewVertex> IGremlinQuery.AddV<TNewVertex>() => AddV(new TNewVertex());

        private GremlinQueryImpl<TNewVertex, TOutVertex, TInVertex> AddV<TNewVertex>(TNewVertex vertex)
        {
            return this
                .AddStep<TNewVertex>(new AddVGremlinStep(vertex))
                .AddStep(new AddElementPropertiesStep(vertex));
        }
        #endregion

        #region AddE
        IEGremlinQuery<TNewEdge> IGremlinQuery.AddE<TNewEdge>() => AddE(new TNewEdge());

        IEGremlinQuery<TNewEdge> IGremlinQuery.AddE<TNewEdge>(TNewEdge edge) => AddE(edge);

        IEGremlinQuery<TEdge, TElement> IVGremlinQuery<TElement>.AddE<TEdge>(TEdge edge) => AddE(edge);

        IEGremlinQuery<TNewEdge, TElement> IVGremlinQuery<TElement>.AddE<TNewEdge>() => AddE(new TNewEdge());

        private GremlinQueryImpl<TNewEdge, TElement, Unit> AddE<TNewEdge>(TNewEdge newEdge)
        {
            return this
                .AddStep<TNewEdge, TElement, Unit>(new AddEGremlinStep(newEdge))
                .AddStep(new AddElementPropertiesStep(newEdge));
        }
        #endregion

        #region And
        IGremlinQuery<TElement> IGremlinQuery<TElement>.And(params Func<IGremlinQuery<TElement>, IGremlinQuery>[] andTraversals) => And(andTraversals);

        IVGremlinQuery<TElement> IVGremlinQuery<TElement>.And(params Func<IVGremlinQuery<TElement>, IGremlinQuery>[] andTraversals) => And(andTraversals);

        GremlinQueryImpl<TElement, TOutVertex, TInVertex> And(params Func<GremlinQueryImpl<TElement, TOutVertex, TInVertex>, IGremlinQuery>[] andTraversals)
        {
            return Call(
                "and",
                andTraversals
                    .Select(andTraversal => andTraversal(Anonymous))
                    .Aggregate(
                        ImmutableList<object>.Empty,
                        (list, query2) => query2.Steps.Count == 2 && (query2.Steps[1] as MethodGremlinStep)?.Name == "and"
                            ? list.AddRange(((MethodGremlinStep)query2.Steps[1]).Parameters)
                            : list.Add(query2)));
        }
        #endregion

        #region As
        TTargetQuery IVGremlinQuery<TElement>.As<TTargetQuery>(Func<IVGremlinQuery<TElement>, VStepLabel<TElement>, TTargetQuery> continuation) => As(continuation);

        TTargetQuery IGremlinQuery<TElement>.As<TTargetQuery>(Func<IGremlinQuery<TElement>, StepLabel<TElement>, TTargetQuery> continuation) => As(continuation);

        TTargetQuery IEGremlinQuery<TElement>.As<TTargetQuery>(Func<IEGremlinQuery<TElement>, EStepLabel<TElement>, TTargetQuery> continuation) => As(continuation);

        private TTargetQuery As<TStepLabel, TTargetQuery>(Func<GremlinQueryImpl<TElement, TOutVertex, TInVertex>, TStepLabel, TTargetQuery> continuation)
            where TStepLabel : StepLabel<TElement>, new()
            where TTargetQuery : IGremlinQuery
        {
            var stepLabel = new TStepLabel();

            return continuation(
                this.As(stepLabel),
                stepLabel);
        }

        IGremlinQuery<TElement> IGremlinQuery<TElement>.As(StepLabel<TElement> stepLabel) => As(stepLabel);

        IVGremlinQuery<TElement> IVGremlinQuery<TElement>.As(StepLabel<TElement> stepLabel) => As(stepLabel);

        private GremlinQueryImpl<TElement, TOutVertex, TInVertex> As(StepLabel<TElement> stepLabel) => Call("as", stepLabel);
        #endregion

        IGremlinQuery<TElement> IGremlinQuery<TElement>.Barrier() => Call("barrier");

        #region Cast
        IGremlinQuery<TTarget> IGremlinQuery.Cast<TTarget>() => Cast<TTarget>();

        IVGremlinQuery<TOtherVertex> IVGremlinQuery<TElement>.Cast<TOtherVertex>() => Cast<TOtherVertex>();

        IEGremlinQuery<TOtherEdge, TOutVertex> IEGremlinQuery<TElement, TOutVertex>.Cast<TOtherEdge>() => Cast<TOtherEdge>();

        IEGremlinQuery<TOtherEdge> IEGremlinQuery<TElement>.Cast<TOtherEdge>() => Cast<TOtherEdge>();

        IEGremlinQuery<TOtherEdge, TOutVertex, TInVertex> IEGremlinQuery<TElement, TOutVertex, TInVertex>.Cast<TOtherEdge>() => Cast<TOtherEdge>();

        IOutEGremlinQuery<TOtherEdge, TOutVertex> IOutEGremlinQuery<TElement, TOutVertex>.Cast<TOtherEdge>() => Cast<TOtherEdge>();

        IInEGremlinQuery<TOtherEdge, TInVertex> IInEGremlinQuery<TElement, TInVertex>.Cast<TOtherEdge>() => Cast<TOtherEdge>();

        private GremlinQueryImpl<TTarget, TOutVertex, TInVertex> Cast<TTarget>() => new GremlinQueryImpl<TTarget, TOutVertex, TInVertex>(Steps, StepLabelMappings);
        #endregion

        #region Coalesce
        TTargetQuery IVGremlinQuery<TElement>.Coalesce<TTargetQuery>(params Func<IVGremlinQuery<TElement>, TTargetQuery>[] traversals) => Coalesce(traversals);

        TTargetQuery IGremlinQuery<TElement>.Coalesce<TTargetQuery>(params Func<IGremlinQuery<TElement>, TTargetQuery>[] traversals) => Coalesce(traversals);

        private TTargetQuery Coalesce<TTargetQuery>(params Func<GremlinQueryImpl<TElement, TOutVertex, TInVertex>, TTargetQuery>[] traversals)
            where TTargetQuery : IGremlinQuery
        {
            return this
                .Call(
                    "coalesce",
                    traversals
                        .Select(traversal => (object)traversal(Anonymous))
                        .ToImmutableList())
                .CastQuery<TTargetQuery>();
        }
        #endregion

        #region Choose
        IGremlinQuery<TResult> IGremlinQuery<TElement>.Choose<TResult>(Func<IGremlinQuery<TElement>, IGremlinQuery> traversalPredicate, Func<IGremlinQuery<TElement>, IGremlinQuery<TResult>> trueChoice, Func<IGremlinQuery<TElement>, IGremlinQuery<TResult>> falseChoice)
        {
            var anonymous = Anonymous;

            return Call<TResult>(
                "choose",
                traversalPredicate(anonymous),
                trueChoice(anonymous),
                falseChoice(anonymous));
        }

        IGremlinQuery<TResult> IGremlinQuery<TElement>.Choose<TResult>(Func<IGremlinQuery<TElement>, IGremlinQuery> traversalPredicate, Func<IGremlinQuery<TElement>, IGremlinQuery<TResult>> trueChoice)
        {
            var anonymous = Anonymous;

            return Call<TResult>(
                "choose",
                traversalPredicate(anonymous),
                trueChoice(anonymous));
        }
        #endregion

        #region BothX
        IVGremlinQuery<Vertex> IVGremlinQuery<TElement>.Both<TNewEdge>() => AddStep<Vertex>(new DerivedLabelNamesGremlinStep<TNewEdge>("both"));

        IEGremlinQuery<TNewEdge> IVGremlinQuery<TElement>.BothE<TNewEdge>() => AddStep<TNewEdge>(new DerivedLabelNamesGremlinStep<TNewEdge>("bothE"));

        IVGremlinQuery<Vertex> IEGremlinQuery<TElement>.BothV() => Call<Vertex>("bothV");
        #endregion

        #region Dedup
        IGremlinQuery<TElement> IGremlinQuery<TElement>.Dedup() => Dedup();

        IVGremlinQuery<TElement> IVGremlinQuery<TElement>.Dedup() => Dedup();

        IEGremlinQuery<TElement> IEGremlinQuery<TElement>.Dedup() => Dedup();

        private GremlinQueryImpl<TElement, TOutVertex, TInVertex> Dedup() => Call("dedup");
        #endregion

        IGremlinQuery<Unit> IGremlinQuery.Drop() => Call<Unit>("drop");

        IEGremlinQuery<Edge> IGremlinQuery.E(params object[] ids) => Call<Edge>("E", ids);

        #region Emit
        IGremlinQuery<TElement> IGremlinQuery<TElement>.Emit() => Emit();

        IVGremlinQuery<TElement> IVGremlinQuery<TElement>.Emit() => Emit();

        IEGremlinQuery<TElement> IEGremlinQuery<TElement>.Emit() => Emit();

        private GremlinQueryImpl<TElement, TOutVertex, TInVertex> Emit() => Call("emit");
        #endregion

        IGremlinQuery<string> IGremlinQuery.Explain() => Call<string>("explain");

        IGremlinQuery<TElement> IGremlinQuery<TElement>.FilterWithLambda(string lambda) => Call("filter", new Lambda(lambda));

        IGremlinQuery<TElement[]> IGremlinQuery<TElement>.Fold() => Call<TElement[]>("fold");

        #region From
        IEGremlinQuery<TElement, TTargetVertex, TOutVertex> IEGremlinQuery<TElement, TOutVertex>.From<TTargetVertex>(StepLabel<TTargetVertex> stepLabel) => Call<TElement, TTargetVertex, TOutVertex>("from", stepLabel);

        IEGremlinQuery<TElement, TTargetVertex, TOutVertex> IEGremlinQuery<TElement, TOutVertex>.From<TTargetVertex>(Func<IGremlinQuery<TElement>, IGremlinQuery<TTargetVertex>> fromVertexTraversal) => Call<TElement, TTargetVertex, TOutVertex>("from", fromVertexTraversal(Anonymous));

        IOutEGremlinQuery<TElement, TNewOutVertex> IEGremlinQuery<TElement>.From<TNewOutVertex>(Func<IGremlinQuery<TElement>, IGremlinQuery<TNewOutVertex>> fromVertexTraversal) => Call<TElement, TNewOutVertex, Unit>("from", fromVertexTraversal(Anonymous));

        IOutEGremlinQuery<TElement, TNewOutVertex> IEGremlinQuery<TElement>.From<TNewOutVertex>(StepLabel<TNewOutVertex> stepLabel) => Call<TElement, TNewOutVertex, Unit>("from", stepLabel);
        #endregion

        IGremlinQuery<object> IGremlinQuery.Id() => Call<object>("id");

        IGremlinQuery<TElement> IGremlinQuery<TElement>.Identity() => Call("identity");

        IVGremlinQuery<Vertex> IVGremlinQuery<TElement>.In<TNewEdge>() => AddStep<Vertex>(new DerivedLabelNamesGremlinStep<TNewEdge>("in"));

        IGremlinQuery<TTarget> IGremlinQuery.InsertStep<TTarget>(int index, GremlinStep step) => new GremlinQueryImpl<TTarget, TOutVertex, TInVertex>(Steps.Insert(index, step), StepLabelMappings);

        IInEGremlinQuery<TNewEdge, TElement> IVGremlinQuery<TElement>.InE<TNewEdge>() => AddStep<TNewEdge, Unit, TElement>(new DerivedLabelNamesGremlinStep<TNewEdge>("inE"));

        #region InV
        IVGremlinQuery<Vertex> IEGremlinQuery<TElement>.InV() => Call<Vertex, Unit, Unit>("inV");

        IVGremlinQuery<TInVertex> IEGremlinQuery<TElement, TOutVertex, TInVertex>.InV() => Call<TInVertex, Unit, Unit>("inV");

        IVGremlinQuery<TInVertex> IInEGremlinQuery<TElement, TInVertex>.InV() => Call<TInVertex, Unit, Unit>("inV");
        #endregion

        IGremlinQuery<TElement> IGremlinQuery<TElement>.Inject(params TElement[] elements) => Call("inject", elements);

        #region Limit
        IGremlinQuery<TElement> IGremlinQuery<TElement>.Limit(long limit) => Limit(limit);

        IVGremlinQuery<TElement> IVGremlinQuery<TElement>.Limit(long limit) => Limit(limit);

        IEGremlinQuery<TElement> IEGremlinQuery<TElement>.Limit(long limit) => Limit(limit);

        private GremlinQueryImpl<TElement, TOutVertex, TInVertex> Limit(long limit) => Call("limit", limit);
        #endregion

        #region Local
        TTargetQuery IEGremlinQuery<TElement>.Local<TTargetQuery>(Func<IEGremlinQuery<TElement>, TTargetQuery> localTraversal) => Local(localTraversal);

        TTargetQuery IVGremlinQuery<TElement>.Local<TTargetQuery>(Func<IVGremlinQuery<TElement>, TTargetQuery> localTraversal) => Local(localTraversal);

        TTargetQuery IGremlinQuery<TElement>.Local<TTargetQuery>(Func<IGremlinQuery<TElement>, TTargetQuery> localTraversal) => Local(localTraversal);

        private TTargetQuery Local<TTargetQuery>(Func<GremlinQueryImpl<TElement, TOutVertex, TInVertex>, TTargetQuery> localTraversal)
            where TTargetQuery : IGremlinQuery
        {
            return this
                .Call(
                    "local",
                    localTraversal(Anonymous))
                .CastQuery<TTargetQuery>();
        }
        #endregion

        IGremlinQuery<TTarget> IGremlinQuery<TElement>.Map<TTarget>(Func<IGremlinQuery<TElement>, IGremlinQuery<TTarget>> mapping) => Call<TTarget>("map", mapping(Anonymous));

        IGremlinQuery<TElement> IGremlinQuery<TElement>.Match(params Func<IGremlinQuery<TElement>, IGremlinQuery<TElement>>[] matchTraversals) => Call("match", matchTraversals.Select(traversal => traversal(Anonymous)).ToArray());

        #region Not
        IGremlinQuery<TElement> IGremlinQuery<TElement>.Not(Func<IGremlinQuery<TElement>, IGremlinQuery> notTraversal) => Not(notTraversal);

        IVGremlinQuery<TElement> IVGremlinQuery<TElement>.Not(Func<IVGremlinQuery<TElement>, IGremlinQuery> notTraversal) => Not(notTraversal);

        private GremlinQueryImpl<TElement, TOutVertex, TInVertex> Not(Func<GremlinQueryImpl<TElement, TOutVertex, TInVertex>, IGremlinQuery> notTraversal) => Call("not", notTraversal(Anonymous));
        #endregion

        #region OfType
        IGremlinQuery<TTarget> IGremlinQuery.OfType<TTarget>() => OfType<TTarget>();

        IVGremlinQuery<TTarget> IVGremlinQuery<TElement>.OfType<TTarget>() => OfType<TTarget>();

        IEGremlinQuery<TTarget, TOutVertex> IEGremlinQuery<TElement, TOutVertex>.OfType<TTarget>() => OfType<TTarget>();

        IEGremlinQuery<TTarget> IEGremlinQuery<TElement>.OfType<TTarget>() => OfType<TTarget>();

        IEGremlinQuery<TTarget, TOutVertex, TInVertex> IEGremlinQuery<TElement, TOutVertex, TInVertex>.OfType<TTarget>() => OfType<TTarget>();

        IOutEGremlinQuery<TTarget, TOutVertex> IOutEGremlinQuery<TElement, TOutVertex>.OfType<TTarget>() => OfType<TTarget>();

        IInEGremlinQuery<TTarget, TInVertex> IInEGremlinQuery<TElement, TInVertex>.OfType<TTarget>() => OfType<TTarget>();

        private GremlinQueryImpl<TTarget, TOutVertex, TInVertex> OfType<TTarget>() => AddStep<TTarget>(new DerivedLabelNamesGremlinStep<TTarget>("hasLabel"));
        #endregion

        IGremlinQuery<TTarget> IVGremlinQuery<TElement>.Optional<TTarget>(Func<IVGremlinQuery<TElement>, IGremlinQuery<TTarget>> optionalTraversal) => Call<TTarget>("optional", optionalTraversal(Anonymous));

        #region Or
        IGremlinQuery<TElement> IGremlinQuery<TElement>.Or(params Func<IGremlinQuery<TElement>, IGremlinQuery>[] orTraversals) => Or(orTraversals);

        IVGremlinQuery<TElement> IVGremlinQuery<TElement>.Or(params Func<IVGremlinQuery<TElement>, IGremlinQuery>[] orTraversals) => Or(orTraversals);

        private GremlinQueryImpl<TElement, TOutVertex, TInVertex> Or(params Func<GremlinQueryImpl<TElement, TOutVertex, TInVertex>, IGremlinQuery>[] orTraversals)
        {
            return Call(
                "or",
                orTraversals
                    .Select(andTraversal => andTraversal(Anonymous))
                    .Aggregate(
                        ImmutableList<object>.Empty,
                        (list, query2) => query2.Steps.Count == 2 && (query2.Steps[1] as MethodGremlinStep)?.Name == "or"
                            ? list.AddRange(((MethodGremlinStep)query2.Steps[1]).Parameters)
                            : list.Add(query2)));
        }
        #endregion

        #region OrderBy{Descending} projection
        IGremlinQuery<TElement> IGremlinQuery<TElement>.OrderBy(Expression<Func<TElement, object>> projection) => OrderBy(projection, Order.Increasing);

        IVGremlinQuery<TElement> IVGremlinQuery<TElement>.OrderBy(Expression<Func<TElement, object>> projection) => OrderBy(projection, Order.Increasing);

        IEGremlinQuery<TElement> IEGremlinQuery<TElement>.OrderBy(Expression<Func<TElement, object>> projection) => OrderBy(projection, Order.Increasing);

        IGremlinQuery<TElement> IGremlinQuery<TElement>.OrderByDescending(Expression<Func<TElement, object>> projection) => OrderBy(projection, Order.Decreasing);

        IVGremlinQuery<TElement> IVGremlinQuery<TElement>.OrderByDescending(Expression<Func<TElement, object>> projection) => OrderBy(projection, Order.Decreasing);

        IEGremlinQuery<TElement> IEGremlinQuery<TElement>.OrderByDescending(Expression<Func<TElement, object>> projection) => OrderBy(projection, Order.Decreasing);

        private GremlinQueryImpl<TElement, TOutVertex, TInVertex> OrderBy(Expression<Func<TElement, object>> projection, Order order)
        {
            if (projection.Body.StripConvert() is MemberExpression memberExpression)
            {
                return this
                    .Call("order")
                    .Call("by", memberExpression.Member.Name, order);
            }

            throw new NotSupportedException();
        }
        #endregion

        #region OrderBy{Descending} traversal
        IGremlinQuery<TElement> IGremlinQuery<TElement>.OrderBy(Func<IGremlinQuery<TElement>, IGremlinQuery> traversal) => OrderBy(traversal, Order.Increasing);

        IVGremlinQuery<TElement> IVGremlinQuery<TElement>.OrderBy(Func<IVGremlinQuery<TElement>, IGremlinQuery> traversal) => OrderBy(traversal, Order.Increasing);

        IEGremlinQuery<TElement> IEGremlinQuery<TElement>.OrderBy(Func<IEGremlinQuery<TElement>, IGremlinQuery> traversal) => OrderBy(traversal, Order.Increasing);

        IGremlinQuery<TElement> IGremlinQuery<TElement>.OrderByDescending(Func<IGremlinQuery<TElement>, IGremlinQuery> traversal) => OrderBy(traversal, Order.Decreasing);

        IVGremlinQuery<TElement> IVGremlinQuery<TElement>.OrderByDescending(Func<IVGremlinQuery<TElement>, IGremlinQuery> traversal) => OrderBy(traversal, Order.Decreasing);

        IEGremlinQuery<TElement> IEGremlinQuery<TElement>.OrderByDescending(Func<IEGremlinQuery<TElement>, IGremlinQuery> traversal) => OrderBy(traversal, Order.Decreasing);

        private GremlinQueryImpl<TElement, TOutVertex, TInVertex> OrderBy(Func<GremlinQueryImpl<TElement, TOutVertex, TInVertex>, IGremlinQuery> traversal, Order order)
        {
            return this
                .Call("order")
                .Call("by", traversal(Anonymous), order);
        }
        #endregion

        #region OrderBy{Descending} lambda
        IGremlinQuery<TElement> IGremlinQuery<TElement>.OrderBy(string lambda) => OrderBy(lambda);

        IVGremlinQuery<TElement> IVGremlinQuery<TElement>.OrderBy(string lambda) => OrderBy(lambda);

        IEGremlinQuery<TElement> IEGremlinQuery<TElement>.OrderBy(string lambda) => OrderBy(lambda);

        private GremlinQueryImpl<TElement, TOutVertex, TInVertex> OrderBy(string lambda)
        {
            return this
                .Call("order")
                .Call("by", new Lambda(lambda));
        }
        #endregion
        
        IVGremlinQuery<Vertex> IEGremlinQuery<TElement>.OtherV() => Call<Vertex>("otherV");

        #region OutX
        IOutEGremlinQuery<TNewEdge, TElement> IVGremlinQuery<TElement>.OutE<TNewEdge>() => AddStep<TNewEdge, TElement, Unit>(new DerivedLabelNamesGremlinStep<TNewEdge>("outE"));

        IVGremlinQuery<Vertex> IEGremlinQuery<TElement>.OutV() => Call<Vertex, Unit, Unit>("outV");
        
        IVGremlinQuery<TOutVertex> IEGremlinQuery<TElement, TOutVertex, TInVertex>.OutV() => Call<TOutVertex, Unit, Unit>("outV");

        IVGremlinQuery<TOutVertex> IOutEGremlinQuery<TElement, TOutVertex>.OutV() => Call<TOutVertex, Unit, Unit>("outV");

        IVGremlinQuery<Vertex> IVGremlinQuery<TElement>.Out<TNewEdge>() => AddStep<Vertex>(new DerivedLabelNamesGremlinStep<TNewEdge>("out"));
        #endregion

        IGremlinQuery<string> IGremlinQuery.Profile() => Call<string>("profile");

        #region Property
        IGremlinQuery<TElement> IGremlinQuery<TElement>.Property(string key, object value) => Call("property", key, value);
        #endregion

        IGremlinQuery<TElement> IGremlinQuery<TElement>.Range(long low, long high) => Call("range", low, high);

        IVGremlinQuery<TElement> IVGremlinQuery<TElement>.Range(long low, long high) => Call("range", low, high);

        IEGremlinQuery<TElement> IEGremlinQuery<TElement>.Range(long low, long high) => Call("range", low, high);

        IVGremlinQuery<TElement> IVGremlinQuery<TElement>.Repeat(Func<IVGremlinQuery<TElement>, IVGremlinQuery<TElement>> repeatTraversal) => Call("repeat", repeatTraversal(Anonymous));

        #region Select
        IGremlinQuery<TStep> IGremlinQuery.Select<TStep>(StepLabel<TStep> label) => Select<TStep, Unit, Unit>(label);

        IVGremlinQuery<TVertex> IGremlinQuery.Select<TVertex>(VStepLabel<TVertex> label) => Select<TVertex, Unit, Unit>(label);

        IEGremlinQuery<TEdge> IGremlinQuery.Select<TEdge>(EStepLabel<TEdge> label) => Select<TEdge, Unit, Unit>(label);

        private GremlinQueryImpl<TSelectedElement, TSelectedOutVertex, TSelectedInVertex> Select<TSelectedElement, TSelectedOutVertex, TSelectedInVertex>(StepLabel<TSelectedElement> stepLabel) => Call<TSelectedElement, TSelectedOutVertex, TSelectedInVertex>("select", stepLabel);

        IGremlinQuery<(T1, T2)> IGremlinQuery.Select<T1, T2>(StepLabel<T1> label1, StepLabel<T2> label2)
        {
            return this
                .Call<(T1, T2)>("select", label1, label2)
                .AddStepLabelBinding(x => x.Item1, label1)
                .AddStepLabelBinding(x => x.Item2, label2);
        }

        IGremlinQuery<(T1, T2, T3)> IGremlinQuery.Select<T1, T2, T3>(StepLabel<T1> label1, StepLabel<T2> label2, StepLabel<T3> label3)
        {
            return this
                .Call<(T1, T2, T3)>("select", label1, label2, label3)
                .AddStepLabelBinding(x => x.Item1, label1)
                .AddStepLabelBinding(x => x.Item2, label2)
                .AddStepLabelBinding(x => x.Item3, label3);
        }

        IGremlinQuery<(T1, T2, T3, T4)> IGremlinQuery.Select<T1, T2, T3, T4>(StepLabel<T1> label1, StepLabel<T2> label2, StepLabel<T3> label3, StepLabel<T4> label4)
        {
            return this
                .Call<(T1, T2, T3, T4)>("select", label1, label2, label3, label4)
                .AddStepLabelBinding(x => x.Item1, label1)
                .AddStepLabelBinding(x => x.Item2, label2)
                .AddStepLabelBinding(x => x.Item3, label3)
                .AddStepLabelBinding(x => x.Item4, label4);
        }
        #endregion

        #region SideEffect
        IGremlinQuery<TElement> IGremlinQuery<TElement>.SideEffect(Func<IGremlinQuery<TElement>, IGremlinQuery> sideEffectTraversal) => SideEffect(sideEffectTraversal);

        IVGremlinQuery<TElement> IVGremlinQuery<TElement>.SideEffect(Func<IVGremlinQuery<TElement>, IGremlinQuery> sideEffectTraversal) => SideEffect(sideEffectTraversal);

        IEGremlinQuery<TElement> IEGremlinQuery<TElement>.SideEffect(Func<IEGremlinQuery<TElement>, IGremlinQuery> sideEffectTraversal) => SideEffect(sideEffectTraversal);

        private GremlinQueryImpl<TElement, TOutVertex, TInVertex> SideEffect(Func<GremlinQueryImpl<TElement, TOutVertex, TInVertex>, IGremlinQuery> sideEffectTraversal) => Call("sideEffect", sideEffectTraversal(Anonymous));
        #endregion

        #region Skip
        IGremlinQuery<TElement> IGremlinQuery<TElement>.Skip(long skip) => Skip(skip);

        IVGremlinQuery<TElement> IVGremlinQuery<TElement>.Skip(long skip) => Skip(skip);

        IEGremlinQuery<TElement> IEGremlinQuery<TElement>.Skip(long skip) => Skip( skip);
        
        private GremlinQueryImpl<TElement, TOutVertex, TInVertex> Skip(long skip) => Call("skip", skip);
        #endregion

        IGremlinQuery<TElement> IGremlinQuery<TElement>.Sum(Scope scope) => Call("sum", scope);

        IGremlinQuery<TElement> IGremlinQuery<TElement>.Times(int count) => Call("times", count);

        #region Tail
        IGremlinQuery<TElement> IGremlinQuery<TElement>.Tail(long limit) => Tail(limit);

        IVGremlinQuery<TElement> IVGremlinQuery<TElement>.Tail(long limit) => Tail(limit);

        IEGremlinQuery<TElement> IEGremlinQuery<TElement>.Tail(long limit) => Tail(limit);

        private GremlinQueryImpl<TElement, TOutVertex, TInVertex> Tail(long limit) => Call("tail", limit);
        #endregion

        #region To
        IEGremlinQuery<TElement, TOutVertex, TTargetVertex> IEGremlinQuery<TElement, TOutVertex>.To<TTargetVertex>(StepLabel<TTargetVertex> stepLabel) => Call<TElement, TOutVertex, TTargetVertex>("to", stepLabel);

        IEGremlinQuery<TElement, TOutVertex, TTargetVertex> IEGremlinQuery<TElement, TOutVertex>.To<TTargetVertex>(Func<IGremlinQuery<TElement>, IGremlinQuery<TTargetVertex>> toVertexTraversal) => Call<TElement, TOutVertex, TTargetVertex>("to", toVertexTraversal(Anonymous));

        IInEGremlinQuery<TElement, TNewInVertex> IEGremlinQuery<TElement>.To<TNewInVertex>(Func<IGremlinQuery<TElement>, IGremlinQuery<TNewInVertex>> toVertexTraversal) => Call<TElement, Unit, TNewInVertex>("to", toVertexTraversal(Anonymous));

        IInEGremlinQuery<TElement, TNewInVertex> IEGremlinQuery<TElement>.To<TNewInVertex>(StepLabel<TNewInVertex> stepLabel) => Call<TElement, Unit, TNewInVertex>("to", stepLabel);
        #endregion

        IGremlinQuery<TElement> IGremlinQuery<TElement>.Unfold(IGremlinQuery<IEnumerable<TElement>> query)
        {
            throw new NotImplementedException();    //Bug!
                                                    //return query
                                                    //    .Call<TElement>("unfold");
        }
        
        TTargetQuery IVGremlinQuery<TElement>.Union<TTargetQuery>(params Func<IVGremlinQuery<TElement>, TTargetQuery>[] unionTraversals)
        {
            return this
                .Call(
                    "union",
                    unionTraversals
                        .Select(unionTraversal => (object)unionTraversal(Anonymous))
                        .ToImmutableList())
                .CastQuery<TTargetQuery>();
        }

        IGremlinQuery<TElement> IGremlinQuery<TElement>.Until(Func<IGremlinQuery<TElement>, IGremlinQuery> untilTraversal) => Call("until", untilTraversal(Anonymous));

        IVGremlinQuery<Vertex> IGremlinQuery.V(params object[] ids) => Call<Vertex>("V", ids);

        IGremlinQuery<TTarget> IGremlinQuery<TElement>.Values<TTarget>(params Expression<Func<TElement, TTarget>>[] projections) => AddStep<TTarget>(new ValuesGremlinStep<TElement, TTarget>(projections));

        #region Where (Traversal)
        IGremlinQuery<TElement> IGremlinQuery<TElement>.Where(Func<IGremlinQuery<TElement>, IGremlinQuery> filterTraversal) => Call("where", filterTraversal(Anonymous));

        IEGremlinQuery<TElement, TOutVertex> IEGremlinQuery<TElement, TOutVertex>.Where(Func<IEGremlinQuery<TElement, TOutVertex>, IGremlinQuery> filterTraversal) => Where(filterTraversal);

        IEGremlinQuery<TElement> IEGremlinQuery<TElement>.Where(Func<IEGremlinQuery<TElement>, IGremlinQuery> filterTraversal) => Where(filterTraversal);

        IEGremlinQuery<TElement, TOutVertex, TInVertex> IEGremlinQuery<TElement, TOutVertex, TInVertex>.Where(Func<IEGremlinQuery<TElement, TOutVertex, TInVertex>, IGremlinQuery> filterTraversal) => Where(filterTraversal);

        IVGremlinQuery<TElement> IVGremlinQuery<TElement>.Where(Func<IVGremlinQuery<TElement>, IGremlinQuery> filterTraversal) => Where(filterTraversal);

        IOutEGremlinQuery<TElement, TOutVertex> IOutEGremlinQuery<TElement, TOutVertex>.Where(Func<IOutEGremlinQuery<TElement, TOutVertex>, IGremlinQuery> filterTraversal) => Where(filterTraversal);

        IInEGremlinQuery<TElement, TInVertex> IInEGremlinQuery<TElement, TInVertex>.Where(Func<IInEGremlinQuery<TElement, TInVertex>, IGremlinQuery> filterTraversal) => this.Where(filterTraversal);

        private GremlinQueryImpl<TElement, TOutVertex, TInVertex> Where(Func<GremlinQueryImpl<TElement, TOutVertex, TInVertex>, IGremlinQuery> filterTraversal) => Call("where", filterTraversal(Anonymous));
        #endregion

        #region Where (Predicate)
        IVGremlinQuery<TElement> IVGremlinQuery<TElement>.Where(Expression<Func<TElement, bool>> predicate) => Where(predicate);

        IGremlinQuery<TElement> IGremlinQuery<TElement>.Where(Expression<Func<TElement, bool>> predicate) => Where(predicate);

        IEGremlinQuery<TElement, TOutVertex> IEGremlinQuery<TElement, TOutVertex>.Where(Expression<Func<TElement, bool>> predicate) => Where(predicate);

        IEGremlinQuery<TElement> IEGremlinQuery<TElement>.Where(Expression<Func<TElement, bool>> predicate) => Where(predicate);

        IEGremlinQuery<TElement, TOutVertex, TInVertex> IEGremlinQuery<TElement, TOutVertex, TInVertex>.Where(Expression<Func<TElement, bool>> predicate) => Where(predicate);

        IOutEGremlinQuery<TElement, TOutVertex> IOutEGremlinQuery<TElement, TOutVertex>.Where(Expression<Func<TElement, bool>> predicate) => this.Where(predicate);

        IInEGremlinQuery<TElement, TInVertex> IInEGremlinQuery<TElement, TInVertex>.Where(Expression<Func<TElement, bool>> predicate) => this.Where(predicate);

        private GremlinQueryImpl<TElement, TOutVertex, TInVertex> Where(Expression<Func<TElement, bool>> predicate)
        {
            var body = predicate.Body;

            switch (body)
            {
                case UnaryExpression unaryExpression:
                {
                    if (unaryExpression.NodeType == ExpressionType.Not)
                        return this.Not(_ => _.Where(Expression.Lambda<Func<TElement, bool>>(unaryExpression.Operand, predicate.Parameters)));

                    break;
                }
                case MemberExpression memberExpression:
                {
                    if (memberExpression.Member is PropertyInfo property)
                    {
                        if (property.PropertyType == typeof(bool))
                            return this.Where(predicate.Parameters[0], memberExpression, Expression.Constant(true), ExpressionType.Equal);
                    }

                    break;
                }
                case BinaryExpression binaryExpression:
                    return this.Where(predicate.Parameters[0], binaryExpression.Left.StripConvert(), binaryExpression.Right.StripConvert(), binaryExpression.NodeType);
                case MethodCallExpression methodCallExpression:
                {
                    var methodInfo = methodCallExpression.Method;

                    if (methodInfo.DeclaringType == typeof(Enumerable))
                    {
                        // ReSharper disable once SwitchStatementMissingSomeCases
                        switch (methodInfo.Name)
                        {
                            case nameof(Enumerable.Contains) when methodInfo.GetParameters().Length == 2:
                            {
                                if (methodCallExpression.Arguments[0] is MemberExpression leftMember && leftMember.Expression == predicate.Parameters[0])
                                    return this.Has(leftMember, P.Eq(methodCallExpression.Arguments[1].GetValue()));

                                if (methodCallExpression.Arguments[1] is MemberExpression rightMember && rightMember.Expression == predicate.Parameters[0])
                                {
                                    if (methodCallExpression.Arguments[0].GetValue() is IEnumerable enumerable)
                                        return this.Has(rightMember, P.Within(enumerable.Cast<object>().ToArray()));
                                }

                                throw new NotSupportedException();
                            }
                            case nameof(Enumerable.Any) when methodInfo.GetParameters().Length == 1:
                                return this.Where(predicate.Parameters[0], methodCallExpression.Arguments[0], Expression.Constant(null, methodCallExpression.Arguments[0].Type), ExpressionType.NotEqual);
                        }
                    }
                    else if (methodInfo.DeclaringType == typeof(EnumerableExtensions))
                    {
                        if (methodInfo.Name == nameof(EnumerableExtensions.Intersects) && methodInfo.GetParameters().Length == 2)
                        {
                            if (methodCallExpression.Arguments[0] is MemberExpression innerMemberExpression)
                            {
                                var constant = methodCallExpression.Arguments[1].GetValue();

                                if (constant is IEnumerable arrayConstant)
                                    return this.Has(innerMemberExpression, P.Within(arrayConstant.Cast<object>().ToArray()));
                            }
                        }
                    }
                    else if (methodInfo.DeclaringType == typeof(string))
                    {
                        if (methodInfo.Name == nameof(string.StartsWith))
                        {
                            if (methodCallExpression.Arguments[0] is MemberExpression argumentExpression && argumentExpression.Expression == predicate.Parameters[0])
                            {
                                if (methodCallExpression.Object.GetValue() is string stringValue)
                                {
                                    return this.Has(
                                        argumentExpression,
                                        P.Within(Enumerable
                                            .Range(0, stringValue.Length + 1)
                                            .Select(i => stringValue.Substring(0, i))
                                            .Cast<object>()
                                            .ToArray()));
                                }
                            }
                            else if (methodCallExpression.Object is MemberExpression memberExpression && memberExpression.Expression == predicate.Parameters[0])
                            {
                                if (methodCallExpression.Arguments[0].GetValue() is string lowerBound)
                                {
                                    string upperBound;

                                    if (lowerBound.Length == 0 || lowerBound[lowerBound.Length - 1] == char.MaxValue)
                                        upperBound = lowerBound + char.MinValue;
                                    else
                                    {
                                        var upperBoundChars = lowerBound.ToCharArray();

                                        upperBoundChars[upperBoundChars.Length - 1]++;
                                        upperBound = new string(upperBoundChars);
                                    }

                                    return this.Has(memberExpression, P.Between(lowerBound, upperBound));
                                }
                            }
                        }
                    }

                    break;
                }
            }

            throw new NotSupportedException();
        }

        private GremlinQueryImpl<TElement, TOutVertex, TInVertex> Where(ParameterExpression parameter, Expression left, Expression right, ExpressionType nodeType)
        {
            if (nodeType == ExpressionType.OrElse || nodeType == ExpressionType.AndAlso)
            {
                var leftLambda = Expression.Lambda<Func<TElement, bool>>(left, parameter);
                var rightLambda = Expression.Lambda<Func<TElement, bool>>(right, parameter);

                return nodeType == ExpressionType.OrElse
                    ? this
                        .Or(
                            _ => _.Where(leftLambda),
                            _ => _.Where(rightLambda))
                    : this
                        .And(
                            _ => _.Where(leftLambda),
                            _ => _.Where(rightLambda));
            }

            return this.Where(parameter, left, right.GetValue(), nodeType);
        }

        private GremlinQueryImpl<TElement, TOutVertex, TInVertex> Where(ParameterExpression parameter, Expression left, object rightConstant, ExpressionType nodeType)
        {
            if (rightConstant == null)
            {
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (nodeType)
                {
                    case ExpressionType.Equal:
                        return this.Not(__ => __.Has(left));
                    case ExpressionType.NotEqual:
                        return this.Has(left);
                }
            }
            else
            {
                var predicateArgument = P.ForExpressionType(nodeType, rightConstant);

                switch (left)
                {
                    case MemberExpression leftMemberExpression when parameter == leftMemberExpression.Expression:
                    {
                        return this.Has(
                            leftMemberExpression,
                            rightConstant is StepLabel
                                ? GremlinQuery
                                    .Anonymous
                                    .Call("where", predicateArgument)
                                : (object)predicateArgument);
                    }
                    case ParameterExpression leftParameterExpression when parameter == leftParameterExpression:
                    {
                        return this.Call(
                            rightConstant is StepLabel
                                ? "where"
                                : "is",
                            predicateArgument);
                    }
                }
            }

            throw new NotSupportedException();
        }
        #endregion

        private GremlinQueryImpl<TElement, TOutVertex, TInVertex> Has(Expression expression, Option<object> maybeArgument = default)
        {
            string name;

            switch (expression)
            {
                case MemberExpression leftMemberExpression:
                {
                    name = leftMemberExpression.Member.Name;
                    break;
                }
                case ParameterExpression leftParameterExpression:
                {
                    name = leftParameterExpression.Name;
                    break;
                }
                default:
                    throw new NotSupportedException();
            }

            return this.AddStep(new HasStep(name, maybeArgument));
        }

        public GroovyExpressionState Serialize(StringBuilder stringBuilder, GroovyExpressionState state)
        {
            foreach (var step in Steps)
            {
                if (step is IGroovySerializable serializableStep)
                    state = serializableStep.Serialize(stringBuilder, state);
                else
                    throw new ArgumentException("Query contains non-serializable step. Please call RewriteSteps on the query first.");
            }

            return state;
        }

        #region Call
        private GremlinQueryImpl<TElement, TOutVertex, TInVertex> Call(string name, params object[] parameters) => Call<TElement>(name, parameters.ToImmutableList());

        private GremlinQueryImpl<TTarget, TOutVertex, TInVertex> Call<TTarget>(string name, params object[] parameters) => Call<TTarget>(name, parameters.ToImmutableList());

        private GremlinQueryImpl<TTarget, TNewOutVertex, TNewInVertex> Call<TTarget, TNewOutVertex, TNewInVertex>(string name, params object[] parameters) => Call<TTarget, TNewOutVertex, TNewInVertex>(name, parameters.ToImmutableList());

        private GremlinQueryImpl<TElement, TOutVertex, TInVertex> Call(string name, ImmutableList<object> parameters) => AddStep<TElement>(new MethodGremlinStep(name, parameters));

        private GremlinQueryImpl<TTarget, TOutVertex, TInVertex> Call<TTarget>(string name, ImmutableList<object> parameters) => AddStep<TTarget>(new MethodGremlinStep(name, parameters));

        private GremlinQueryImpl<TTarget, TNewOutVertex, TNewInVertex> Call<TTarget, TNewOutVertex, TNewInVertex>(string name, ImmutableList<object> parameters) => AddStep<TTarget, TNewOutVertex, TNewInVertex>(new MethodGremlinStep(name, parameters));
        #endregion

        #region AddStep
        private GremlinQueryImpl<TElement, TOutVertex, TInVertex> AddStep(GremlinStep step) => AddStep<TElement>(step);

        private GremlinQueryImpl<TNewElement, TOutVertex, TInVertex> AddStep<TNewElement>(GremlinStep step) => AddStep<TNewElement, TOutVertex, TInVertex>(step);

        private GremlinQueryImpl<TNewElement, TNewOutVertex, TNewInVertex> AddStep<TNewElement, TNewOutVertex, TNewInVertex>(GremlinStep step) => new GremlinQueryImpl<TNewElement, TNewOutVertex, TNewInVertex>(Steps.Insert(Steps.Count, step), StepLabelMappings);
        #endregion

        private TTargetQuery CastQuery<TTargetQuery>() where TTargetQuery : IGremlinQuery
        {
            var elementType = typeof(Unit);
            var inVertexType = typeof(Unit);
            var outVertexType = typeof(Unit);

            if (typeof(TTargetQuery) != typeof(IGremlinQuery))
            {
                if (!typeof(TTargetQuery).IsGenericType)
                    throw new NotSupportedException();

                var genericTypeDef = typeof(TTargetQuery).GetGenericTypeDefinition();

                if (genericTypeDef != typeof(IVGremlinQuery<>) && genericTypeDef != typeof(IEGremlinQuery<>) && genericTypeDef != typeof(IEGremlinQuery<,>) && genericTypeDef != typeof(IEGremlinQuery<,,>))
                    throw new NotSupportedException();

                elementType = typeof(TTargetQuery).GetGenericArguments()[0];

                if (genericTypeDef == typeof(IEGremlinQuery<,>) || genericTypeDef == typeof(IEGremlinQuery<,,>))
                    outVertexType = typeof(TTargetQuery).GetGenericArguments()[1];

                if (genericTypeDef == typeof(IEGremlinQuery<,,>))
                    inVertexType = typeof(TTargetQuery).GetGenericArguments()[2];
            }

            var type = typeof(GremlinQueryImpl<,,>).MakeGenericType(elementType, outVertexType, inVertexType);
            return (TTargetQuery)Activator.CreateInstance(type, this.Steps, this.StepLabelMappings);
        }

        public IImmutableList<GremlinStep> Steps { get; }
        public IImmutableDictionary<StepLabel, string> StepLabelMappings { get; }
    }

    internal static class GremlinQuery<TElement>
    {
        public static IGremlinQuery<TElement> Create(string graphName = "g")
        {
            return new GremlinQueryImpl<TElement, Unit, Unit>(ImmutableList<GremlinStep>.Empty.Add(new IdentifierGremlinStep(graphName)), ImmutableDictionary<StepLabel, string>.Empty);
        }
    }

    public static class GremlinQuery
    {
        public static readonly IGremlinQuery<Unit> Anonymous = GremlinQueryImpl<Unit, Unit, Unit>.Anonymous;

        public static IGremlinQuery<Unit> Create(string graphName)
        {
            return GremlinQuery<Unit>.Create(graphName);
        }

        public static IGremlinQuery<TElement> Create<TElement>(string graphName)
        {
            return GremlinQuery<TElement>.Create(graphName);
        }

        public static IGremlinQuery<TElement> Property<TElement, TProperty>(this IGremlinQuery<TElement> query, Expression<Func<TElement, TProperty>> propertyExpression, TProperty property)
        {
            if (propertyExpression.Body is MemberExpression memberExpression)
            {
                if (memberExpression.Expression == propertyExpression.Parameters[0])
                {
                    return query.Property(memberExpression.Member.Name, property);
                }
            }

            throw new NotSupportedException();
        }

        public static IGremlinQuery<TElement> SetModel<TElement>(this IGremlinQuery<TElement> query, IGraphModel model)
        {
            return query
                .AddStep(new SetModelGremlinStep(model));
        }

        public static IGremlinQuery<TElement> SetTypedGremlinQueryProvider<TElement>(this IGremlinQuery<TElement> query, ITypedGremlinQueryProvider queryProvider)
        {
            return query
                .AddStep(new SetTypedGremlinQueryProviderGremlinStep(queryProvider));
        }

        public static (string queryString, IDictionary<string, object> parameters) Serialize(this IGremlinQuery query)
        {
            var stringBuilder = new StringBuilder();

            var groovyBuilder = query
                .Serialize(stringBuilder, GroovyExpressionState.FromQuery(query));

            return (stringBuilder.ToString(), groovyBuilder.GetVariables());
        }

        public static Task<TElement> FirstAsync<TElement>(this IGremlinQuery<TElement> query, CancellationToken ct = default)
        {
            return query
                .Limit(1)
                .Execute()
                .First(ct);
        }

        public static async Task<Option<TElement>> FirstOrNoneAsync<TElement>(this IGremlinQuery<TElement> query, CancellationToken ct = default)
        {
            var array = await query
                .Limit(1)
                .Execute()
                .ToArray(ct)
                .ConfigureAwait(false);

            return array.Length > 0
                ? array[0]
                : Option<TElement>.None;
        }

        public static Task<TElement[]> ToArrayAsync<TElement>(this IGremlinQuery<TElement> query, CancellationToken ct = default)
        {
            return query
                .Execute()
                .ToArray(ct);
        }

        public static IGremlinQuery<TElement> Call<TElement>(this IGremlinQuery<TElement> query, string name, params object[] parameters)
        {
            return query.Call(name, parameters.ToImmutableList());
        }

        public static IGremlinQuery<TElement> Call<TElement>(this IGremlinQuery<TElement> query, string name, ImmutableList<object> parameters)
        {
            return query.InsertStep<TElement>(query.Steps.Count, new MethodGremlinStep(name, parameters));
        }

        internal static IGremlinQuery<TElement> AddStep<TElement>(this IGremlinQuery<TElement> query, GremlinStep step)
        {
            return query.InsertStep<TElement>(query.Steps.Count, step);
        }
        
        public static IGremlinQuery<TNewEdge> E<TNewEdge>(this IGremlinQuery query, params object[] ids)
        {
            return query
                .E(ids)
                .OfType<TNewEdge>();
        }

        public static IGremlinQuery<TElement> ReplaceSteps<TElement>(this IGremlinQuery<TElement> query, IImmutableList<GremlinStep> steps)
        {
            return ReferenceEquals(steps, query.Steps)
                ? query 
                : new GremlinQueryImpl<TElement, Unit, Unit>(steps, query.StepLabelMappings);
        }

        public static IGremlinQuery<TElement> Resolve<TElement>(this IGremlinQuery<TElement> query)
        {
            var model = query
                .TryGetModel()
                .IfNone(() => throw new ArgumentException("No IGraphModel set on the query."));

            return query
                .RewriteSteps(x => Option<IEnumerable<GremlinStep>>.Some(x.Resolve(model)))
                .Cast<TElement>();
        }

        public static IGremlinQuery<Unit> RewriteSteps(this IGremlinQuery query, Func<NonTerminalGremlinStep, Option<IEnumerable<GremlinStep>>> resolveFunction)
        {
            var steps = query.Steps;

            for(var index = 0; index < steps.Count; index++)
            {
                var step = steps[index];

                switch (step)
                {
                    case MethodGremlinStep terminal:
                    {
                        var parameters = terminal.Parameters;

                        for (var j = 0; j < parameters.Count; j++)
                        {
                            var parameter = parameters[j];

                            if (parameter is IGremlinQuery subQuery)
                                parameters = parameters.SetItem(j, subQuery.RewriteSteps(resolveFunction));
                        }

                        if (!object.ReferenceEquals(parameters, terminal.Parameters))
                            steps = steps.SetItem(index, new MethodGremlinStep(terminal.Name, parameters));

                        break;
                    }
                    case NonTerminalGremlinStep nonTerminal:
                    {
                        var newTuple = resolveFunction(nonTerminal)
                            .Fold(
                                (steps, index),
                                (tuple, newSteps) => (
                                    tuple.steps
                                        .RemoveAt(tuple.index)
                                        .InsertRange(tuple.index, newSteps),
                                    tuple.index - 1));

                        index = newTuple.index;
                        steps = newTuple.steps;

                        break;
                    }
                }
            }

            return query
                .Cast<Unit>()
                .ReplaceSteps(steps);
        }

        public static IVGremlinQuery<TNewVertex> V<TNewVertex>(this IGremlinQuery query, params object[] ids)
        {
            return query
                .V(ids)
                .OfType<TNewVertex>();
        }

        internal static IGremlinQuery<TElement> AddStepLabelBinding<TElement>(this IGremlinQuery<TElement> query, Expression<Func<TElement, object>> memberExpression, StepLabel stepLabel)
        {
            var body = memberExpression.Body.StripConvert();
            
            if (!(body is MemberExpression memberExpressionBody))
                throw new ArgumentException();

            return new GremlinQueryImpl<TElement, Unit, Unit>(query.Steps, query.StepLabelMappings.SetItem(stepLabel, memberExpressionBody.Member.Name));
        }

        internal static IGremlinQuery<TElement> ReplaceProvider<TElement>(this IGremlinQuery<TElement> query, ITypedGremlinQueryProvider provider)
        {
            return new GremlinQueryImpl<TElement, Unit, Unit>(query.Steps, query.StepLabelMappings);
        }

        public static IGremlinQuery<Unit> WithSubgraphStrategy(this IGremlinQuery<Unit> query, Func<IGremlinQuery<Unit>, IGremlinQuery> vertexCriterion, Func<IGremlinQuery<Unit>, IGremlinQuery> edgeCriterion)
        {
            var vertexCriterionTraversal = vertexCriterion(GremlinQuery.Anonymous);
            var edgeCriterionTraversal = edgeCriterion(GremlinQuery.Anonymous);

            if (vertexCriterionTraversal.Steps.Count > 1 || edgeCriterionTraversal.Steps.Count > 1)
            {
                var strategy = GremlinQuery
                    .Create("SubgraphStrategy")
                    .Call("build");

                if (vertexCriterionTraversal.Steps.Count > 0)
                    strategy = strategy.Call("vertices", vertexCriterionTraversal);

                if (edgeCriterionTraversal.Steps.Count > 0)
                    strategy = strategy.Call("edges", edgeCriterionTraversal);

                return query.AddStep(new MethodGremlinStep("withStrategies", strategy.Call("create")));
            }

            return query;
        }

        internal static Option<ITypedGremlinQueryProvider> TryGetTypedGremlinQueryProvider(this IGremlinQuery query)
        {
            return query
                .Steps
                .OfType<SetTypedGremlinQueryProviderGremlinStep>()
                .Select(x => Option<ITypedGremlinQueryProvider>.Some(x.TypedGremlinQueryProvider))
                .LastOrDefault();
        }

        internal static Option<IGraphModel> TryGetModel(this IGremlinQuery query)
        {
            return query
                .Steps
                .OfType<SetModelGremlinStep>()
                .Select(x => Option<IGraphModel>.Some(x.Model))
                .LastOrDefault();
        }
    }
}