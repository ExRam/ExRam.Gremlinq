﻿using System.Collections.Generic;
using System.Collections.Immutable;
using ExRam.Gremlinq.Core.Models;
using ExRam.Gremlinq.Core.Steps;
using Gremlin.Net.Process.Traversal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ExRam.Gremlinq.Core
{
    public static class GremlinqOption
    {
        public static readonly GremlinqOption<Traversal> VertexProjectionSteps = new(Traversal.Empty.Push(
            new ProjectStep(ImmutableArray.Create("id", "label", "properties")),
            new ProjectStep.ByKeyStep(T.Id),
            new ProjectStep.ByKeyStep(T.Label),
            new ProjectStep.ByTraversalStep(Traversal.Empty.Push(
                new PropertiesStep(ImmutableArray<string>.Empty),
                GroupStep.Instance,
                new GroupStep.ByKeyStep(T.Label),
                new GroupStep.ByTraversalStep(Traversal.Empty.Push(
                    new ProjectStep(ImmutableArray.Create("id", "label", "value", "properties")),
                    new ProjectStep.ByKeyStep(T.Id),
                    new ProjectStep.ByKeyStep(T.Label),
                    new ProjectStep.ByKeyStep(T.Value),
                    new ProjectStep.ByTraversalStep(new ValueMapStep(ImmutableArray<string>.Empty)),
                    FoldStep.Instance))))));

        public static readonly GremlinqOption<Traversal> VertexProjectionWithoutMetaPropertiesSteps = new(Traversal.Empty.Push(
            new ProjectStep(ImmutableArray.Create("id", "label", "properties")),
            new ProjectStep.ByKeyStep(T.Id),
            new ProjectStep.ByKeyStep(T.Label),
            new ProjectStep.ByTraversalStep(Traversal.Empty.Push(
                new PropertiesStep(ImmutableArray<string>.Empty),
                GroupStep.Instance,
                new GroupStep.ByKeyStep(T.Label),
                new GroupStep.ByTraversalStep(Traversal.Empty.Push(
                    new ProjectStep(ImmutableArray.Create("id", "label", "value")),
                    new ProjectStep.ByKeyStep(T.Id),
                    new ProjectStep.ByKeyStep(T.Label),
                    new ProjectStep.ByKeyStep(T.Value),
                    FoldStep.Instance))))));

        public static readonly GremlinqOption<Traversal> VertexPropertyProjectionSteps = new(Traversal.Empty.Push(
            new ProjectStep(ImmutableArray.Create("id", "label", "value", "properties")),
            new ProjectStep.ByKeyStep(T.Id),
            new ProjectStep.ByKeyStep(T.Label),
            new ProjectStep.ByKeyStep(T.Value),
            new ProjectStep.ByTraversalStep(new ValueMapStep(ImmutableArray<string>.Empty))));

        public static readonly GremlinqOption<Traversal> VertexPropertyProjectionWithoutMetaPropertiesSteps = new(Traversal.Empty.Push(
            new ProjectStep(ImmutableArray.Create("id", "label", "value")),
            new ProjectStep.ByKeyStep(T.Id),
            new ProjectStep.ByKeyStep(T.Label),
            new ProjectStep.ByKeyStep(T.Value)));

        public static readonly GremlinqOption<Traversal> EdgeProjectionSteps = new(Traversal.Empty.Push(
            new ProjectStep(ImmutableArray.Create("id", "label", "properties")),
            new ProjectStep.ByKeyStep(T.Id),
            new ProjectStep.ByKeyStep(T.Label),
            new ProjectStep.ByTraversalStep(new ValueMapStep(ImmutableArray<string>.Empty))));

        public static readonly GremlinqOption<Traversal> EmptyProjectionProtectionDecoratorSteps = new(Traversal.Empty.Push(
            new MapStep(Traversal.Empty.Push(
                UnfoldStep.Instance,
                GroupStep.Instance,
                new GroupStep.ByTraversalStep(new SelectColumnStep(Column.Keys)),
                new GroupStep.ByTraversalStep(Traversal.Empty.Push(
                    new SelectColumnStep(Column.Values),
                    UnfoldStep.Instance))))));

        public static readonly GremlinqOption<IImmutableDictionary<T, SerializationBehaviour>> TSerializationBehaviourOverrides = new(
            new Dictionary<T, SerializationBehaviour>
            {
                { T.Key, SerializationBehaviour.IgnoreOnUpdate },
                { T.Id, SerializationBehaviour.IgnoreOnUpdate },
                { T.Label, SerializationBehaviour.IgnoreAlways },
                { T.Value, SerializationBehaviour.Default }
            }
            .ToImmutableDictionary());

        public static readonly GremlinqOption<bool> EnableEmptyProjectionValueProtection = new (false);

        public static readonly GremlinqOption<FilterLabelsVerbosity> FilterLabelsVerbosity = new(Core.FilterLabelsVerbosity.Maximum);
        public static readonly GremlinqOption<DisabledTextPredicates> DisabledTextPredicates = new(Core.DisabledTextPredicates.None);
        public static readonly GremlinqOption<StringComparisonTranslationStrictness> StringComparisonTranslationStrictness = new(Core.StringComparisonTranslationStrictness.Strict);

        public static GremlinqOption<LogLevel> QueryLogLogLevel = new(LogLevel.Debug);
        public static GremlinqOption<Formatting> QueryLogFormatting = new(Formatting.None);
        public static GremlinqOption<QueryLogVerbosity> QueryLogVerbosity = new(Core.QueryLogVerbosity.QueryOnly);
    }

    public class GremlinqOption<TValue> : IGremlinqOption
    {
        public GremlinqOption(TValue defaultValue)
        {
            DefaultValue = defaultValue;
        }

        public TValue DefaultValue { get; }
    }
}
