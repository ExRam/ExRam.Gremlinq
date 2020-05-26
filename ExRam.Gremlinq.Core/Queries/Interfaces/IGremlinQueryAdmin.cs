﻿using System;
using System.Collections.Immutable;

namespace ExRam.Gremlinq.Core
{
    public interface IGremlinQueryAdmin
    {
        IValueGremlinQuery<object> ConfigureSteps(Func<IImmutableStack<Step>, IImmutableStack<Step>> configurator);
        TTargetQuery AddStep<TTargetQuery>(Step step) where TTargetQuery : IGremlinQueryBase;

        TTargetQuery ChangeQueryType<TTargetQuery>() where TTargetQuery : IGremlinQueryBase;

        Traversal ToTraversal();

        IImmutableStack<Step> Steps { get; }
        IGremlinQueryEnvironment Environment { get; }
    }
}
