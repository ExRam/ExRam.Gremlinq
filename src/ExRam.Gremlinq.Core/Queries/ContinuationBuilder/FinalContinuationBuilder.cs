﻿using System;
using System.Collections.Immutable;
using ExRam.Gremlinq.Core.Projections;
using ExRam.Gremlinq.Core.Steps;

namespace ExRam.Gremlinq.Core
{
    internal readonly struct FinalContinuationBuilder<TOuterQuery>
        where TOuterQuery : GremlinQueryBase
    {
        private readonly StepStack? _steps;
        private readonly TOuterQuery? _outer;
        private readonly Projection? _projection;
        private readonly QueryFlags _additionalFlags = QueryFlags.None;
        private readonly IImmutableDictionary<StepLabel, Projection>? _stepLabelProjections;

        public FinalContinuationBuilder(TOuterQuery outerQuery) : this(outerQuery, outerQuery.Steps, outerQuery.Projection, outerQuery.StepLabelProjections, outerQuery.Flags)
        {

        }

        public FinalContinuationBuilder(TOuterQuery outerQuery, StepStack steps, Projection projection, IImmutableDictionary<StepLabel, Projection> stepLabelProjections, QueryFlags additionalFlags)
        {
            _steps = steps;
            _outer = outerQuery;
            _projection = projection;
            _additionalFlags = additionalFlags;
            _stepLabelProjections = stepLabelProjections;
        }

        public FinalContinuationBuilder<TOuterQuery> AddStep<TStep>(TStep step)
             where TStep : Step
        {
            return _outer is { } outer && _steps is { } steps && _projection is { } projection && _stepLabelProjections is { } stepLabelProjections
                ? outer.Flags.HasFlag(QueryFlags.IsMuted)
                    ? this
                    : new(outer, outer.Environment.AddStepHandler.AddStep(steps, step, outer.Environment), projection, stepLabelProjections, _additionalFlags)
                : throw new InvalidOperationException();
        }

        public FinalContinuationBuilder<TOuterQuery> WithNewProjection(Func<Projection, Projection> projectionTransformation)
        {
            return _outer is { } query && _steps is { } steps && _stepLabelProjections is { } stepLabelProjections
                ? new(_outer, steps, projectionTransformation(_projection ?? Projection.Empty), _stepLabelProjections, _additionalFlags)
                : throw new InvalidOperationException();
        }

        public FinalContinuationBuilder<TOuterQuery> WithNewProjection(Projection newProjection)
        {
            return _outer is { } query && _steps is { } steps && _stepLabelProjections is { } stepLabelProjections
                ? new(_outer, steps, newProjection, _stepLabelProjections, _additionalFlags)
                : throw new InvalidOperationException();
        }

        public FinalContinuationBuilder<TOuterQuery> WithNewStepLabelProjection(Func<IImmutableDictionary<StepLabel, Projection>, IImmutableDictionary<StepLabel, Projection>> stepLabelProjectionsTransformation)
        {
            return _outer is { } query && _steps is { } steps && _projection is { } projection && _stepLabelProjections is { } stepLabelProjections
                ? new(_outer, steps, projection, stepLabelProjectionsTransformation(stepLabelProjections), _additionalFlags)
                : throw new InvalidOperationException();
        }

        public FinalContinuationBuilder<TOuterQuery> WithNewStepLabelProjection(IImmutableDictionary<StepLabel, Projection> newStepLabelProjections)
        {
            return _outer is { } query && _steps is { } steps && _projection is { } projection
                ? new(_outer, steps, projection, newStepLabelProjections, _additionalFlags)
                : throw new InvalidOperationException();
        }

        public FinalContinuationBuilder<TOuterQuery> WithAdditionalFlags(QueryFlags additionalFlags)
        {
            return _outer is { } query && _steps is { } steps && _projection is { } projection && _stepLabelProjections is { } stepLabelProjections
                ? new(_outer, steps, projection, stepLabelProjections, _additionalFlags | additionalFlags)
                : throw new InvalidOperationException();
        }

        public TOuterQuery Build()
        {
            return Build<TOuterQuery>();
        }

        public TNewTargetQuery Build<TNewTargetQuery>()
        {
            return _outer is { } query
                ? query.CloneAs<TNewTargetQuery>(
                    maybeStepStackTransformation: _steps is { } newSteps ? _ => newSteps : null,
                    maybeProjectionTransformation: _projection is { } newProjection ? _ => newProjection : null,
                    maybeStepLabelProjectionsTransformation: _stepLabelProjections is { } newStepLabelProjections ? _ => newStepLabelProjections : null,
                    maybeQueryFlagsTransformation: _additionalFlags is { } additionalFlags ? flags => flags | additionalFlags : null)
                : throw new InvalidOperationException();
        }

        public TOuterQuery OuterQuery => _outer ?? throw new InvalidOperationException();
    }
}
