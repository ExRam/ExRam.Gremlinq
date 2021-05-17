﻿namespace ExRam.Gremlinq.Core.Projections
{
    public sealed class VertexProjection : EdgeOrVertexProjection
    {
        public override Traversal ToTraversal(IGremlinQueryEnvironment environment) => environment.Options.GetValue(environment.FeatureSet.Supports(VertexFeatures.MetaProperties)
            ? GremlinqOption.VertexProjectionSteps
            : GremlinqOption.VertexProjectionWithoutMetaPropertiesSteps);

        public override Projection Lower() => EdgeOrVertex;
    }
}
