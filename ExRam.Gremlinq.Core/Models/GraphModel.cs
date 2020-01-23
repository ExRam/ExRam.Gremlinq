﻿using System;
using System.Collections.Immutable;
using System.Reflection;
using ExRam.Gremlinq.Core.GraphElements;
using Microsoft.Extensions.Logging;

namespace ExRam.Gremlinq.Core
{
    public static class GraphModel
    {
        private sealed class GraphModelImpl : IGraphModel
        {
            public GraphModelImpl(IGraphElementModel verticesModel, IGraphElementModel edgesModel, IGraphElementPropertyModel propertiesModel)
            {
                VerticesModel = verticesModel;
                EdgesModel = edgesModel;
                PropertiesModel = propertiesModel;
            }

            public IGraphElementModel VerticesModel { get; }

            public IGraphElementModel EdgesModel { get; }

            public IGraphElementPropertyModel PropertiesModel { get; }
        }

        private sealed class EmptyGraphModel : IGraphModel
        {
            public IGraphElementModel VerticesModel => GraphElementModel.Empty;
            public IGraphElementModel EdgesModel => GraphElementModel.Empty;

            public IGraphElementPropertyModel PropertiesModel { get; } = GraphElementPropertyModel.Default;
        }

        public static readonly IGraphModel Empty = new EmptyGraphModel();

        public static IGraphModel Default(Func<IAssemblyLookupBuilder, IAssemblyLookupSet> assemblyLookupTransformation, ILogger? logger = null)
        {
            return FromBaseTypes<IVertex, IEdge>(assemblyLookupTransformation, logger);
        }

        public static IGraphModel FromBaseTypes<TVertex, TEdge>(Func<IAssemblyLookupBuilder, IAssemblyLookupSet> assemblyLookupTransformation, ILogger? logger = null)
        {
            return FromBaseTypes(typeof(TVertex), typeof(TEdge), assemblyLookupTransformation, logger);
        }

        public static IGraphModel FromBaseTypes(Type vertexBaseType, Type edgeBaseType, Func<IAssemblyLookupBuilder, IAssemblyLookupSet> assemblyLookupTransformation,  ILogger? logger = null)
        {
            if (vertexBaseType.IsAssignableFrom(edgeBaseType))
                throw new ArgumentException($"{vertexBaseType} may not be in the inheritance hierarchy of {edgeBaseType}.");

            if (edgeBaseType.IsAssignableFrom(vertexBaseType))
                throw new ArgumentException($"{edgeBaseType} may not be in the inheritance hierarchy of {vertexBaseType}.");

            var assemblies = assemblyLookupTransformation(new AssemblyLookupSet(new []{ vertexBaseType, edgeBaseType}, ImmutableList<Assembly>.Empty))
                .Assemblies;

            var verticesModel = GraphElementModel.FromBaseType(vertexBaseType, assemblies, logger);
            var edgesModel = GraphElementModel.FromBaseType(edgeBaseType, assemblies, logger);

            return new GraphModelImpl(
                verticesModel,
                edgesModel,
                GraphElementPropertyModel.FromGraphElementModels(verticesModel, edgesModel));
        }

        public static IGraphModel FromTypes(Type[] vertexTypes, Type[] edgeTypes)
        {
            var verticesModel = GraphElementModel.FromTypes(vertexTypes);
            var edgesModel = GraphElementModel.FromTypes(edgeTypes);

            return new GraphModelImpl(
                verticesModel,
                edgesModel,
                GraphElementPropertyModel.FromGraphElementModels(verticesModel, edgesModel));
        }

        public static IGraphModel ConfigureElements(this IGraphModel model, Func<IGraphElementModel, IGraphElementModel> transformation)
        {
            return model
                .ConfigureVertices(transformation)
                .ConfigureEdges(transformation);
        }

        public static IGraphModel ConfigureVertices(this IGraphModel model, Func<IGraphElementModel, IGraphElementModel> transformation)
        {
            return new GraphModelImpl(
                transformation(model.VerticesModel),
                model.EdgesModel,
                model.PropertiesModel);
        }

        public static IGraphModel ConfigureEdges(this IGraphModel model, Func<IGraphElementModel, IGraphElementModel> transformation)
        {
            return new GraphModelImpl(
                model.VerticesModel,
                transformation(model.EdgesModel),
                model.PropertiesModel);
        }

        public static IGraphModel ConfigureProperties(this IGraphModel model, Func<IGraphElementPropertyModel, IGraphElementPropertyModel> transformation)
        {
            return new GraphModelImpl(
                model.VerticesModel,
                model.EdgesModel,
                transformation(model.PropertiesModel));
        }
    }
}
