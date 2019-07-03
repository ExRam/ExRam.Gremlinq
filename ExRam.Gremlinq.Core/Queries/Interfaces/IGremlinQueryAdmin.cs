﻿using System;
using System.Collections.Immutable;

namespace ExRam.Gremlinq.Core
{
    public interface IGremlinQueryAdmin
    {
        IGremlinQuery ConfigureSteps(Func<IImmutableList<Step>, IImmutableList<Step>> configurator);
        TTargetQuery ChangeQueryType<TTargetQuery>() where TTargetQuery : IGremlinQuery;

        IImmutableList<Step> Steps { get; }
        IGremlinQueryEnvironment Environment { get; }
    }
}
