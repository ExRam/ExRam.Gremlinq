﻿namespace ExRam.Gremlinq.Core
{
    public interface IBothEdgeGremlinQueryBase :
        IInOrOutEdgeGremlinQueryBase,
        IOutEdgeGremlinQueryBase,
        IInEdgeGremlinQueryBase
    {
        new IEdgeOrVertexGremlinQuery<object> Lower();
    }
    
    public interface IBothEdgeGremlinQueryBase<TEdge, TOutVertex, TInVertex> :
        IBothEdgeGremlinQueryBase,
        IOutEdgeGremlinQueryBase<TEdge, TOutVertex>,
        IInEdgeGremlinQueryBase<TEdge, TInVertex>
    {
        new IEdgeOrVertexGremlinQuery<TEdge> Lower();
    }

    public interface IBothEdgeGremlinQueryRec<TSelf> :
        IBothEdgeGremlinQueryBase,
        IInOrOutEdgeGremlinQueryBaseRec<TSelf>,
        IOutEdgeGremlinQueryBaseRec<TSelf>,
        IInEdgeGremlinQueryBaseRec<TSelf>
        where TSelf : IBothEdgeGremlinQueryRec<TSelf>
    {

    }

    public interface IBothEdgeGremlinQueryRec<TEdge, TOutVertex, TInVertex, TSelf> :
        IBothEdgeGremlinQueryRec<TSelf>,
        IBothEdgeGremlinQueryBase<TEdge, TOutVertex, TInVertex>,
        IEdgeGremlinQueryBaseRec<TEdge, TSelf>
        where TSelf : IBothEdgeGremlinQueryRec<TEdge, TOutVertex, TInVertex, TSelf>
    {

    }

    public interface IBothEdgeGremlinQuery<TEdge, TOutVertex, TInVertex> :
        IBothEdgeGremlinQueryRec<TEdge, TOutVertex, TInVertex, IBothEdgeGremlinQuery<TEdge, TOutVertex, TInVertex>>
    {

    }
}
