﻿using ExRam.Gremlinq.Core.Serialization;

namespace ExRam.Gremlinq.Core
{
    public sealed class AddVStep : AddElementStep
    {
        public AddVStep(IGraphModel model, object value) : base(model.VertexModel, value)
        {
        }

        public override void Accept(IGremlinQueryElementVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
