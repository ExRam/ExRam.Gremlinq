﻿namespace ExRam.Gremlinq.Core
{
    public interface IStartGremlinQuery
    {
        IVertexGremlinQuery<TVertex> AddV<TVertex>(TVertex vertex);
        IVertexGremlinQuery<TVertex> AddV<TVertex>() where TVertex : new();

        IEdgeGremlinQuery<TEdge> AddE<TEdge>(TEdge edge);
        IEdgeGremlinQuery<TEdge> AddE<TEdge>() where TEdge : new();

        IGremlinQueryAdmin AsAdmin();

        IEdgeGremlinQuery<object> E(object id);
        IEdgeGremlinQuery<object> E(params object[] ids);
        IEdgeGremlinQuery<TEdge> E<TEdge>(object id);
        IEdgeGremlinQuery<TEdge> E<TEdge>(params object[] ids);

        IVertexGremlinQuery<object> V(object id);
        IVertexGremlinQuery<object> V(params object[] ids);
        IVertexGremlinQuery<TVertex> V<TVertex>(object id);
        IVertexGremlinQuery<TVertex> V<TVertex>(params object[] ids);

        IGremlinQuery<TElement> Inject<TElement>(params TElement[] elements);

        IEdgeGremlinQuery<TNewEdge> ReplaceE<TNewEdge>(TNewEdge edge);
        IVertexGremlinQuery<TNewVertex> ReplaceV<TNewVertex>(TNewVertex vertex);
    }
}
