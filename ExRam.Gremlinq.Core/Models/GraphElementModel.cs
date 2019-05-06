﻿using System;
using System.Collections.Immutable;
using LanguageExt;

namespace ExRam.Gremlinq.Core
{
    public static class GraphElementModel
    {
        private sealed class EmptyGraphElementModel : IGraphElementModel
        {
            public IImmutableDictionary<Type, string> Labels => ImmutableDictionary<Type, string>.Empty;

            public Option<string[]> TryGetFilterLabels(Type elementType) => default;
        }

        private sealed class InvalidGraphElementModel : IGraphElementModel
        {
            private const string ErrorMessage = "'{0}' must not be called on GraphElementModel.Invalid. If you are getting this exception while executing a query, set a proper GraphModel on the GremlinQuerySource (e.g. by calling 'g.WithModel(...)').";

            public IImmutableDictionary<Type, string> Labels => throw new InvalidOperationException(string.Format(ErrorMessage, nameof(Labels)));

            public Option<string[]> TryGetFilterLabels(Type elementType) => throw new InvalidOperationException(string.Format(ErrorMessage, nameof(TryGetFilterLabels)));
        }

        public static readonly IGraphElementModel Empty = new EmptyGraphElementModel();
        public static readonly IGraphElementModel Invalid = new InvalidGraphElementModel();

        internal static string[] GetValidFilterLabels(this IGraphElementModel model, Type type)
        {
            return model.TryGetFilterLabels(type)
                .IfNone(() => throw new GraphModelException($"Can't determine labels for type {type.FullName}."));
        }
    }
}
