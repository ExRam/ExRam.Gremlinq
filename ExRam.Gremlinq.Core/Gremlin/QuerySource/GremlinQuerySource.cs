﻿using System;
using System.Collections.Immutable;
using ExRam.Gremlinq.Core.GraphElements;
using LanguageExt;

namespace ExRam.Gremlinq.Core
{
    public static class GremlinQuerySource
    {
        // ReSharper disable once InconsistentNaming
        #pragma warning disable IDE1006 // Naming Styles
        public static readonly IConfigurableGremlinQuerySource g = Create("g");
        #pragma warning restore IDE1006 // Naming Styles
    
        public static IConfigurableGremlinQuerySource Create(string name)
        {
            return new ConfigurableGremlinQuerySourceImpl(name, GraphModel.Invalid, GremlinQueryExecutor.Invalid, ImmutableList<IGremlinQueryStrategy>.Empty);
        }

        private sealed class ConfigurableGremlinQuerySourceImpl : IConfigurableGremlinQuerySource
        {
            private readonly string _name;
            private readonly IGraphModel _model;
            private readonly IGremlinQueryExecutor _queryExecutor;
            private readonly ImmutableList<IGremlinQueryStrategy> _strategies;

            public ConfigurableGremlinQuerySourceImpl(string name, IGraphModel model, IGremlinQueryExecutor queryExecutor, ImmutableList<IGremlinQueryStrategy> strategies)
            {
                _name = name;
                _model = model;
                _strategies = strategies;
                _queryExecutor = queryExecutor;
            }

            public IVGremlinQuery<TVertex> AddV<TVertex>(TVertex vertex)
            {
                return Create()
                    .AddV(vertex);
            }

            public IVGremlinQuery<TVertex> AddV<TVertex>() where TVertex : new()
            {
                return Create()
                    .AddV<TVertex>();
            }

            public IEGremlinQuery<TEdge> AddE<TEdge>(TEdge edge)
            {
                return Create()
                    .AddE(edge);
            }

            public IEGremlinQuery<TEdge> AddE<TEdge>() where TEdge : new()
            {
                return Create()
                    .AddE<TEdge>();
            }

            public IVGremlinQuery<IVertex> V(params object[] ids)
            {
                return Create()
                    .V(ids);
            }

            public IVGremlinQuery<TVertex> V<TVertex>(params object[] ids)
            {
                return Create()
                    .V<TVertex>(ids);
            }

            public IEGremlinQuery<IEdge> E(params object[] ids)
            {
                return Create()
                    .E(ids);
            }

            public IEGremlinQuery<TEdge> E<TEdge>(params object[] ids)
            {
                return Create()
                    .E<TEdge>(ids);
            }

            public IGremlinQuery<TElement> Inject<TElement>(params TElement[] elements)
            {
                return Create()
                    .Cast<TElement>()
                    .Inject(elements);
            }

            public IConfigurableGremlinQuerySource WithStrategies(params IGremlinQueryStrategy[] strategies)
            {
                return new ConfigurableGremlinQuerySourceImpl(_name, _model, _queryExecutor, _strategies.AddRange(strategies));
            }

            public IConfigurableGremlinQuerySource WithModel(IGraphModel model)
            {
                return new ConfigurableGremlinQuerySourceImpl(_name, model, _queryExecutor, _strategies);
            }

            public IConfigurableGremlinQuerySource WithExecutor(IGremlinQueryExecutor executor)
            {
                return new ConfigurableGremlinQuerySourceImpl(_name, _model, executor, _strategies);
            }

            private IGremlinQuery<Unit> Create()
            {
                var model = _model == GraphModel.Invalid
                    ? GraphModel.Dynamic()
                    : _model;

                var ret =
                    new GremlinQueryImpl<Unit, Unit, Unit>(
                            model,
                            _queryExecutor,
                            ImmutableList<Step>.Empty,
                            ImmutableDictionary<StepLabel, string>.Empty)
                        .AddStep(IdentifierStep.Create(_name));

                foreach (var strategy in _strategies)
                {
                    ret = strategy.Apply(ret);
                }

                return ret;
            }
        }
    }
}
