﻿using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using ExRam.Gremlinq.Core;

namespace ExRam.Gremlinq.Tests.Fixtures
{
    public abstract class TestContainerFixture : GremlinqFixture
    {
        private sealed class ContainerAttachedGremlinQuerySource : IGremlinQuerySource, IAsyncDisposable
        {
            private readonly IContainer _container;
            private readonly IGremlinQuerySource _baseSource;

            public ContainerAttachedGremlinQuerySource(IContainer container, IGremlinQuerySource baseSource)
            {
                _container = container;
                _baseSource = baseSource;
            }

            IGremlinQueryEnvironment IGremlinQuerySource.Environment => _baseSource.Environment;

            IEdgeGremlinQuery<TEdge> IStartGremlinQuery.AddE<TEdge>(TEdge edge) => _baseSource.AddE(edge);

            IEdgeGremlinQuery<TEdge> IStartGremlinQuery.AddE<TEdge>() => _baseSource.AddE<TEdge>();

            IVertexGremlinQuery<TVertex> IStartGremlinQuery.AddV<TVertex>(TVertex vertex) => _baseSource.AddV(vertex);

            IVertexGremlinQuery<TVertex> IStartGremlinQuery.AddV<TVertex>() => _baseSource.AddV<TVertex>();

            IGremlinQueryAdmin IStartGremlinQuery.AsAdmin() => _baseSource.AsAdmin();

            IGremlinQuerySource IConfigurableGremlinQuerySource.ConfigureEnvironment(Func<IGremlinQueryEnvironment, IGremlinQueryEnvironment> environmentTransformation) => _baseSource.ConfigureEnvironment(environmentTransformation);

            IEdgeGremlinQuery<object> IStartGremlinQuery.E(object id) => _baseSource.E(id);

            IEdgeGremlinQuery<object> IStartGremlinQuery.E(params object[] ids) => _baseSource.E(ids);

            IEdgeGremlinQuery<TEdge> IStartGremlinQuery.E<TEdge>(object id) => _baseSource.E<TEdge>(id);

            IEdgeGremlinQuery<TEdge> IStartGremlinQuery.E<TEdge>(params object[] ids) => _baseSource.E<TEdge>(ids);

            IValueGremlinQuery<TElement> IStartGremlinQuery.Inject<TElement>(params TElement[] elements) => _baseSource.Inject(elements);

            IEdgeGremlinQuery<TNewEdge> IStartGremlinQuery.ReplaceE<TNewEdge>(TNewEdge edge) => _baseSource.ReplaceE(edge);

            IVertexGremlinQuery<TNewVertex> IStartGremlinQuery.ReplaceV<TNewVertex>(TNewVertex vertex) => _baseSource.ReplaceV(vertex);

            IVertexGremlinQuery<object> IStartGremlinQuery.V(object id) => _baseSource.V(id);

            IVertexGremlinQuery<object> IStartGremlinQuery.V(params object[] ids) => _baseSource.V(ids);

            IVertexGremlinQuery<TVertex> IStartGremlinQuery.V<TVertex>(object id) => _baseSource.V<TVertex>(id);

            IVertexGremlinQuery<TVertex> IStartGremlinQuery.V<TVertex>(params object[] ids) => _baseSource.V<TVertex>(ids);

            IGremlinQuerySource IGremlinQuerySource.WithoutStrategies(params Type[] strategyTypes) => _baseSource.WithoutStrategies(strategyTypes);

            IGremlinQuerySource IGremlinQuerySource.WithSideEffect<TSideEffect>(StepLabel<TSideEffect> label, TSideEffect value) => _baseSource.WithSideEffect(label, value);

            TQuery IGremlinQuerySource.WithSideEffect<TSideEffect, TQuery>(TSideEffect value, Func<IGremlinQuerySource, StepLabel<TSideEffect>, TQuery> continuation) => _baseSource.WithSideEffect(value, continuation);

            IGremlinQuerySource IGremlinQuerySource.WithStrategy<TStrategy>(Func<IGremlinQuerySource, TStrategy> factory) => _baseSource.WithStrategy(factory);

            IGremlinQuerySource IGremlinQuerySource.WithStrategy<TStrategy>(TStrategy strategy) => _baseSource.WithStrategy(strategy);

            async ValueTask IAsyncDisposable.DisposeAsync()
            {
                await using(_container)
                {
                    await _container.StopAsync();
                }
            }
        }

        private readonly int _port;
        private readonly string _image;

        protected TestContainerFixture(string image, int port = 8182)
        {
            _port = port;
            _image = image;
        }

        protected abstract Task<IGremlinQuerySource> TransformQuerySource(IContainer container, IConfigurableGremlinQuerySource g);

        protected override sealed async Task<IGremlinQuerySource> TransformQuerySource(IConfigurableGremlinQuerySource g)
        {
            var container = new ContainerBuilder()
                .WithImage(_image)
                .WithPortBinding(_port, true)
                .WithWaitStrategy(Wait
                    .ForUnixContainer()
                .UntilPortIsAvailable(_port))
                .Build();

            await container.StartAsync();

            return new ContainerAttachedGremlinQuerySource(container, await TransformQuerySource(container, g));
        }
    }
}