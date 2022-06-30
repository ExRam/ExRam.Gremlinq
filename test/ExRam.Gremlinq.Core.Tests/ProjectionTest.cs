﻿using ExRam.Gremlinq.Core.Models;
using ExRam.Gremlinq.Tests.Entities;
using static ExRam.Gremlinq.Core.GremlinQuerySource;

namespace ExRam.Gremlinq.Core.Tests
{
    public class ProjectionTest : GremlinqTestBase
    {
        private readonly IGremlinQuerySource _g;

        public ProjectionTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _g = g
                .ConfigureEnvironment(x => x.UseModel(GraphModel
                    .FromBaseTypes<Vertex, Edge>()));
        }

        [Fact]
        public virtual Task Coalesce_with_2_subQueries_has_right_semantics()
        {
            return Verify(_g
                .V()
                .Coalesce(
                    _ => _.Out(),
                    _ => _.In())
                .ToTraversal()
                .Projection);
        }

        [Fact]
        public virtual Task Coalesce_with_2_not_matching_subQueries_has_right_semantics()
        {
            return Verify(_g
                .V()
                .Coalesce(
                    _ => _.OutE(),
                    _ => _.In())
                .ToTraversal()
                .Projection);
        }

        [Fact]
        public virtual Task ForceEdge_will_not_preserve_Vertex()
        {
            return Verify(_g
                .V()
                .ForceEdge()
                .ToTraversal()
                .Projection);
        }

        [Fact]
        public virtual Task ForceElement_will_preserve_Vertex()
        {
            return Verify(_g
                .V()
                .ForceElement()
                .ToTraversal()
                .Projection);
        }

        [Fact]
        public virtual Task ForceValue_will_not_preserve_Vertex()
        {
            return Verify(_g
                .V()
                .ForceValue()
                .ToTraversal()
                .Projection);
        }

        [Fact]
        public virtual Task Unfold_will_get_Vertex_back()
        {
            return Verify(_g
                .V()
                .Fold()
                .Unfold()
                .ToTraversal()
                .Projection);
        }
    }
}
