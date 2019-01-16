﻿#pragma warning disable CS0109 // Member does not hide an inherited member; new keyword is not required
using System;
using System.Linq.Expressions;

namespace ExRam.Gremlinq.Core
{
    public partial interface IGremlinQuery
    {
        new IGremlinQuery<TResult> Cast<TResult>();
    }

    public partial interface IElementGremlinQuery
    {
        new IElementGremlinQuery<TResult> Cast<TResult>();
    }

    public partial interface IVertexGremlinQuery
    {
        new IVertexGremlinQuery<TResult> Cast<TResult>();
    }

    public partial interface IEdgeGremlinQuery
    {
        new IEdgeGremlinQuery<TResult> Cast<TResult>();
    }

    public partial interface IGremlinQuery
    {
        IGremlinQuery And(params Func<IGremlinQuery, IGremlinQuery>[] andTraversals);

        new IGremlinQuery As(params StepLabel[] stepLabels);

        new IGremlinQuery Barrier();

        TTargetQuery Choose<TTargetQuery>(Func<IGremlinQuery, IGremlinQuery> traversalPredicate, Func<IGremlinQuery, TTargetQuery> trueChoice, Func<IGremlinQuery, TTargetQuery> falseChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Choose<TTargetQuery>(Func<IGremlinQuery, IGremlinQuery> traversalPredicate, Func<IGremlinQuery, TTargetQuery> trueChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Coalesce<TTargetQuery>(params Func<IGremlinQuery, TTargetQuery>[] traversals) where TTargetQuery : IGremlinQuery;

        new IGremlinQuery Dedup();

        new IGremlinQuery Emit();

        new IGremlinQuery Filter(string lambda);
        TTargetQuery FlatMap<TTargetQuery>(Func<IGremlinQuery, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;

        new IGremlinQuery Identity();

        new IGremlinQuery Limit(long count);
        TTargetQuery Local<TTargetQuery>(Func<IGremlinQuery , TTargetQuery> localTraversal) where TTargetQuery : IGremlinQuery;

        TTargetQuery Map<TTargetQuery>(Func<IGremlinQuery, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;
        
        IGremlinQuery Not(Func<IGremlinQuery, IGremlinQuery> notTraversal);

        TTargetQuery Optional<TTargetQuery>(Func<IGremlinQuery, TTargetQuery> optionalTraversal) where TTargetQuery : IGremlinQuery;
        IGremlinQuery Or(params Func<IGremlinQuery, IGremlinQuery>[] orTraversals);

        new IGremlinQuery Range(long low, long high);

        TTargetQuery Repeat<TTargetQuery>(Func<IGremlinQuery, TTargetQuery> repeatTraversal) where TTargetQuery : IGremlinQuery;
        TTargetQuery RepeatUntil<TTargetQuery>(Func<IGremlinQuery, TTargetQuery> repeatTraversal, Func<IGremlinQuery, IGremlinQuery> untilTraversal) where TTargetQuery : IGremlinQuery;

        IGremlinQuery SideEffect(Func<IGremlinQuery, IGremlinQuery> sideEffectTraversal);
        new IGremlinQuery Skip(long count);

        new IGremlinQuery Tail(long count);
        new IGremlinQuery Times(int count);

        TTargetQuery Union<TTargetQuery>(params Func<IGremlinQuery, TTargetQuery>[] unionTraversals) where TTargetQuery : IGremlinQuery;
    }

    public partial interface IElementGremlinQuery
    {
        IElementGremlinQuery And(params Func<IElementGremlinQuery, IGremlinQuery>[] andTraversals);

        new IElementGremlinQuery As(params StepLabel[] stepLabels);

        new IElementGremlinQuery Barrier();

        TTargetQuery Choose<TTargetQuery>(Func<IElementGremlinQuery, IGremlinQuery> traversalPredicate, Func<IElementGremlinQuery, TTargetQuery> trueChoice, Func<IElementGremlinQuery, TTargetQuery> falseChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Choose<TTargetQuery>(Func<IElementGremlinQuery, IGremlinQuery> traversalPredicate, Func<IElementGremlinQuery, TTargetQuery> trueChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Coalesce<TTargetQuery>(params Func<IElementGremlinQuery, TTargetQuery>[] traversals) where TTargetQuery : IGremlinQuery;

        new IElementGremlinQuery Dedup();

        new IElementGremlinQuery Emit();

        new IElementGremlinQuery Filter(string lambda);
        TTargetQuery FlatMap<TTargetQuery>(Func<IElementGremlinQuery, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;

        new IElementGremlinQuery Identity();

        new IElementGremlinQuery Limit(long count);
        TTargetQuery Local<TTargetQuery>(Func<IElementGremlinQuery , TTargetQuery> localTraversal) where TTargetQuery : IGremlinQuery;

        TTargetQuery Map<TTargetQuery>(Func<IElementGremlinQuery, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;
        
        IElementGremlinQuery Not(Func<IElementGremlinQuery, IGremlinQuery> notTraversal);

        TTargetQuery Optional<TTargetQuery>(Func<IElementGremlinQuery, TTargetQuery> optionalTraversal) where TTargetQuery : IGremlinQuery;
        IElementGremlinQuery Or(params Func<IElementGremlinQuery, IGremlinQuery>[] orTraversals);

        new IElementGremlinQuery Range(long low, long high);

        TTargetQuery Repeat<TTargetQuery>(Func<IElementGremlinQuery, TTargetQuery> repeatTraversal) where TTargetQuery : IElementGremlinQuery;
        TTargetQuery RepeatUntil<TTargetQuery>(Func<IElementGremlinQuery, TTargetQuery> repeatTraversal, Func<IElementGremlinQuery, IGremlinQuery> untilTraversal) where TTargetQuery : IElementGremlinQuery;

        IElementGremlinQuery SideEffect(Func<IElementGremlinQuery, IGremlinQuery> sideEffectTraversal);
        new IElementGremlinQuery Skip(long count);

        new IElementGremlinQuery Tail(long count);
        new IElementGremlinQuery Times(int count);

        TTargetQuery Union<TTargetQuery>(params Func<IElementGremlinQuery, TTargetQuery>[] unionTraversals) where TTargetQuery : IGremlinQuery;
    }

    public partial interface IVertexGremlinQuery
    {
        IVertexGremlinQuery And(params Func<IVertexGremlinQuery, IGremlinQuery>[] andTraversals);

        new IVertexGremlinQuery As(params StepLabel[] stepLabels);

        new IVertexGremlinQuery Barrier();

        TTargetQuery Choose<TTargetQuery>(Func<IVertexGremlinQuery, IGremlinQuery> traversalPredicate, Func<IVertexGremlinQuery, TTargetQuery> trueChoice, Func<IVertexGremlinQuery, TTargetQuery> falseChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Choose<TTargetQuery>(Func<IVertexGremlinQuery, IGremlinQuery> traversalPredicate, Func<IVertexGremlinQuery, TTargetQuery> trueChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Coalesce<TTargetQuery>(params Func<IVertexGremlinQuery, TTargetQuery>[] traversals) where TTargetQuery : IGremlinQuery;

        new IVertexGremlinQuery Dedup();

        new IVertexGremlinQuery Emit();

        new IVertexGremlinQuery Filter(string lambda);
        TTargetQuery FlatMap<TTargetQuery>(Func<IVertexGremlinQuery, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;

        new IVertexGremlinQuery Identity();

        new IVertexGremlinQuery Limit(long count);
        TTargetQuery Local<TTargetQuery>(Func<IVertexGremlinQuery , TTargetQuery> localTraversal) where TTargetQuery : IGremlinQuery;

        TTargetQuery Map<TTargetQuery>(Func<IVertexGremlinQuery, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;
        
        IVertexGremlinQuery Not(Func<IVertexGremlinQuery, IGremlinQuery> notTraversal);

        TTargetQuery Optional<TTargetQuery>(Func<IVertexGremlinQuery, TTargetQuery> optionalTraversal) where TTargetQuery : IGremlinQuery;
        IVertexGremlinQuery Or(params Func<IVertexGremlinQuery, IGremlinQuery>[] orTraversals);

        new IVertexGremlinQuery Range(long low, long high);

        TTargetQuery Repeat<TTargetQuery>(Func<IVertexGremlinQuery, TTargetQuery> repeatTraversal) where TTargetQuery : IVertexGremlinQuery;
        TTargetQuery RepeatUntil<TTargetQuery>(Func<IVertexGremlinQuery, TTargetQuery> repeatTraversal, Func<IVertexGremlinQuery, IGremlinQuery> untilTraversal) where TTargetQuery : IVertexGremlinQuery;

        IVertexGremlinQuery SideEffect(Func<IVertexGremlinQuery, IGremlinQuery> sideEffectTraversal);
        new IVertexGremlinQuery Skip(long count);

        new IVertexGremlinQuery Tail(long count);
        new IVertexGremlinQuery Times(int count);

        TTargetQuery Union<TTargetQuery>(params Func<IVertexGremlinQuery, TTargetQuery>[] unionTraversals) where TTargetQuery : IGremlinQuery;
    }

    public partial interface IEdgeGremlinQuery
    {
        IEdgeGremlinQuery And(params Func<IEdgeGremlinQuery, IGremlinQuery>[] andTraversals);

        new IEdgeGremlinQuery As(params StepLabel[] stepLabels);

        new IEdgeGremlinQuery Barrier();

        TTargetQuery Choose<TTargetQuery>(Func<IEdgeGremlinQuery, IGremlinQuery> traversalPredicate, Func<IEdgeGremlinQuery, TTargetQuery> trueChoice, Func<IEdgeGremlinQuery, TTargetQuery> falseChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Choose<TTargetQuery>(Func<IEdgeGremlinQuery, IGremlinQuery> traversalPredicate, Func<IEdgeGremlinQuery, TTargetQuery> trueChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Coalesce<TTargetQuery>(params Func<IEdgeGremlinQuery, TTargetQuery>[] traversals) where TTargetQuery : IGremlinQuery;

        new IEdgeGremlinQuery Dedup();

        new IEdgeGremlinQuery Emit();

        new IEdgeGremlinQuery Filter(string lambda);
        TTargetQuery FlatMap<TTargetQuery>(Func<IEdgeGremlinQuery, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;

        new IEdgeGremlinQuery Identity();

        new IEdgeGremlinQuery Limit(long count);
        TTargetQuery Local<TTargetQuery>(Func<IEdgeGremlinQuery , TTargetQuery> localTraversal) where TTargetQuery : IGremlinQuery;

        TTargetQuery Map<TTargetQuery>(Func<IEdgeGremlinQuery, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;
        
        IEdgeGremlinQuery Not(Func<IEdgeGremlinQuery, IGremlinQuery> notTraversal);

        TTargetQuery Optional<TTargetQuery>(Func<IEdgeGremlinQuery, TTargetQuery> optionalTraversal) where TTargetQuery : IGremlinQuery;
        IEdgeGremlinQuery Or(params Func<IEdgeGremlinQuery, IGremlinQuery>[] orTraversals);

        new IEdgeGremlinQuery Range(long low, long high);

        TTargetQuery Repeat<TTargetQuery>(Func<IEdgeGremlinQuery, TTargetQuery> repeatTraversal) where TTargetQuery : IEdgeGremlinQuery;
        TTargetQuery RepeatUntil<TTargetQuery>(Func<IEdgeGremlinQuery, TTargetQuery> repeatTraversal, Func<IEdgeGremlinQuery, IGremlinQuery> untilTraversal) where TTargetQuery : IEdgeGremlinQuery;

        IEdgeGremlinQuery SideEffect(Func<IEdgeGremlinQuery, IGremlinQuery> sideEffectTraversal);
        new IEdgeGremlinQuery Skip(long count);

        new IEdgeGremlinQuery Tail(long count);
        new IEdgeGremlinQuery Times(int count);

        TTargetQuery Union<TTargetQuery>(params Func<IEdgeGremlinQuery, TTargetQuery>[] unionTraversals) where TTargetQuery : IGremlinQuery;
    }

    public partial interface IGremlinQuery<TElement>
    {
        IGremlinQuery<TElement> And(params Func<IGremlinQuery<TElement>, IGremlinQuery>[] andTraversals);

        new IGremlinQuery<TElement> As(params StepLabel[] stepLabels);

        new IGremlinQuery<TElement> Barrier();

        TTargetQuery Choose<TTargetQuery>(Func<IGremlinQuery<TElement>, IGremlinQuery> traversalPredicate, Func<IGremlinQuery<TElement>, TTargetQuery> trueChoice, Func<IGremlinQuery<TElement>, TTargetQuery> falseChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Choose<TTargetQuery>(Func<IGremlinQuery<TElement>, IGremlinQuery> traversalPredicate, Func<IGremlinQuery<TElement>, TTargetQuery> trueChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Coalesce<TTargetQuery>(params Func<IGremlinQuery<TElement>, TTargetQuery>[] traversals) where TTargetQuery : IGremlinQuery;

        new IGremlinQuery<TElement> Dedup();

        new IGremlinQuery<TElement> Emit();

        new IGremlinQuery<TElement> Filter(string lambda);
        TTargetQuery FlatMap<TTargetQuery>(Func<IGremlinQuery<TElement>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;

        new IGremlinQuery<TElement> Identity();

        new IGremlinQuery<TElement> Limit(long count);
        TTargetQuery Local<TTargetQuery>(Func<IGremlinQuery<TElement> , TTargetQuery> localTraversal) where TTargetQuery : IGremlinQuery;

        TTargetQuery Map<TTargetQuery>(Func<IGremlinQuery<TElement>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;
        
        IGremlinQuery<TElement> Not(Func<IGremlinQuery<TElement>, IGremlinQuery> notTraversal);

        TTargetQuery Optional<TTargetQuery>(Func<IGremlinQuery<TElement>, TTargetQuery> optionalTraversal) where TTargetQuery : IGremlinQuery;
        IGremlinQuery<TElement> Or(params Func<IGremlinQuery<TElement>, IGremlinQuery>[] orTraversals);

        new IGremlinQuery<TElement> Range(long low, long high);

        TTargetQuery Repeat<TTargetQuery>(Func<IGremlinQuery<TElement>, TTargetQuery> repeatTraversal) where TTargetQuery : IGremlinQuery<TElement>;
        TTargetQuery RepeatUntil<TTargetQuery>(Func<IGremlinQuery<TElement>, TTargetQuery> repeatTraversal, Func<IGremlinQuery<TElement>, IGremlinQuery> untilTraversal) where TTargetQuery : IGremlinQuery<TElement>;

        IGremlinQuery<TElement> SideEffect(Func<IGremlinQuery<TElement>, IGremlinQuery> sideEffectTraversal);
        new IGremlinQuery<TElement> Skip(long count);

        new IGremlinQuery<TElement> Tail(long count);
        new IGremlinQuery<TElement> Times(int count);

        TTargetQuery Union<TTargetQuery>(params Func<IGremlinQuery<TElement>, TTargetQuery>[] unionTraversals) where TTargetQuery : IGremlinQuery;
    }

    public partial interface IValueGremlinQuery<TElement>
    {
        IValueGremlinQuery<TElement> And(params Func<IValueGremlinQuery<TElement>, IGremlinQuery>[] andTraversals);

        new IValueGremlinQuery<TElement> As(params StepLabel[] stepLabels);

        new IValueGremlinQuery<TElement> Barrier();

        TTargetQuery Choose<TTargetQuery>(Func<IValueGremlinQuery<TElement>, IGremlinQuery> traversalPredicate, Func<IValueGremlinQuery<TElement>, TTargetQuery> trueChoice, Func<IValueGremlinQuery<TElement>, TTargetQuery> falseChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Choose<TTargetQuery>(Func<IValueGremlinQuery<TElement>, IGremlinQuery> traversalPredicate, Func<IValueGremlinQuery<TElement>, TTargetQuery> trueChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Coalesce<TTargetQuery>(params Func<IValueGremlinQuery<TElement>, TTargetQuery>[] traversals) where TTargetQuery : IGremlinQuery;

        new IValueGremlinQuery<TElement> Dedup();

        new IValueGremlinQuery<TElement> Emit();

        new IValueGremlinQuery<TElement> Filter(string lambda);
        TTargetQuery FlatMap<TTargetQuery>(Func<IValueGremlinQuery<TElement>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;

        new IValueGremlinQuery<TElement> Identity();

        new IValueGremlinQuery<TElement> Limit(long count);
        TTargetQuery Local<TTargetQuery>(Func<IValueGremlinQuery<TElement> , TTargetQuery> localTraversal) where TTargetQuery : IGremlinQuery;

        TTargetQuery Map<TTargetQuery>(Func<IValueGremlinQuery<TElement>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;
        
        IValueGremlinQuery<TElement> Not(Func<IValueGremlinQuery<TElement>, IGremlinQuery> notTraversal);

        TTargetQuery Optional<TTargetQuery>(Func<IValueGremlinQuery<TElement>, TTargetQuery> optionalTraversal) where TTargetQuery : IGremlinQuery;
        IValueGremlinQuery<TElement> Or(params Func<IValueGremlinQuery<TElement>, IGremlinQuery>[] orTraversals);

        new IValueGremlinQuery<TElement> Range(long low, long high);

        TTargetQuery Repeat<TTargetQuery>(Func<IValueGremlinQuery<TElement>, TTargetQuery> repeatTraversal) where TTargetQuery : IValueGremlinQuery<TElement>;
        TTargetQuery RepeatUntil<TTargetQuery>(Func<IValueGremlinQuery<TElement>, TTargetQuery> repeatTraversal, Func<IValueGremlinQuery<TElement>, IGremlinQuery> untilTraversal) where TTargetQuery : IValueGremlinQuery<TElement>;

        IValueGremlinQuery<TElement> SideEffect(Func<IValueGremlinQuery<TElement>, IGremlinQuery> sideEffectTraversal);
        new IValueGremlinQuery<TElement> Skip(long count);

        new IValueGremlinQuery<TElement> Tail(long count);
        new IValueGremlinQuery<TElement> Times(int count);

        TTargetQuery Union<TTargetQuery>(params Func<IValueGremlinQuery<TElement>, TTargetQuery>[] unionTraversals) where TTargetQuery : IGremlinQuery;
    }

    public partial interface IArrayGremlinQuery<TArray, TQuery>
    {
        IArrayGremlinQuery<TArray, TQuery> And(params Func<IArrayGremlinQuery<TArray, TQuery>, IGremlinQuery>[] andTraversals);

        new IArrayGremlinQuery<TArray, TQuery> As(params StepLabel[] stepLabels);

        new IArrayGremlinQuery<TArray, TQuery> Barrier();

        TTargetQuery Choose<TTargetQuery>(Func<IArrayGremlinQuery<TArray, TQuery>, IGremlinQuery> traversalPredicate, Func<IArrayGremlinQuery<TArray, TQuery>, TTargetQuery> trueChoice, Func<IArrayGremlinQuery<TArray, TQuery>, TTargetQuery> falseChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Choose<TTargetQuery>(Func<IArrayGremlinQuery<TArray, TQuery>, IGremlinQuery> traversalPredicate, Func<IArrayGremlinQuery<TArray, TQuery>, TTargetQuery> trueChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Coalesce<TTargetQuery>(params Func<IArrayGremlinQuery<TArray, TQuery>, TTargetQuery>[] traversals) where TTargetQuery : IGremlinQuery;

        new IArrayGremlinQuery<TArray, TQuery> Dedup();

        new IArrayGremlinQuery<TArray, TQuery> Emit();

        new IArrayGremlinQuery<TArray, TQuery> Filter(string lambda);
        TTargetQuery FlatMap<TTargetQuery>(Func<IArrayGremlinQuery<TArray, TQuery>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;

        new IArrayGremlinQuery<TArray, TQuery> Identity();

        new IArrayGremlinQuery<TArray, TQuery> Limit(long count);
        TTargetQuery Local<TTargetQuery>(Func<IArrayGremlinQuery<TArray, TQuery> , TTargetQuery> localTraversal) where TTargetQuery : IGremlinQuery;

        TTargetQuery Map<TTargetQuery>(Func<IArrayGremlinQuery<TArray, TQuery>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;
        
        IArrayGremlinQuery<TArray, TQuery> Not(Func<IArrayGremlinQuery<TArray, TQuery>, IGremlinQuery> notTraversal);

        TTargetQuery Optional<TTargetQuery>(Func<IArrayGremlinQuery<TArray, TQuery>, TTargetQuery> optionalTraversal) where TTargetQuery : IGremlinQuery;
        IArrayGremlinQuery<TArray, TQuery> Or(params Func<IArrayGremlinQuery<TArray, TQuery>, IGremlinQuery>[] orTraversals);

        new IArrayGremlinQuery<TArray, TQuery> Range(long low, long high);

        TTargetQuery Repeat<TTargetQuery>(Func<IArrayGremlinQuery<TArray, TQuery>, TTargetQuery> repeatTraversal) where TTargetQuery : IArrayGremlinQuery<TArray, TQuery>;
        TTargetQuery RepeatUntil<TTargetQuery>(Func<IArrayGremlinQuery<TArray, TQuery>, TTargetQuery> repeatTraversal, Func<IArrayGremlinQuery<TArray, TQuery>, IGremlinQuery> untilTraversal) where TTargetQuery : IArrayGremlinQuery<TArray, TQuery>;

        IArrayGremlinQuery<TArray, TQuery> SideEffect(Func<IArrayGremlinQuery<TArray, TQuery>, IGremlinQuery> sideEffectTraversal);
        new IArrayGremlinQuery<TArray, TQuery> Skip(long count);

        new IArrayGremlinQuery<TArray, TQuery> Tail(long count);
        new IArrayGremlinQuery<TArray, TQuery> Times(int count);

        TTargetQuery Union<TTargetQuery>(params Func<IArrayGremlinQuery<TArray, TQuery>, TTargetQuery>[] unionTraversals) where TTargetQuery : IGremlinQuery;
    }

    public partial interface IElementGremlinQuery<TElement>
    {
        IElementGremlinQuery<TElement> And(params Func<IElementGremlinQuery<TElement>, IGremlinQuery>[] andTraversals);

        new IElementGremlinQuery<TElement> As(params StepLabel[] stepLabels);

        new IElementGremlinQuery<TElement> Barrier();

        TTargetQuery Choose<TTargetQuery>(Func<IElementGremlinQuery<TElement>, IGremlinQuery> traversalPredicate, Func<IElementGremlinQuery<TElement>, TTargetQuery> trueChoice, Func<IElementGremlinQuery<TElement>, TTargetQuery> falseChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Choose<TTargetQuery>(Func<IElementGremlinQuery<TElement>, IGremlinQuery> traversalPredicate, Func<IElementGremlinQuery<TElement>, TTargetQuery> trueChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Coalesce<TTargetQuery>(params Func<IElementGremlinQuery<TElement>, TTargetQuery>[] traversals) where TTargetQuery : IGremlinQuery;

        new IElementGremlinQuery<TElement> Dedup();

        new IElementGremlinQuery<TElement> Emit();

        new IElementGremlinQuery<TElement> Filter(string lambda);
        TTargetQuery FlatMap<TTargetQuery>(Func<IElementGremlinQuery<TElement>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;

        new IElementGremlinQuery<TElement> Identity();

        new IElementGremlinQuery<TElement> Limit(long count);
        TTargetQuery Local<TTargetQuery>(Func<IElementGremlinQuery<TElement> , TTargetQuery> localTraversal) where TTargetQuery : IGremlinQuery;

        TTargetQuery Map<TTargetQuery>(Func<IElementGremlinQuery<TElement>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;
        
        IElementGremlinQuery<TElement> Not(Func<IElementGremlinQuery<TElement>, IGremlinQuery> notTraversal);

        TTargetQuery Optional<TTargetQuery>(Func<IElementGremlinQuery<TElement>, TTargetQuery> optionalTraversal) where TTargetQuery : IGremlinQuery;
        IElementGremlinQuery<TElement> Or(params Func<IElementGremlinQuery<TElement>, IGremlinQuery>[] orTraversals);

        new IElementGremlinQuery<TElement> Range(long low, long high);

        TTargetQuery Repeat<TTargetQuery>(Func<IElementGremlinQuery<TElement>, TTargetQuery> repeatTraversal) where TTargetQuery : IElementGremlinQuery<TElement>;
        TTargetQuery RepeatUntil<TTargetQuery>(Func<IElementGremlinQuery<TElement>, TTargetQuery> repeatTraversal, Func<IElementGremlinQuery<TElement>, IGremlinQuery> untilTraversal) where TTargetQuery : IElementGremlinQuery<TElement>;

        IElementGremlinQuery<TElement> SideEffect(Func<IElementGremlinQuery<TElement>, IGremlinQuery> sideEffectTraversal);
        new IElementGremlinQuery<TElement> Skip(long count);

        new IElementGremlinQuery<TElement> Tail(long count);
        new IElementGremlinQuery<TElement> Times(int count);

        TTargetQuery Union<TTargetQuery>(params Func<IElementGremlinQuery<TElement>, TTargetQuery>[] unionTraversals) where TTargetQuery : IGremlinQuery;
    }

    public partial interface IVertexGremlinQuery<TVertex>
    {
        IVertexGremlinQuery<TVertex> And(params Func<IVertexGremlinQuery<TVertex>, IGremlinQuery>[] andTraversals);

        new IVertexGremlinQuery<TVertex> As(params StepLabel[] stepLabels);

        new IVertexGremlinQuery<TVertex> Barrier();

        TTargetQuery Choose<TTargetQuery>(Func<IVertexGremlinQuery<TVertex>, IGremlinQuery> traversalPredicate, Func<IVertexGremlinQuery<TVertex>, TTargetQuery> trueChoice, Func<IVertexGremlinQuery<TVertex>, TTargetQuery> falseChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Choose<TTargetQuery>(Func<IVertexGremlinQuery<TVertex>, IGremlinQuery> traversalPredicate, Func<IVertexGremlinQuery<TVertex>, TTargetQuery> trueChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Coalesce<TTargetQuery>(params Func<IVertexGremlinQuery<TVertex>, TTargetQuery>[] traversals) where TTargetQuery : IGremlinQuery;

        new IVertexGremlinQuery<TVertex> Dedup();

        new IVertexGremlinQuery<TVertex> Emit();

        new IVertexGremlinQuery<TVertex> Filter(string lambda);
        TTargetQuery FlatMap<TTargetQuery>(Func<IVertexGremlinQuery<TVertex>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;

        new IVertexGremlinQuery<TVertex> Identity();

        new IVertexGremlinQuery<TVertex> Limit(long count);
        TTargetQuery Local<TTargetQuery>(Func<IVertexGremlinQuery<TVertex> , TTargetQuery> localTraversal) where TTargetQuery : IGremlinQuery;

        TTargetQuery Map<TTargetQuery>(Func<IVertexGremlinQuery<TVertex>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;
        
        IVertexGremlinQuery<TVertex> Not(Func<IVertexGremlinQuery<TVertex>, IGremlinQuery> notTraversal);

        TTargetQuery Optional<TTargetQuery>(Func<IVertexGremlinQuery<TVertex>, TTargetQuery> optionalTraversal) where TTargetQuery : IGremlinQuery;
        IVertexGremlinQuery<TVertex> Or(params Func<IVertexGremlinQuery<TVertex>, IGremlinQuery>[] orTraversals);

        new IVertexGremlinQuery<TVertex> Range(long low, long high);

        TTargetQuery Repeat<TTargetQuery>(Func<IVertexGremlinQuery<TVertex>, TTargetQuery> repeatTraversal) where TTargetQuery : IVertexGremlinQuery<TVertex>;
        TTargetQuery RepeatUntil<TTargetQuery>(Func<IVertexGremlinQuery<TVertex>, TTargetQuery> repeatTraversal, Func<IVertexGremlinQuery<TVertex>, IGremlinQuery> untilTraversal) where TTargetQuery : IVertexGremlinQuery<TVertex>;

        IVertexGremlinQuery<TVertex> SideEffect(Func<IVertexGremlinQuery<TVertex>, IGremlinQuery> sideEffectTraversal);
        new IVertexGremlinQuery<TVertex> Skip(long count);

        new IVertexGremlinQuery<TVertex> Tail(long count);
        new IVertexGremlinQuery<TVertex> Times(int count);

        TTargetQuery Union<TTargetQuery>(params Func<IVertexGremlinQuery<TVertex>, TTargetQuery>[] unionTraversals) where TTargetQuery : IGremlinQuery;
    }

    public partial interface IEdgeGremlinQuery<TEdge>
    {
        IEdgeGremlinQuery<TEdge> And(params Func<IEdgeGremlinQuery<TEdge>, IGremlinQuery>[] andTraversals);

        new IEdgeGremlinQuery<TEdge> As(params StepLabel[] stepLabels);

        new IEdgeGremlinQuery<TEdge> Barrier();

        TTargetQuery Choose<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge>, IGremlinQuery> traversalPredicate, Func<IEdgeGremlinQuery<TEdge>, TTargetQuery> trueChoice, Func<IEdgeGremlinQuery<TEdge>, TTargetQuery> falseChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Choose<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge>, IGremlinQuery> traversalPredicate, Func<IEdgeGremlinQuery<TEdge>, TTargetQuery> trueChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Coalesce<TTargetQuery>(params Func<IEdgeGremlinQuery<TEdge>, TTargetQuery>[] traversals) where TTargetQuery : IGremlinQuery;

        new IEdgeGremlinQuery<TEdge> Dedup();

        new IEdgeGremlinQuery<TEdge> Emit();

        new IEdgeGremlinQuery<TEdge> Filter(string lambda);
        TTargetQuery FlatMap<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;

        new IEdgeGremlinQuery<TEdge> Identity();

        new IEdgeGremlinQuery<TEdge> Limit(long count);
        TTargetQuery Local<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge> , TTargetQuery> localTraversal) where TTargetQuery : IGremlinQuery;

        TTargetQuery Map<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;
        
        IEdgeGremlinQuery<TEdge> Not(Func<IEdgeGremlinQuery<TEdge>, IGremlinQuery> notTraversal);

        TTargetQuery Optional<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge>, TTargetQuery> optionalTraversal) where TTargetQuery : IGremlinQuery;
        IEdgeGremlinQuery<TEdge> Or(params Func<IEdgeGremlinQuery<TEdge>, IGremlinQuery>[] orTraversals);

        new IEdgeGremlinQuery<TEdge> Range(long low, long high);

        TTargetQuery Repeat<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge>, TTargetQuery> repeatTraversal) where TTargetQuery : IEdgeGremlinQuery<TEdge>;
        TTargetQuery RepeatUntil<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge>, TTargetQuery> repeatTraversal, Func<IEdgeGremlinQuery<TEdge>, IGremlinQuery> untilTraversal) where TTargetQuery : IEdgeGremlinQuery<TEdge>;

        IEdgeGremlinQuery<TEdge> SideEffect(Func<IEdgeGremlinQuery<TEdge>, IGremlinQuery> sideEffectTraversal);
        new IEdgeGremlinQuery<TEdge> Skip(long count);

        new IEdgeGremlinQuery<TEdge> Tail(long count);
        new IEdgeGremlinQuery<TEdge> Times(int count);

        TTargetQuery Union<TTargetQuery>(params Func<IEdgeGremlinQuery<TEdge>, TTargetQuery>[] unionTraversals) where TTargetQuery : IGremlinQuery;
    }

    public partial interface IEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {
        IEdgeGremlinQuery<TEdge, TAdjacentVertex> And(params Func<IEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery>[] andTraversals);

        new IEdgeGremlinQuery<TEdge, TAdjacentVertex> As(params StepLabel[] stepLabels);

        new IEdgeGremlinQuery<TEdge, TAdjacentVertex> Barrier();

        TTargetQuery Choose<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> traversalPredicate, Func<IEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery> trueChoice, Func<IEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery> falseChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Choose<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> traversalPredicate, Func<IEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery> trueChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Coalesce<TTargetQuery>(params Func<IEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery>[] traversals) where TTargetQuery : IGremlinQuery;

        new IEdgeGremlinQuery<TEdge, TAdjacentVertex> Dedup();

        new IEdgeGremlinQuery<TEdge, TAdjacentVertex> Emit();

        new IEdgeGremlinQuery<TEdge, TAdjacentVertex> Filter(string lambda);
        TTargetQuery FlatMap<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;

        new IEdgeGremlinQuery<TEdge, TAdjacentVertex> Identity();

        new IEdgeGremlinQuery<TEdge, TAdjacentVertex> Limit(long count);
        TTargetQuery Local<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge, TAdjacentVertex> , TTargetQuery> localTraversal) where TTargetQuery : IGremlinQuery;

        TTargetQuery Map<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;
        
        IEdgeGremlinQuery<TEdge, TAdjacentVertex> Not(Func<IEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> notTraversal);

        TTargetQuery Optional<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery> optionalTraversal) where TTargetQuery : IGremlinQuery;
        IEdgeGremlinQuery<TEdge, TAdjacentVertex> Or(params Func<IEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery>[] orTraversals);

        new IEdgeGremlinQuery<TEdge, TAdjacentVertex> Range(long low, long high);

        TTargetQuery Repeat<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery> repeatTraversal) where TTargetQuery : IEdgeGremlinQuery<TEdge, TAdjacentVertex>;
        TTargetQuery RepeatUntil<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery> repeatTraversal, Func<IEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> untilTraversal) where TTargetQuery : IEdgeGremlinQuery<TEdge, TAdjacentVertex>;

        IEdgeGremlinQuery<TEdge, TAdjacentVertex> SideEffect(Func<IEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> sideEffectTraversal);
        new IEdgeGremlinQuery<TEdge, TAdjacentVertex> Skip(long count);

        new IEdgeGremlinQuery<TEdge, TAdjacentVertex> Tail(long count);
        new IEdgeGremlinQuery<TEdge, TAdjacentVertex> Times(int count);

        TTargetQuery Union<TTargetQuery>(params Func<IEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery>[] unionTraversals) where TTargetQuery : IGremlinQuery;
    }

    public partial interface IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>
    {
        IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> And(params Func<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, IGremlinQuery>[] andTraversals);

        new IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> As(params StepLabel[] stepLabels);

        new IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> Barrier();

        TTargetQuery Choose<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, IGremlinQuery> traversalPredicate, Func<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, TTargetQuery> trueChoice, Func<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, TTargetQuery> falseChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Choose<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, IGremlinQuery> traversalPredicate, Func<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, TTargetQuery> trueChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Coalesce<TTargetQuery>(params Func<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, TTargetQuery>[] traversals) where TTargetQuery : IGremlinQuery;

        new IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> Dedup();

        new IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> Emit();

        new IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> Filter(string lambda);
        TTargetQuery FlatMap<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;

        new IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> Identity();

        new IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> Limit(long count);
        TTargetQuery Local<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> , TTargetQuery> localTraversal) where TTargetQuery : IGremlinQuery;

        TTargetQuery Map<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;
        
        IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> Not(Func<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, IGremlinQuery> notTraversal);

        TTargetQuery Optional<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, TTargetQuery> optionalTraversal) where TTargetQuery : IGremlinQuery;
        IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> Or(params Func<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, IGremlinQuery>[] orTraversals);

        new IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> Range(long low, long high);

        TTargetQuery Repeat<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, TTargetQuery> repeatTraversal) where TTargetQuery : IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>;
        TTargetQuery RepeatUntil<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, TTargetQuery> repeatTraversal, Func<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, IGremlinQuery> untilTraversal) where TTargetQuery : IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>;

        IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> SideEffect(Func<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, IGremlinQuery> sideEffectTraversal);
        new IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> Skip(long count);

        new IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> Tail(long count);
        new IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> Times(int count);

        TTargetQuery Union<TTargetQuery>(params Func<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, TTargetQuery>[] unionTraversals) where TTargetQuery : IGremlinQuery;
    }

    public partial interface IInEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {
        IInEdgeGremlinQuery<TEdge, TAdjacentVertex> And(params Func<IInEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery>[] andTraversals);

        new IInEdgeGremlinQuery<TEdge, TAdjacentVertex> As(params StepLabel[] stepLabels);

        new IInEdgeGremlinQuery<TEdge, TAdjacentVertex> Barrier();

        TTargetQuery Choose<TTargetQuery>(Func<IInEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> traversalPredicate, Func<IInEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery> trueChoice, Func<IInEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery> falseChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Choose<TTargetQuery>(Func<IInEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> traversalPredicate, Func<IInEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery> trueChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Coalesce<TTargetQuery>(params Func<IInEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery>[] traversals) where TTargetQuery : IGremlinQuery;

        new IInEdgeGremlinQuery<TEdge, TAdjacentVertex> Dedup();

        new IInEdgeGremlinQuery<TEdge, TAdjacentVertex> Emit();

        new IInEdgeGremlinQuery<TEdge, TAdjacentVertex> Filter(string lambda);
        TTargetQuery FlatMap<TTargetQuery>(Func<IInEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;

        new IInEdgeGremlinQuery<TEdge, TAdjacentVertex> Identity();

        new IInEdgeGremlinQuery<TEdge, TAdjacentVertex> Limit(long count);
        TTargetQuery Local<TTargetQuery>(Func<IInEdgeGremlinQuery<TEdge, TAdjacentVertex> , TTargetQuery> localTraversal) where TTargetQuery : IGremlinQuery;

        TTargetQuery Map<TTargetQuery>(Func<IInEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;
        
        IInEdgeGremlinQuery<TEdge, TAdjacentVertex> Not(Func<IInEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> notTraversal);

        TTargetQuery Optional<TTargetQuery>(Func<IInEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery> optionalTraversal) where TTargetQuery : IGremlinQuery;
        IInEdgeGremlinQuery<TEdge, TAdjacentVertex> Or(params Func<IInEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery>[] orTraversals);

        new IInEdgeGremlinQuery<TEdge, TAdjacentVertex> Range(long low, long high);

        TTargetQuery Repeat<TTargetQuery>(Func<IInEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery> repeatTraversal) where TTargetQuery : IInEdgeGremlinQuery<TEdge, TAdjacentVertex>;
        TTargetQuery RepeatUntil<TTargetQuery>(Func<IInEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery> repeatTraversal, Func<IInEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> untilTraversal) where TTargetQuery : IInEdgeGremlinQuery<TEdge, TAdjacentVertex>;

        IInEdgeGremlinQuery<TEdge, TAdjacentVertex> SideEffect(Func<IInEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> sideEffectTraversal);
        new IInEdgeGremlinQuery<TEdge, TAdjacentVertex> Skip(long count);

        new IInEdgeGremlinQuery<TEdge, TAdjacentVertex> Tail(long count);
        new IInEdgeGremlinQuery<TEdge, TAdjacentVertex> Times(int count);

        TTargetQuery Union<TTargetQuery>(params Func<IInEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery>[] unionTraversals) where TTargetQuery : IGremlinQuery;
    }

    public partial interface IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {
        IOutEdgeGremlinQuery<TEdge, TAdjacentVertex> And(params Func<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery>[] andTraversals);

        new IOutEdgeGremlinQuery<TEdge, TAdjacentVertex> As(params StepLabel[] stepLabels);

        new IOutEdgeGremlinQuery<TEdge, TAdjacentVertex> Barrier();

        TTargetQuery Choose<TTargetQuery>(Func<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> traversalPredicate, Func<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery> trueChoice, Func<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery> falseChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Choose<TTargetQuery>(Func<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> traversalPredicate, Func<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery> trueChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Coalesce<TTargetQuery>(params Func<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery>[] traversals) where TTargetQuery : IGremlinQuery;

        new IOutEdgeGremlinQuery<TEdge, TAdjacentVertex> Dedup();

        new IOutEdgeGremlinQuery<TEdge, TAdjacentVertex> Emit();

        new IOutEdgeGremlinQuery<TEdge, TAdjacentVertex> Filter(string lambda);
        TTargetQuery FlatMap<TTargetQuery>(Func<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;

        new IOutEdgeGremlinQuery<TEdge, TAdjacentVertex> Identity();

        new IOutEdgeGremlinQuery<TEdge, TAdjacentVertex> Limit(long count);
        TTargetQuery Local<TTargetQuery>(Func<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex> , TTargetQuery> localTraversal) where TTargetQuery : IGremlinQuery;

        TTargetQuery Map<TTargetQuery>(Func<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;
        
        IOutEdgeGremlinQuery<TEdge, TAdjacentVertex> Not(Func<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> notTraversal);

        TTargetQuery Optional<TTargetQuery>(Func<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery> optionalTraversal) where TTargetQuery : IGremlinQuery;
        IOutEdgeGremlinQuery<TEdge, TAdjacentVertex> Or(params Func<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery>[] orTraversals);

        new IOutEdgeGremlinQuery<TEdge, TAdjacentVertex> Range(long low, long high);

        TTargetQuery Repeat<TTargetQuery>(Func<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery> repeatTraversal) where TTargetQuery : IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>;
        TTargetQuery RepeatUntil<TTargetQuery>(Func<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery> repeatTraversal, Func<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> untilTraversal) where TTargetQuery : IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>;

        IOutEdgeGremlinQuery<TEdge, TAdjacentVertex> SideEffect(Func<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> sideEffectTraversal);
        new IOutEdgeGremlinQuery<TEdge, TAdjacentVertex> Skip(long count);

        new IOutEdgeGremlinQuery<TEdge, TAdjacentVertex> Tail(long count);
        new IOutEdgeGremlinQuery<TEdge, TAdjacentVertex> Times(int count);

        TTargetQuery Union<TTargetQuery>(params Func<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, TTargetQuery>[] unionTraversals) where TTargetQuery : IGremlinQuery;
    }

    public partial interface IVertexPropertyGremlinQuery<TValue>
    {
        IVertexPropertyGremlinQuery<TValue> And(params Func<IVertexPropertyGremlinQuery<TValue>, IGremlinQuery>[] andTraversals);

        new IVertexPropertyGremlinQuery<TValue> As(params StepLabel[] stepLabels);

        new IVertexPropertyGremlinQuery<TValue> Barrier();

        TTargetQuery Choose<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TValue>, IGremlinQuery> traversalPredicate, Func<IVertexPropertyGremlinQuery<TValue>, TTargetQuery> trueChoice, Func<IVertexPropertyGremlinQuery<TValue>, TTargetQuery> falseChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Choose<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TValue>, IGremlinQuery> traversalPredicate, Func<IVertexPropertyGremlinQuery<TValue>, TTargetQuery> trueChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Coalesce<TTargetQuery>(params Func<IVertexPropertyGremlinQuery<TValue>, TTargetQuery>[] traversals) where TTargetQuery : IGremlinQuery;

        new IVertexPropertyGremlinQuery<TValue> Dedup();

        new IVertexPropertyGremlinQuery<TValue> Emit();

        new IVertexPropertyGremlinQuery<TValue> Filter(string lambda);
        TTargetQuery FlatMap<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TValue>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;

        new IVertexPropertyGremlinQuery<TValue> Identity();

        new IVertexPropertyGremlinQuery<TValue> Limit(long count);
        TTargetQuery Local<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TValue> , TTargetQuery> localTraversal) where TTargetQuery : IGremlinQuery;

        TTargetQuery Map<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TValue>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;
        
        IVertexPropertyGremlinQuery<TValue> Not(Func<IVertexPropertyGremlinQuery<TValue>, IGremlinQuery> notTraversal);

        TTargetQuery Optional<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TValue>, TTargetQuery> optionalTraversal) where TTargetQuery : IGremlinQuery;
        IVertexPropertyGremlinQuery<TValue> Or(params Func<IVertexPropertyGremlinQuery<TValue>, IGremlinQuery>[] orTraversals);

        new IVertexPropertyGremlinQuery<TValue> Range(long low, long high);

        TTargetQuery Repeat<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TValue>, TTargetQuery> repeatTraversal) where TTargetQuery : IVertexPropertyGremlinQuery<TValue>;
        TTargetQuery RepeatUntil<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TValue>, TTargetQuery> repeatTraversal, Func<IVertexPropertyGremlinQuery<TValue>, IGremlinQuery> untilTraversal) where TTargetQuery : IVertexPropertyGremlinQuery<TValue>;

        IVertexPropertyGremlinQuery<TValue> SideEffect(Func<IVertexPropertyGremlinQuery<TValue>, IGremlinQuery> sideEffectTraversal);
        new IVertexPropertyGremlinQuery<TValue> Skip(long count);

        new IVertexPropertyGremlinQuery<TValue> Tail(long count);
        new IVertexPropertyGremlinQuery<TValue> Times(int count);

        TTargetQuery Union<TTargetQuery>(params Func<IVertexPropertyGremlinQuery<TValue>, TTargetQuery>[] unionTraversals) where TTargetQuery : IGremlinQuery;
    }

    public partial interface IVertexPropertyGremlinQuery<TValue, TMeta>
    {
        IVertexPropertyGremlinQuery<TValue, TMeta> And(params Func<IVertexPropertyGremlinQuery<TValue, TMeta>, IGremlinQuery>[] andTraversals);

        new IVertexPropertyGremlinQuery<TValue, TMeta> As(params StepLabel[] stepLabels);

        new IVertexPropertyGremlinQuery<TValue, TMeta> Barrier();

        TTargetQuery Choose<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TValue, TMeta>, IGremlinQuery> traversalPredicate, Func<IVertexPropertyGremlinQuery<TValue, TMeta>, TTargetQuery> trueChoice, Func<IVertexPropertyGremlinQuery<TValue, TMeta>, TTargetQuery> falseChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Choose<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TValue, TMeta>, IGremlinQuery> traversalPredicate, Func<IVertexPropertyGremlinQuery<TValue, TMeta>, TTargetQuery> trueChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Coalesce<TTargetQuery>(params Func<IVertexPropertyGremlinQuery<TValue, TMeta>, TTargetQuery>[] traversals) where TTargetQuery : IGremlinQuery;

        new IVertexPropertyGremlinQuery<TValue, TMeta> Dedup();

        new IVertexPropertyGremlinQuery<TValue, TMeta> Emit();

        new IVertexPropertyGremlinQuery<TValue, TMeta> Filter(string lambda);
        TTargetQuery FlatMap<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TValue, TMeta>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;

        new IVertexPropertyGremlinQuery<TValue, TMeta> Identity();

        new IVertexPropertyGremlinQuery<TValue, TMeta> Limit(long count);
        TTargetQuery Local<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TValue, TMeta> , TTargetQuery> localTraversal) where TTargetQuery : IGremlinQuery;

        TTargetQuery Map<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TValue, TMeta>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;
        
        IVertexPropertyGremlinQuery<TValue, TMeta> Not(Func<IVertexPropertyGremlinQuery<TValue, TMeta>, IGremlinQuery> notTraversal);

        TTargetQuery Optional<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TValue, TMeta>, TTargetQuery> optionalTraversal) where TTargetQuery : IGremlinQuery;
        IVertexPropertyGremlinQuery<TValue, TMeta> Or(params Func<IVertexPropertyGremlinQuery<TValue, TMeta>, IGremlinQuery>[] orTraversals);

        new IVertexPropertyGremlinQuery<TValue, TMeta> Range(long low, long high);

        TTargetQuery Repeat<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TValue, TMeta>, TTargetQuery> repeatTraversal) where TTargetQuery : IVertexPropertyGremlinQuery<TValue, TMeta>;
        TTargetQuery RepeatUntil<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TValue, TMeta>, TTargetQuery> repeatTraversal, Func<IVertexPropertyGremlinQuery<TValue, TMeta>, IGremlinQuery> untilTraversal) where TTargetQuery : IVertexPropertyGremlinQuery<TValue, TMeta>;

        IVertexPropertyGremlinQuery<TValue, TMeta> SideEffect(Func<IVertexPropertyGremlinQuery<TValue, TMeta>, IGremlinQuery> sideEffectTraversal);
        new IVertexPropertyGremlinQuery<TValue, TMeta> Skip(long count);

        new IVertexPropertyGremlinQuery<TValue, TMeta> Tail(long count);
        new IVertexPropertyGremlinQuery<TValue, TMeta> Times(int count);

        TTargetQuery Union<TTargetQuery>(params Func<IVertexPropertyGremlinQuery<TValue, TMeta>, TTargetQuery>[] unionTraversals) where TTargetQuery : IGremlinQuery;
    }

    public partial interface IEdgePropertyGremlinQuery<TValue>
    {
        IEdgePropertyGremlinQuery<TValue> And(params Func<IEdgePropertyGremlinQuery<TValue>, IGremlinQuery>[] andTraversals);

        new IEdgePropertyGremlinQuery<TValue> As(params StepLabel[] stepLabels);

        new IEdgePropertyGremlinQuery<TValue> Barrier();

        TTargetQuery Choose<TTargetQuery>(Func<IEdgePropertyGremlinQuery<TValue>, IGremlinQuery> traversalPredicate, Func<IEdgePropertyGremlinQuery<TValue>, TTargetQuery> trueChoice, Func<IEdgePropertyGremlinQuery<TValue>, TTargetQuery> falseChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Choose<TTargetQuery>(Func<IEdgePropertyGremlinQuery<TValue>, IGremlinQuery> traversalPredicate, Func<IEdgePropertyGremlinQuery<TValue>, TTargetQuery> trueChoice) where TTargetQuery : IGremlinQuery;
        TTargetQuery Coalesce<TTargetQuery>(params Func<IEdgePropertyGremlinQuery<TValue>, TTargetQuery>[] traversals) where TTargetQuery : IGremlinQuery;

        new IEdgePropertyGremlinQuery<TValue> Dedup();

        new IEdgePropertyGremlinQuery<TValue> Emit();

        new IEdgePropertyGremlinQuery<TValue> Filter(string lambda);
        TTargetQuery FlatMap<TTargetQuery>(Func<IEdgePropertyGremlinQuery<TValue>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;

        new IEdgePropertyGremlinQuery<TValue> Identity();

        new IEdgePropertyGremlinQuery<TValue> Limit(long count);
        TTargetQuery Local<TTargetQuery>(Func<IEdgePropertyGremlinQuery<TValue> , TTargetQuery> localTraversal) where TTargetQuery : IGremlinQuery;

        TTargetQuery Map<TTargetQuery>(Func<IEdgePropertyGremlinQuery<TValue>, TTargetQuery> mapping) where TTargetQuery : IGremlinQuery;
        
        IEdgePropertyGremlinQuery<TValue> Not(Func<IEdgePropertyGremlinQuery<TValue>, IGremlinQuery> notTraversal);

        TTargetQuery Optional<TTargetQuery>(Func<IEdgePropertyGremlinQuery<TValue>, TTargetQuery> optionalTraversal) where TTargetQuery : IGremlinQuery;
        IEdgePropertyGremlinQuery<TValue> Or(params Func<IEdgePropertyGremlinQuery<TValue>, IGremlinQuery>[] orTraversals);

        new IEdgePropertyGremlinQuery<TValue> Range(long low, long high);

        TTargetQuery Repeat<TTargetQuery>(Func<IEdgePropertyGremlinQuery<TValue>, TTargetQuery> repeatTraversal) where TTargetQuery : IEdgePropertyGremlinQuery<TValue>;
        TTargetQuery RepeatUntil<TTargetQuery>(Func<IEdgePropertyGremlinQuery<TValue>, TTargetQuery> repeatTraversal, Func<IEdgePropertyGremlinQuery<TValue>, IGremlinQuery> untilTraversal) where TTargetQuery : IEdgePropertyGremlinQuery<TValue>;

        IEdgePropertyGremlinQuery<TValue> SideEffect(Func<IEdgePropertyGremlinQuery<TValue>, IGremlinQuery> sideEffectTraversal);
        new IEdgePropertyGremlinQuery<TValue> Skip(long count);

        new IEdgePropertyGremlinQuery<TValue> Tail(long count);
        new IEdgePropertyGremlinQuery<TValue> Times(int count);

        TTargetQuery Union<TTargetQuery>(params Func<IEdgePropertyGremlinQuery<TValue>, TTargetQuery>[] unionTraversals) where TTargetQuery : IGremlinQuery;
    }

    public partial interface IOrderedVertexGremlinQuery : IVertexGremlinQuery
    {
    }

    public partial interface IVertexGremlinQuery
    {
        new IOrderedVertexGremlinQuery OrderBy(Func<IVertexGremlinQuery, IGremlinQuery> traversal);
        new IOrderedVertexGremlinQuery OrderBy(string lambda);
        new IOrderedVertexGremlinQuery OrderByDescending(Func<IVertexGremlinQuery, IGremlinQuery> traversal);
    }
    
    public partial interface IOrderedEdgeGremlinQuery : IEdgeGremlinQuery
    {
    }

    public partial interface IEdgeGremlinQuery
    {
        new IOrderedEdgeGremlinQuery OrderBy(Func<IEdgeGremlinQuery, IGremlinQuery> traversal);
        new IOrderedEdgeGremlinQuery OrderBy(string lambda);
        new IOrderedEdgeGremlinQuery OrderByDescending(Func<IEdgeGremlinQuery, IGremlinQuery> traversal);
    }
    
    public partial interface IOrderedValueGremlinQuery<TElement> : IValueGremlinQuery<TElement>
    {
    }

    public partial interface IValueGremlinQuery<TElement>
    {
        new IOrderedValueGremlinQuery<TElement> OrderBy(Expression<Func<TElement, object>> projection);
        new IOrderedValueGremlinQuery<TElement> OrderByDescending(Expression<Func<TElement, object>> projection);
        new IOrderedValueGremlinQuery<TElement> OrderBy(Func<IValueGremlinQuery<TElement>, IGremlinQuery> traversal);
        new IOrderedValueGremlinQuery<TElement> OrderBy(string lambda);
        new IOrderedValueGremlinQuery<TElement> OrderByDescending(Func<IValueGremlinQuery<TElement>, IGremlinQuery> traversal);
    }
    
    public partial interface IOrderedArrayGremlinQuery<TArray, TQuery> : IArrayGremlinQuery<TArray, TQuery>
    {
    }

    public partial interface IArrayGremlinQuery<TArray, TQuery>
    {
        new IOrderedArrayGremlinQuery<TArray, TQuery> OrderBy(Expression<Func<TArray, object>> projection);
        new IOrderedArrayGremlinQuery<TArray, TQuery> OrderByDescending(Expression<Func<TArray, object>> projection);
        new IOrderedArrayGremlinQuery<TArray, TQuery> OrderBy(Func<IArrayGremlinQuery<TArray, TQuery>, IGremlinQuery> traversal);
        new IOrderedArrayGremlinQuery<TArray, TQuery> OrderBy(string lambda);
        new IOrderedArrayGremlinQuery<TArray, TQuery> OrderByDescending(Func<IArrayGremlinQuery<TArray, TQuery>, IGremlinQuery> traversal);
    }
    
    public partial interface IOrderedVertexGremlinQuery<TVertex> : IVertexGremlinQuery<TVertex>
    {
    }

    public partial interface IVertexGremlinQuery<TVertex>
    {
        new IOrderedVertexGremlinQuery<TVertex> OrderBy(Expression<Func<TVertex, object>> projection);
        new IOrderedVertexGremlinQuery<TVertex> OrderByDescending(Expression<Func<TVertex, object>> projection);
        new IOrderedVertexGremlinQuery<TVertex> OrderBy(Func<IVertexGremlinQuery<TVertex>, IGremlinQuery> traversal);
        new IOrderedVertexGremlinQuery<TVertex> OrderBy(string lambda);
        new IOrderedVertexGremlinQuery<TVertex> OrderByDescending(Func<IVertexGremlinQuery<TVertex>, IGremlinQuery> traversal);
    }
    
    public partial interface IOrderedEdgeGremlinQuery<TEdge> : IEdgeGremlinQuery<TEdge>
    {
    }

    public partial interface IEdgeGremlinQuery<TEdge>
    {
        new IOrderedEdgeGremlinQuery<TEdge> OrderBy(Expression<Func<TEdge, object>> projection);
        new IOrderedEdgeGremlinQuery<TEdge> OrderByDescending(Expression<Func<TEdge, object>> projection);
        new IOrderedEdgeGremlinQuery<TEdge> OrderBy(Func<IEdgeGremlinQuery<TEdge>, IGremlinQuery> traversal);
        new IOrderedEdgeGremlinQuery<TEdge> OrderBy(string lambda);
        new IOrderedEdgeGremlinQuery<TEdge> OrderByDescending(Func<IEdgeGremlinQuery<TEdge>, IGremlinQuery> traversal);
    }
    
    public partial interface IOrderedEdgeGremlinQuery<TEdge, TAdjacentVertex> : IEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {
    }

    public partial interface IEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {
        new IOrderedEdgeGremlinQuery<TEdge, TAdjacentVertex> OrderBy(Expression<Func<TEdge, object>> projection);
        new IOrderedEdgeGremlinQuery<TEdge, TAdjacentVertex> OrderByDescending(Expression<Func<TEdge, object>> projection);
        new IOrderedEdgeGremlinQuery<TEdge, TAdjacentVertex> OrderBy(Func<IEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> traversal);
        new IOrderedEdgeGremlinQuery<TEdge, TAdjacentVertex> OrderBy(string lambda);
        new IOrderedEdgeGremlinQuery<TEdge, TAdjacentVertex> OrderByDescending(Func<IEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> traversal);
    }
    
    public partial interface IOrderedEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> : IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>
    {
    }

    public partial interface IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>
    {
        new IOrderedEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> OrderBy(Expression<Func<TEdge, object>> projection);
        new IOrderedEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> OrderByDescending(Expression<Func<TEdge, object>> projection);
        new IOrderedEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> OrderBy(Func<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, IGremlinQuery> traversal);
        new IOrderedEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> OrderBy(string lambda);
        new IOrderedEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> OrderByDescending(Func<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, IGremlinQuery> traversal);
    }
    
    public partial interface IOrderedInEdgeGremlinQuery<TEdge, TAdjacentVertex> : IInEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {
    }

    public partial interface IInEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {
        new IOrderedInEdgeGremlinQuery<TEdge, TAdjacentVertex> OrderBy(Expression<Func<TEdge, object>> projection);
        new IOrderedInEdgeGremlinQuery<TEdge, TAdjacentVertex> OrderByDescending(Expression<Func<TEdge, object>> projection);
        new IOrderedInEdgeGremlinQuery<TEdge, TAdjacentVertex> OrderBy(Func<IInEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> traversal);
        new IOrderedInEdgeGremlinQuery<TEdge, TAdjacentVertex> OrderBy(string lambda);
        new IOrderedInEdgeGremlinQuery<TEdge, TAdjacentVertex> OrderByDescending(Func<IInEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> traversal);
    }
    
    public partial interface IOrderedOutEdgeGremlinQuery<TEdge, TAdjacentVertex> : IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {
    }

    public partial interface IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {
        new IOrderedOutEdgeGremlinQuery<TEdge, TAdjacentVertex> OrderBy(Expression<Func<TEdge, object>> projection);
        new IOrderedOutEdgeGremlinQuery<TEdge, TAdjacentVertex> OrderByDescending(Expression<Func<TEdge, object>> projection);
        new IOrderedOutEdgeGremlinQuery<TEdge, TAdjacentVertex> OrderBy(Func<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> traversal);
        new IOrderedOutEdgeGremlinQuery<TEdge, TAdjacentVertex> OrderBy(string lambda);
        new IOrderedOutEdgeGremlinQuery<TEdge, TAdjacentVertex> OrderByDescending(Func<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> traversal);
    }
    
    public partial interface IOrderedVertexPropertyGremlinQuery<TValue> : IVertexPropertyGremlinQuery<TValue>
    {
    }

    public partial interface IVertexPropertyGremlinQuery<TValue>
    {
        new IOrderedVertexPropertyGremlinQuery<TValue> OrderBy(Expression<Func<TValue, object>> projection);
        new IOrderedVertexPropertyGremlinQuery<TValue> OrderByDescending(Expression<Func<TValue, object>> projection);
        new IOrderedVertexPropertyGremlinQuery<TValue> OrderBy(Func<IVertexPropertyGremlinQuery<TValue>, IGremlinQuery> traversal);
        new IOrderedVertexPropertyGremlinQuery<TValue> OrderBy(string lambda);
        new IOrderedVertexPropertyGremlinQuery<TValue> OrderByDescending(Func<IVertexPropertyGremlinQuery<TValue>, IGremlinQuery> traversal);
    }
    
    public partial interface IOrderedVertexPropertyGremlinQuery<TValue, TMeta> : IVertexPropertyGremlinQuery<TValue, TMeta>
    {
    }

    public partial interface IVertexPropertyGremlinQuery<TValue, TMeta>
    {
        new IOrderedVertexPropertyGremlinQuery<TValue, TMeta> OrderBy(Expression<Func<TValue, object>> projection);
        new IOrderedVertexPropertyGremlinQuery<TValue, TMeta> OrderByDescending(Expression<Func<TValue, object>> projection);
        new IOrderedVertexPropertyGremlinQuery<TValue, TMeta> OrderBy(Func<IVertexPropertyGremlinQuery<TValue, TMeta>, IGremlinQuery> traversal);
        new IOrderedVertexPropertyGremlinQuery<TValue, TMeta> OrderBy(string lambda);
        new IOrderedVertexPropertyGremlinQuery<TValue, TMeta> OrderByDescending(Func<IVertexPropertyGremlinQuery<TValue, TMeta>, IGremlinQuery> traversal);
    }
    
    public partial interface IOrderedEdgePropertyGremlinQuery<TValue> : IEdgePropertyGremlinQuery<TValue>
    {
    }

    public partial interface IEdgePropertyGremlinQuery<TValue>
    {
        new IOrderedEdgePropertyGremlinQuery<TValue> OrderBy(Expression<Func<TValue, object>> projection);
        new IOrderedEdgePropertyGremlinQuery<TValue> OrderByDescending(Expression<Func<TValue, object>> projection);
        new IOrderedEdgePropertyGremlinQuery<TValue> OrderBy(Func<IEdgePropertyGremlinQuery<TValue>, IGremlinQuery> traversal);
        new IOrderedEdgePropertyGremlinQuery<TValue> OrderBy(string lambda);
        new IOrderedEdgePropertyGremlinQuery<TValue> OrderByDescending(Func<IEdgePropertyGremlinQuery<TValue>, IGremlinQuery> traversal);
    }
    
    public partial interface IOrderedVertexGremlinQuery
    {
        IOrderedVertexGremlinQuery ThenBy(Func<IVertexGremlinQuery, IGremlinQuery> traversal);
        IOrderedVertexGremlinQuery ThenByDescending(Func<IVertexGremlinQuery, IGremlinQuery> traversal);
        new IOrderedVertexGremlinQuery ThenBy(string lambda);
    }

    public partial interface IOrderedEdgeGremlinQuery
    {
        IOrderedEdgeGremlinQuery ThenBy(Func<IEdgeGremlinQuery, IGremlinQuery> traversal);
        IOrderedEdgeGremlinQuery ThenByDescending(Func<IEdgeGremlinQuery, IGremlinQuery> traversal);
        new IOrderedEdgeGremlinQuery ThenBy(string lambda);
    }

    public partial interface IOrderedValueGremlinQuery<TElement>
    {
        new IOrderedValueGremlinQuery<TElement> ThenBy(Expression<Func<TElement, object>> projection);
        new IOrderedValueGremlinQuery<TElement> ThenByDescending(Expression<Func<TElement, object>> projection);
        IOrderedValueGremlinQuery<TElement> ThenBy(Func<IValueGremlinQuery<TElement>, IGremlinQuery> traversal);
        IOrderedValueGremlinQuery<TElement> ThenByDescending(Func<IValueGremlinQuery<TElement>, IGremlinQuery> traversal);
        new IOrderedValueGremlinQuery<TElement> ThenBy(string lambda);
    }

    public partial interface IOrderedArrayGremlinQuery<TArray, TQuery>
    {
        new IOrderedArrayGremlinQuery<TArray, TQuery> ThenBy(Expression<Func<TArray, object>> projection);
        new IOrderedArrayGremlinQuery<TArray, TQuery> ThenByDescending(Expression<Func<TArray, object>> projection);
        IOrderedArrayGremlinQuery<TArray, TQuery> ThenBy(Func<IArrayGremlinQuery<TArray, TQuery>, IGremlinQuery> traversal);
        IOrderedArrayGremlinQuery<TArray, TQuery> ThenByDescending(Func<IArrayGremlinQuery<TArray, TQuery>, IGremlinQuery> traversal);
        new IOrderedArrayGremlinQuery<TArray, TQuery> ThenBy(string lambda);
    }

    public partial interface IOrderedVertexGremlinQuery<TVertex>
    {
        new IOrderedVertexGremlinQuery<TVertex> ThenBy(Expression<Func<TVertex, object>> projection);
        new IOrderedVertexGremlinQuery<TVertex> ThenByDescending(Expression<Func<TVertex, object>> projection);
        IOrderedVertexGremlinQuery<TVertex> ThenBy(Func<IVertexGremlinQuery<TVertex>, IGremlinQuery> traversal);
        IOrderedVertexGremlinQuery<TVertex> ThenByDescending(Func<IVertexGremlinQuery<TVertex>, IGremlinQuery> traversal);
        new IOrderedVertexGremlinQuery<TVertex> ThenBy(string lambda);
    }

    public partial interface IOrderedEdgeGremlinQuery<TEdge>
    {
        new IOrderedEdgeGremlinQuery<TEdge> ThenBy(Expression<Func<TEdge, object>> projection);
        new IOrderedEdgeGremlinQuery<TEdge> ThenByDescending(Expression<Func<TEdge, object>> projection);
        IOrderedEdgeGremlinQuery<TEdge> ThenBy(Func<IEdgeGremlinQuery<TEdge>, IGremlinQuery> traversal);
        IOrderedEdgeGremlinQuery<TEdge> ThenByDescending(Func<IEdgeGremlinQuery<TEdge>, IGremlinQuery> traversal);
        new IOrderedEdgeGremlinQuery<TEdge> ThenBy(string lambda);
    }

    public partial interface IOrderedEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {
        new IOrderedEdgeGremlinQuery<TEdge, TAdjacentVertex> ThenBy(Expression<Func<TEdge, object>> projection);
        new IOrderedEdgeGremlinQuery<TEdge, TAdjacentVertex> ThenByDescending(Expression<Func<TEdge, object>> projection);
        IOrderedEdgeGremlinQuery<TEdge, TAdjacentVertex> ThenBy(Func<IEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> traversal);
        IOrderedEdgeGremlinQuery<TEdge, TAdjacentVertex> ThenByDescending(Func<IEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> traversal);
        new IOrderedEdgeGremlinQuery<TEdge, TAdjacentVertex> ThenBy(string lambda);
    }

    public partial interface IOrderedEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>
    {
        new IOrderedEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> ThenBy(Expression<Func<TEdge, object>> projection);
        new IOrderedEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> ThenByDescending(Expression<Func<TEdge, object>> projection);
        IOrderedEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> ThenBy(Func<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, IGremlinQuery> traversal);
        IOrderedEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> ThenByDescending(Func<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, IGremlinQuery> traversal);
        new IOrderedEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> ThenBy(string lambda);
    }

    public partial interface IOrderedInEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {
        new IOrderedInEdgeGremlinQuery<TEdge, TAdjacentVertex> ThenBy(Expression<Func<TEdge, object>> projection);
        new IOrderedInEdgeGremlinQuery<TEdge, TAdjacentVertex> ThenByDescending(Expression<Func<TEdge, object>> projection);
        IOrderedInEdgeGremlinQuery<TEdge, TAdjacentVertex> ThenBy(Func<IInEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> traversal);
        IOrderedInEdgeGremlinQuery<TEdge, TAdjacentVertex> ThenByDescending(Func<IInEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> traversal);
        new IOrderedInEdgeGremlinQuery<TEdge, TAdjacentVertex> ThenBy(string lambda);
    }

    public partial interface IOrderedOutEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {
        new IOrderedOutEdgeGremlinQuery<TEdge, TAdjacentVertex> ThenBy(Expression<Func<TEdge, object>> projection);
        new IOrderedOutEdgeGremlinQuery<TEdge, TAdjacentVertex> ThenByDescending(Expression<Func<TEdge, object>> projection);
        IOrderedOutEdgeGremlinQuery<TEdge, TAdjacentVertex> ThenBy(Func<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> traversal);
        IOrderedOutEdgeGremlinQuery<TEdge, TAdjacentVertex> ThenByDescending(Func<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> traversal);
        new IOrderedOutEdgeGremlinQuery<TEdge, TAdjacentVertex> ThenBy(string lambda);
    }

    public partial interface IOrderedVertexPropertyGremlinQuery<TValue>
    {
        new IOrderedVertexPropertyGremlinQuery<TValue> ThenBy(Expression<Func<TValue, object>> projection);
        new IOrderedVertexPropertyGremlinQuery<TValue> ThenByDescending(Expression<Func<TValue, object>> projection);
        IOrderedVertexPropertyGremlinQuery<TValue> ThenBy(Func<IVertexPropertyGremlinQuery<TValue>, IGremlinQuery> traversal);
        IOrderedVertexPropertyGremlinQuery<TValue> ThenByDescending(Func<IVertexPropertyGremlinQuery<TValue>, IGremlinQuery> traversal);
        new IOrderedVertexPropertyGremlinQuery<TValue> ThenBy(string lambda);
    }

    public partial interface IOrderedVertexPropertyGremlinQuery<TValue, TMeta>
    {
        new IOrderedVertexPropertyGremlinQuery<TValue, TMeta> ThenBy(Expression<Func<TValue, object>> projection);
        new IOrderedVertexPropertyGremlinQuery<TValue, TMeta> ThenByDescending(Expression<Func<TValue, object>> projection);
        IOrderedVertexPropertyGremlinQuery<TValue, TMeta> ThenBy(Func<IVertexPropertyGremlinQuery<TValue, TMeta>, IGremlinQuery> traversal);
        IOrderedVertexPropertyGremlinQuery<TValue, TMeta> ThenByDescending(Func<IVertexPropertyGremlinQuery<TValue, TMeta>, IGremlinQuery> traversal);
        new IOrderedVertexPropertyGremlinQuery<TValue, TMeta> ThenBy(string lambda);
    }

    public partial interface IOrderedEdgePropertyGremlinQuery<TValue>
    {
        new IOrderedEdgePropertyGremlinQuery<TValue> ThenBy(Expression<Func<TValue, object>> projection);
        new IOrderedEdgePropertyGremlinQuery<TValue> ThenByDescending(Expression<Func<TValue, object>> projection);
        IOrderedEdgePropertyGremlinQuery<TValue> ThenBy(Func<IEdgePropertyGremlinQuery<TValue>, IGremlinQuery> traversal);
        IOrderedEdgePropertyGremlinQuery<TValue> ThenByDescending(Func<IEdgePropertyGremlinQuery<TValue>, IGremlinQuery> traversal);
        new IOrderedEdgePropertyGremlinQuery<TValue> ThenBy(string lambda);
    }

    public partial interface IElementGremlinQuery<TElement>
    {

    }

    public partial interface IVertexGremlinQuery<TVertex>
    {

    }

    public partial interface IEdgeGremlinQuery<TEdge>
    {

    }

    public partial interface IEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {

    }

    public partial interface IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>
    {

    }

    public partial interface IInEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {

    }

    public partial interface IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {

    }

    public partial interface IVertexPropertyGremlinQuery<TValue>
    {

    }

    public partial interface IVertexPropertyGremlinQuery<TValue, TMeta>
    {

    }

    public partial interface IVertexGremlinQuery<TVertex>
    {
        new IValueGremlinQuery<TTarget> Values<TTarget>(params Expression<Func<TVertex, TTarget>>[] projections);
        new IValueGremlinQuery<TTarget> Values<TTarget>(params Expression<Func<TVertex, TTarget[]>>[] projections);
    }

    public partial interface IEdgeGremlinQuery<TEdge>
    {
        new IValueGremlinQuery<TTarget> Values<TTarget>(params Expression<Func<TEdge, TTarget>>[] projections);
        new IValueGremlinQuery<TTarget> Values<TTarget>(params Expression<Func<TEdge, TTarget[]>>[] projections);
    }

    public partial interface IVertexGremlinQuery
    {
        IVertexGremlinQuery<TTarget> OfType<TTarget>();
    }

    public partial interface IEdgeGremlinQuery
    {
        IEdgeGremlinQuery<TTarget> OfType<TTarget>();
    }

    public partial interface IVertexGremlinQuery<TVertex>
    {
        new IVertexGremlinQuery<TTarget> OfType<TTarget>();

        new IVertexGremlinQuery<TVertex> Property<TProjectedValue>(Expression<Func<TVertex, TProjectedValue>> projection, TProjectedValue value);
        new IVertexGremlinQuery<TVertex> Property<TProjectedValue>(Expression<Func<TVertex, TProjectedValue[]>> projection, TProjectedValue value);

        new IVertexGremlinQuery<TVertex> Where(Expression<Func<TVertex, bool>> predicate);
        new IVertexGremlinQuery<TVertex> Where<TProjection>(Expression<Func<TVertex, TProjection>> projection, Func<IGremlinQuery<TProjection>, IGremlinQuery> propertyTraversal);
    }

    public partial interface IEdgeGremlinQuery<TEdge>
    {
        new IEdgeGremlinQuery<TTarget> OfType<TTarget>();

        new IEdgeGremlinQuery<TEdge> Property<TProjectedValue>(Expression<Func<TEdge, TProjectedValue>> projection, TProjectedValue value);
        new IEdgeGremlinQuery<TEdge> Property<TProjectedValue>(Expression<Func<TEdge, TProjectedValue[]>> projection, TProjectedValue value);

        new IEdgeGremlinQuery<TEdge> Where(Expression<Func<TEdge, bool>> predicate);
        new IEdgeGremlinQuery<TEdge> Where<TProjection>(Expression<Func<TEdge, TProjection>> projection, Func<IGremlinQuery<TProjection>, IGremlinQuery> propertyTraversal);
    }

    public partial interface IEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {
        new IEdgeGremlinQuery<TTarget, TAdjacentVertex> OfType<TTarget>();

        new IEdgeGremlinQuery<TEdge, TAdjacentVertex> Property<TProjectedValue>(Expression<Func<TEdge, TProjectedValue>> projection, TProjectedValue value);
        new IEdgeGremlinQuery<TEdge, TAdjacentVertex> Property<TProjectedValue>(Expression<Func<TEdge, TProjectedValue[]>> projection, TProjectedValue value);

        new IEdgeGremlinQuery<TEdge, TAdjacentVertex> Where(Expression<Func<TEdge, bool>> predicate);
        new IEdgeGremlinQuery<TEdge, TAdjacentVertex> Where<TProjection>(Expression<Func<TEdge, TProjection>> projection, Func<IGremlinQuery<TProjection>, IGremlinQuery> propertyTraversal);
    }

    public partial interface IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>
    {
        new IEdgeGremlinQuery<TTarget, TOutVertex, TInVertex> OfType<TTarget>();

        new IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> Property<TProjectedValue>(Expression<Func<TEdge, TProjectedValue>> projection, TProjectedValue value);
        new IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> Property<TProjectedValue>(Expression<Func<TEdge, TProjectedValue[]>> projection, TProjectedValue value);

        new IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> Where(Expression<Func<TEdge, bool>> predicate);
        new IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> Where<TProjection>(Expression<Func<TEdge, TProjection>> projection, Func<IGremlinQuery<TProjection>, IGremlinQuery> propertyTraversal);
    }

    public partial interface IInEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {
        new IInEdgeGremlinQuery<TTarget, TAdjacentVertex> OfType<TTarget>();

        new IInEdgeGremlinQuery<TEdge, TAdjacentVertex> Property<TProjectedValue>(Expression<Func<TEdge, TProjectedValue>> projection, TProjectedValue value);
        new IInEdgeGremlinQuery<TEdge, TAdjacentVertex> Property<TProjectedValue>(Expression<Func<TEdge, TProjectedValue[]>> projection, TProjectedValue value);

        new IInEdgeGremlinQuery<TEdge, TAdjacentVertex> Where(Expression<Func<TEdge, bool>> predicate);
        new IInEdgeGremlinQuery<TEdge, TAdjacentVertex> Where<TProjection>(Expression<Func<TEdge, TProjection>> projection, Func<IGremlinQuery<TProjection>, IGremlinQuery> propertyTraversal);
    }

    public partial interface IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {
        new IOutEdgeGremlinQuery<TTarget, TAdjacentVertex> OfType<TTarget>();

        new IOutEdgeGremlinQuery<TEdge, TAdjacentVertex> Property<TProjectedValue>(Expression<Func<TEdge, TProjectedValue>> projection, TProjectedValue value);
        new IOutEdgeGremlinQuery<TEdge, TAdjacentVertex> Property<TProjectedValue>(Expression<Func<TEdge, TProjectedValue[]>> projection, TProjectedValue value);

        new IOutEdgeGremlinQuery<TEdge, TAdjacentVertex> Where(Expression<Func<TEdge, bool>> predicate);
        new IOutEdgeGremlinQuery<TEdge, TAdjacentVertex> Where<TProjection>(Expression<Func<TEdge, TProjection>> projection, Func<IGremlinQuery<TProjection>, IGremlinQuery> propertyTraversal);
    }

    public partial interface IGremlinQuery<TElement>
    {
        TTargetQuery Aggregate<TTargetQuery>(Func<IGremlinQuery<TElement>, StepLabel<TElement[]>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;
        TTargetQuery As<TTargetQuery>(Func<IGremlinQuery<TElement>, StepLabel<IGremlinQuery<TElement>, TElement>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;

        new IGremlinQuery<TResult> Cast<TResult>();

        new IArrayGremlinQuery<TElement[], IGremlinQuery<TElement>> Fold();

        new IGremlinQuery<TElement> Where(Func<IGremlinQuery<TElement>, IGremlinQuery> filterTraversal);
    }

    public partial interface IValueGremlinQuery<TElement>
    {
        TTargetQuery Aggregate<TTargetQuery>(Func<IValueGremlinQuery<TElement>, StepLabel<TElement[]>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;
        TTargetQuery As<TTargetQuery>(Func<IValueGremlinQuery<TElement>, StepLabel<IValueGremlinQuery<TElement>, TElement>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;

        new IValueGremlinQuery<TResult> Cast<TResult>();

        new IArrayGremlinQuery<TElement[], IValueGremlinQuery<TElement>> Fold();

        new IValueGremlinQuery<TElement> Where(Func<IValueGremlinQuery<TElement>, IGremlinQuery> filterTraversal);
    }

    public partial interface IArrayGremlinQuery<TArray, TQuery>
    {
        TTargetQuery Aggregate<TTargetQuery>(Func<IArrayGremlinQuery<TArray, TQuery>, StepLabel<TArray[]>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;
        TTargetQuery As<TTargetQuery>(Func<IArrayGremlinQuery<TArray, TQuery>, StepLabel<IArrayGremlinQuery<TArray, TQuery>, TArray>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;

        new IArrayGremlinQuery<TResult, TQuery> Cast<TResult>();

        new IArrayGremlinQuery<TArray[], IArrayGremlinQuery<TArray, TQuery>> Fold();

        new IArrayGremlinQuery<TArray, TQuery> Where(Func<IArrayGremlinQuery<TArray, TQuery>, IGremlinQuery> filterTraversal);
    }

    public partial interface IElementGremlinQuery<TElement>
    {
        TTargetQuery Aggregate<TTargetQuery>(Func<IElementGremlinQuery<TElement>, StepLabel<TElement[]>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;
        TTargetQuery As<TTargetQuery>(Func<IElementGremlinQuery<TElement>, StepLabel<IElementGremlinQuery<TElement>, TElement>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;

        new IElementGremlinQuery<TResult> Cast<TResult>();

        new IArrayGremlinQuery<TElement[], IElementGremlinQuery<TElement>> Fold();

        new IElementGremlinQuery<TElement> Where(Func<IElementGremlinQuery<TElement>, IGremlinQuery> filterTraversal);
    }

    public partial interface IVertexGremlinQuery<TVertex>
    {
        TTargetQuery Aggregate<TTargetQuery>(Func<IVertexGremlinQuery<TVertex>, StepLabel<TVertex[]>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;
        TTargetQuery As<TTargetQuery>(Func<IVertexGremlinQuery<TVertex>, StepLabel<IVertexGremlinQuery<TVertex>, TVertex>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;

        new IVertexGremlinQuery<TResult> Cast<TResult>();

        new IArrayGremlinQuery<TVertex[], IVertexGremlinQuery<TVertex>> Fold();

        new IVertexGremlinQuery<TVertex> Where(Func<IVertexGremlinQuery<TVertex>, IGremlinQuery> filterTraversal);
    }

    public partial interface IEdgeGremlinQuery<TEdge>
    {
        TTargetQuery Aggregate<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge>, StepLabel<TEdge[]>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;
        TTargetQuery As<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge>, StepLabel<IEdgeGremlinQuery<TEdge>, TEdge>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;

        new IEdgeGremlinQuery<TResult> Cast<TResult>();

        new IArrayGremlinQuery<TEdge[], IEdgeGremlinQuery<TEdge>> Fold();

        new IEdgeGremlinQuery<TEdge> Where(Func<IEdgeGremlinQuery<TEdge>, IGremlinQuery> filterTraversal);
    }

    public partial interface IEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {
        TTargetQuery Aggregate<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge, TAdjacentVertex>, StepLabel<TEdge[]>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;
        TTargetQuery As<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge, TAdjacentVertex>, StepLabel<IEdgeGremlinQuery<TEdge, TAdjacentVertex>, TEdge>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;

        new IEdgeGremlinQuery<TResult, TAdjacentVertex> Cast<TResult>();

        new IArrayGremlinQuery<TEdge[], IEdgeGremlinQuery<TEdge, TAdjacentVertex>> Fold();

        new IEdgeGremlinQuery<TEdge, TAdjacentVertex> Where(Func<IEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> filterTraversal);
    }

    public partial interface IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>
    {
        TTargetQuery Aggregate<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, StepLabel<TEdge[]>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;
        TTargetQuery As<TTargetQuery>(Func<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, StepLabel<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, TEdge>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;

        new IEdgeGremlinQuery<TResult, TOutVertex, TInVertex> Cast<TResult>();

        new IArrayGremlinQuery<TEdge[], IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>> Fold();

        new IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> Where(Func<IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, IGremlinQuery> filterTraversal);
    }

    public partial interface IInEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {
        TTargetQuery Aggregate<TTargetQuery>(Func<IInEdgeGremlinQuery<TEdge, TAdjacentVertex>, StepLabel<TEdge[]>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;
        TTargetQuery As<TTargetQuery>(Func<IInEdgeGremlinQuery<TEdge, TAdjacentVertex>, StepLabel<IInEdgeGremlinQuery<TEdge, TAdjacentVertex>, TEdge>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;

        new IInEdgeGremlinQuery<TResult, TAdjacentVertex> Cast<TResult>();

        new IArrayGremlinQuery<TEdge[], IInEdgeGremlinQuery<TEdge, TAdjacentVertex>> Fold();

        new IInEdgeGremlinQuery<TEdge, TAdjacentVertex> Where(Func<IInEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> filterTraversal);
    }

    public partial interface IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {
        TTargetQuery Aggregate<TTargetQuery>(Func<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, StepLabel<TEdge[]>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;
        TTargetQuery As<TTargetQuery>(Func<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, StepLabel<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, TEdge>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;

        new IOutEdgeGremlinQuery<TResult, TAdjacentVertex> Cast<TResult>();

        new IArrayGremlinQuery<TEdge[], IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>> Fold();

        new IOutEdgeGremlinQuery<TEdge, TAdjacentVertex> Where(Func<IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> filterTraversal);
    }

    public partial interface IVertexPropertyGremlinQuery<TValue>
    {
        TTargetQuery Aggregate<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TValue>, StepLabel<TValue[]>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;
        TTargetQuery As<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TValue>, StepLabel<IVertexPropertyGremlinQuery<TValue>, TValue>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;

        new IVertexPropertyGremlinQuery<TResult> Cast<TResult>();

        new IArrayGremlinQuery<TValue[], IVertexPropertyGremlinQuery<TValue>> Fold();

        new IVertexPropertyGremlinQuery<TValue> Where(Func<IVertexPropertyGremlinQuery<TValue>, IGremlinQuery> filterTraversal);
    }

    public partial interface IVertexPropertyGremlinQuery<TValue, TMeta>
    {
        TTargetQuery Aggregate<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TValue, TMeta>, StepLabel<TValue[]>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;
        TTargetQuery As<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TValue, TMeta>, StepLabel<IVertexPropertyGremlinQuery<TValue, TMeta>, TValue>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;

        new IVertexPropertyGremlinQuery<TResult, TMeta> Cast<TResult>();

        new IArrayGremlinQuery<TValue[], IVertexPropertyGremlinQuery<TValue, TMeta>> Fold();

        new IVertexPropertyGremlinQuery<TValue, TMeta> Where(Func<IVertexPropertyGremlinQuery<TValue, TMeta>, IGremlinQuery> filterTraversal);
    }

    public partial interface IEdgePropertyGremlinQuery<TValue>
    {
        TTargetQuery Aggregate<TTargetQuery>(Func<IEdgePropertyGremlinQuery<TValue>, StepLabel<TValue[]>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;
        TTargetQuery As<TTargetQuery>(Func<IEdgePropertyGremlinQuery<TValue>, StepLabel<IEdgePropertyGremlinQuery<TValue>, TValue>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;

        new IEdgePropertyGremlinQuery<TResult> Cast<TResult>();

        new IArrayGremlinQuery<TValue[], IEdgePropertyGremlinQuery<TValue>> Fold();

        new IEdgePropertyGremlinQuery<TValue> Where(Func<IEdgePropertyGremlinQuery<TValue>, IGremlinQuery> filterTraversal);
    }

    public partial interface IOrderedValueGremlinQuery<TElement>
    {
        TTargetQuery Aggregate<TTargetQuery>(Func<IOrderedValueGremlinQuery<TElement>, StepLabel<TElement[]>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;
        TTargetQuery As<TTargetQuery>(Func<IOrderedValueGremlinQuery<TElement>, StepLabel<IOrderedValueGremlinQuery<TElement>, TElement>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;

        new IOrderedValueGremlinQuery<TResult> Cast<TResult>();

        new IArrayGremlinQuery<TElement[], IValueGremlinQuery<TElement>> Fold();

        new IValueGremlinQuery<TElement> Where(Func<IOrderedValueGremlinQuery<TElement>, IGremlinQuery> filterTraversal);
    }

    public partial interface IOrderedArrayGremlinQuery<TArray, TQuery>
    {
        TTargetQuery Aggregate<TTargetQuery>(Func<IOrderedArrayGremlinQuery<TArray, TQuery>, StepLabel<TArray[]>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;
        TTargetQuery As<TTargetQuery>(Func<IOrderedArrayGremlinQuery<TArray, TQuery>, StepLabel<IOrderedArrayGremlinQuery<TArray, TQuery>, TArray>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;

        new IOrderedArrayGremlinQuery<TResult, TQuery> Cast<TResult>();

        new IArrayGremlinQuery<TArray[], IArrayGremlinQuery<TArray, TQuery>> Fold();

        new IArrayGremlinQuery<TArray, TQuery> Where(Func<IOrderedArrayGremlinQuery<TArray, TQuery>, IGremlinQuery> filterTraversal);
    }

    public partial interface IOrderedVertexGremlinQuery<TVertex>
    {
        TTargetQuery Aggregate<TTargetQuery>(Func<IOrderedVertexGremlinQuery<TVertex>, StepLabel<TVertex[]>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;
        TTargetQuery As<TTargetQuery>(Func<IOrderedVertexGremlinQuery<TVertex>, StepLabel<IOrderedVertexGremlinQuery<TVertex>, TVertex>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;

        new IOrderedVertexGremlinQuery<TResult> Cast<TResult>();

        new IArrayGremlinQuery<TVertex[], IVertexGremlinQuery<TVertex>> Fold();

        new IVertexGremlinQuery<TVertex> Where(Func<IOrderedVertexGremlinQuery<TVertex>, IGremlinQuery> filterTraversal);
    }

    public partial interface IOrderedEdgeGremlinQuery<TEdge>
    {
        TTargetQuery Aggregate<TTargetQuery>(Func<IOrderedEdgeGremlinQuery<TEdge>, StepLabel<TEdge[]>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;
        TTargetQuery As<TTargetQuery>(Func<IOrderedEdgeGremlinQuery<TEdge>, StepLabel<IOrderedEdgeGremlinQuery<TEdge>, TEdge>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;

        new IOrderedEdgeGremlinQuery<TResult> Cast<TResult>();

        new IArrayGremlinQuery<TEdge[], IEdgeGremlinQuery<TEdge>> Fold();

        new IEdgeGremlinQuery<TEdge> Where(Func<IOrderedEdgeGremlinQuery<TEdge>, IGremlinQuery> filterTraversal);
    }

    public partial interface IOrderedEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {
        TTargetQuery Aggregate<TTargetQuery>(Func<IOrderedEdgeGremlinQuery<TEdge, TAdjacentVertex>, StepLabel<TEdge[]>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;
        TTargetQuery As<TTargetQuery>(Func<IOrderedEdgeGremlinQuery<TEdge, TAdjacentVertex>, StepLabel<IOrderedEdgeGremlinQuery<TEdge, TAdjacentVertex>, TEdge>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;

        new IOrderedEdgeGremlinQuery<TResult, TAdjacentVertex> Cast<TResult>();

        new IArrayGremlinQuery<TEdge[], IEdgeGremlinQuery<TEdge, TAdjacentVertex>> Fold();

        new IEdgeGremlinQuery<TEdge, TAdjacentVertex> Where(Func<IOrderedEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> filterTraversal);
    }

    public partial interface IOrderedEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>
    {
        TTargetQuery Aggregate<TTargetQuery>(Func<IOrderedEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, StepLabel<TEdge[]>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;
        TTargetQuery As<TTargetQuery>(Func<IOrderedEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, StepLabel<IOrderedEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, TEdge>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;

        new IOrderedEdgeGremlinQuery<TResult, TOutVertex, TInVertex> Cast<TResult>();

        new IArrayGremlinQuery<TEdge[], IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>> Fold();

        new IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> Where(Func<IOrderedEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>, IGremlinQuery> filterTraversal);
    }

    public partial interface IOrderedInEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {
        TTargetQuery Aggregate<TTargetQuery>(Func<IOrderedInEdgeGremlinQuery<TEdge, TAdjacentVertex>, StepLabel<TEdge[]>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;
        TTargetQuery As<TTargetQuery>(Func<IOrderedInEdgeGremlinQuery<TEdge, TAdjacentVertex>, StepLabel<IOrderedInEdgeGremlinQuery<TEdge, TAdjacentVertex>, TEdge>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;

        new IOrderedInEdgeGremlinQuery<TResult, TAdjacentVertex> Cast<TResult>();

        new IArrayGremlinQuery<TEdge[], IInEdgeGremlinQuery<TEdge, TAdjacentVertex>> Fold();

        new IInEdgeGremlinQuery<TEdge, TAdjacentVertex> Where(Func<IOrderedInEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> filterTraversal);
    }

    public partial interface IOrderedOutEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {
        TTargetQuery Aggregate<TTargetQuery>(Func<IOrderedOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, StepLabel<TEdge[]>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;
        TTargetQuery As<TTargetQuery>(Func<IOrderedOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, StepLabel<IOrderedOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, TEdge>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;

        new IOrderedOutEdgeGremlinQuery<TResult, TAdjacentVertex> Cast<TResult>();

        new IArrayGremlinQuery<TEdge[], IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>> Fold();

        new IOutEdgeGremlinQuery<TEdge, TAdjacentVertex> Where(Func<IOrderedOutEdgeGremlinQuery<TEdge, TAdjacentVertex>, IGremlinQuery> filterTraversal);
    }

    public partial interface IOrderedVertexPropertyGremlinQuery<TValue>
    {
        TTargetQuery Aggregate<TTargetQuery>(Func<IOrderedVertexPropertyGremlinQuery<TValue>, StepLabel<TValue[]>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;
        TTargetQuery As<TTargetQuery>(Func<IOrderedVertexPropertyGremlinQuery<TValue>, StepLabel<IOrderedVertexPropertyGremlinQuery<TValue>, TValue>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;

        new IOrderedVertexPropertyGremlinQuery<TResult> Cast<TResult>();

        new IArrayGremlinQuery<TValue[], IVertexPropertyGremlinQuery<TValue>> Fold();

        new IVertexPropertyGremlinQuery<TValue> Where(Func<IOrderedVertexPropertyGremlinQuery<TValue>, IGremlinQuery> filterTraversal);
    }

    public partial interface IOrderedVertexPropertyGremlinQuery<TValue, TMeta>
    {
        TTargetQuery Aggregate<TTargetQuery>(Func<IOrderedVertexPropertyGremlinQuery<TValue, TMeta>, StepLabel<TValue[]>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;
        TTargetQuery As<TTargetQuery>(Func<IOrderedVertexPropertyGremlinQuery<TValue, TMeta>, StepLabel<IOrderedVertexPropertyGremlinQuery<TValue, TMeta>, TValue>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;

        new IOrderedVertexPropertyGremlinQuery<TResult, TMeta> Cast<TResult>();

        new IArrayGremlinQuery<TValue[], IVertexPropertyGremlinQuery<TValue, TMeta>> Fold();

        new IVertexPropertyGremlinQuery<TValue, TMeta> Where(Func<IOrderedVertexPropertyGremlinQuery<TValue, TMeta>, IGremlinQuery> filterTraversal);
    }

    public partial interface IOrderedEdgePropertyGremlinQuery<TValue>
    {
        TTargetQuery Aggregate<TTargetQuery>(Func<IOrderedEdgePropertyGremlinQuery<TValue>, StepLabel<TValue[]>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;
        TTargetQuery As<TTargetQuery>(Func<IOrderedEdgePropertyGremlinQuery<TValue>, StepLabel<IOrderedEdgePropertyGremlinQuery<TValue>, TValue>, TTargetQuery> continuation) where TTargetQuery : IGremlinQuery;

        new IOrderedEdgePropertyGremlinQuery<TResult> Cast<TResult>();

        new IArrayGremlinQuery<TValue[], IEdgePropertyGremlinQuery<TValue>> Fold();

        new IEdgePropertyGremlinQuery<TValue> Where(Func<IOrderedEdgePropertyGremlinQuery<TValue>, IGremlinQuery> filterTraversal);
    }

    public partial interface IGremlinQuery
    {

    }

    public partial interface IElementGremlinQuery
    {

    }

    public partial interface IVertexGremlinQuery
    {

    }

    public partial interface IEdgeGremlinQuery
    {

    }

    public partial interface IGremlinQuery<TElement>
    {

    }

    public partial interface IValueGremlinQuery<TElement>
    {

    }

    public partial interface IArrayGremlinQuery<TArray, TQuery>
    {

    }

    public partial interface IElementGremlinQuery<TElement>
    {

    }

    public partial interface IVertexGremlinQuery<TVertex>
    {

    }

    public partial interface IEdgeGremlinQuery<TEdge>
    {

    }

    public partial interface IEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {

    }

    public partial interface IEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>
    {

    }

    public partial interface IInEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {

    }

    public partial interface IOutEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {

    }

    public partial interface IVertexPropertyGremlinQuery<TValue>
    {

    }

    public partial interface IVertexPropertyGremlinQuery<TValue, TMeta>
    {

    }

    public partial interface IEdgePropertyGremlinQuery<TValue>
    {

    }

    public partial interface IOrderedVertexGremlinQuery
    {

    }

    public partial interface IOrderedEdgeGremlinQuery
    {

    }

    public partial interface IOrderedValueGremlinQuery<TElement>
    {

    }

    public partial interface IOrderedArrayGremlinQuery<TArray, TQuery>
    {

    }

    public partial interface IOrderedVertexGremlinQuery<TVertex>
    {

    }

    public partial interface IOrderedEdgeGremlinQuery<TEdge>
    {

    }

    public partial interface IOrderedEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {

    }

    public partial interface IOrderedEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>
    {

    }

    public partial interface IOrderedInEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {

    }

    public partial interface IOrderedOutEdgeGremlinQuery<TEdge, TAdjacentVertex>
    {

    }

    public partial interface IOrderedVertexPropertyGremlinQuery<TValue>
    {

    }

    public partial interface IOrderedVertexPropertyGremlinQuery<TValue, TMeta>
    {

    }

    public partial interface IOrderedEdgePropertyGremlinQuery<TValue>
    {

    }

}
#pragma warning restore CS0109 // Member does not hide an inherited member; new keyword is not required




