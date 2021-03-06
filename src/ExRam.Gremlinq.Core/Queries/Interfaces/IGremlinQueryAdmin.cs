﻿using System;
using ExRam.Gremlinq.Core.Projections;
using ExRam.Gremlinq.Core.Steps;

namespace ExRam.Gremlinq.Core
{
    public interface IGremlinQueryAdmin
    {
        TTargetQuery ConfigureSteps<TTargetQuery>(Func<StepStack, StepStack> configurator, Func<Projection, Projection>? projectionTransformation = null) where TTargetQuery : IGremlinQueryBase;
        TTargetQuery AddStep<TTargetQuery>(Step step, Func<Projection, Projection>? projectionTransformation = null) where TTargetQuery : IGremlinQueryBase;

        TTargetQuery ChangeQueryType<TTargetQuery>(Projection? forceProjection = null) where TTargetQuery : IGremlinQueryBase;

        IGremlinQuerySource GetSource();

        StepStack Steps { get; }
        Type ElementType { get; }
        Projection Projection { get; }
        IGremlinQueryEnvironment Environment { get; }
    }
}
