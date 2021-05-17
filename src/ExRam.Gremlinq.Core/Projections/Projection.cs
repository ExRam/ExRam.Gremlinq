﻿using System;
using System.Linq;

using ExRam.Gremlinq.Core.Steps;

namespace ExRam.Gremlinq.Core.Projections
{
    public abstract class Projection
    {
        public static readonly EdgeProjection Edge = new EdgeProjection();
        public static readonly EmptyProjection Empty = new EmptyProjection();
        public static readonly ValueProjection Value = new ValueProjection();
        public static readonly VertexProjection Vertex = new VertexProjection();
        public static readonly ElementProjection Element = new ElementProjection();
        public static readonly EdgeOrVertexProjection EdgeOrVertex = new EdgeOrVertexProjection();

        public abstract Traversal ToTraversal(IGremlinQueryEnvironment environment);

        public ArrayProjection Fold() => new(this);

        public TupleProjection Project(ProjectStep projectStep, ProjectStep.ByStep[] bySteps)
        {
            if (projectStep.Projections.Length != bySteps.Length)
                throw new ArgumentException();

            return new TupleProjection(projectStep.Projections
                .Select((key, i) =>
                {
                    var projection = bySteps[i] is ProjectStep.ByTraversalStep byTraversal
                        ? byTraversal.Traversal.Projection
                        : Empty;

                    return (key, projection);
                })
                .ToArray());
        }

        internal Projection If<TProjection>(Func<TProjection, Projection> transformation)
            where TProjection : Projection
        {
            if (this is TProjection projection)
                return transformation(projection);

            return this;
        }

        internal Projection Lowest(Projection other)
        {
            var @this = this;

            while (@this != Empty)
            {
                if (other.GetType().IsInstanceOfType(@this))
                    return other;

                if (@this.GetType().IsInstanceOfType(other))
                    return @this;

                @this = @this.BaseProjection;
            }

            return Empty;
        }

        internal Projection Highest(Projection other)
        {
            if (GetType().IsAssignableFrom(other.GetType()))
                return other;

            return this;
        }

        public abstract Projection BaseProjection { get; }

        public string Name { get => ToString()!; }
    }
}
