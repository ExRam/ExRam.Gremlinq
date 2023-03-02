﻿using System.Diagnostics.CodeAnalysis;
using ExRam.Gremlinq.Core.Transformation;

namespace ExRam.Gremlinq.Core
{
    public static class TransformerClassExtensions
    {
        public readonly struct TryTransformToBuilder<TTarget>
            where TTarget : class
        {
            private readonly ITransformer _transformer;

            public TryTransformToBuilder(ITransformer transformer)
            {
                _transformer = transformer;
            }

            public TTarget? From<TSource>(TSource source, IGremlinQueryEnvironment environment) => _transformer.TryTransform<TSource, TTarget>(source, environment, out var value)
                ? value
                : default;
        }

        private sealed class FixedTypeConverterFactory<TStaticSource, TStaticTarget> : IConverterFactory
           where TStaticTarget : class
        {
            private sealed class FixedTypeConverter<TSource> : IConverter<TSource, TStaticTarget>
            {
                private readonly Func<TStaticSource, IGremlinQueryEnvironment, ITransformer, ITransformer, TStaticTarget?> _func;

                public FixedTypeConverter(Func<TStaticSource, IGremlinQueryEnvironment, ITransformer, ITransformer, TStaticTarget?> func)
                {
                    _func = func;
                }

                public bool TryConvert(TSource source, IGremlinQueryEnvironment environment, ITransformer defer, ITransformer recurse, [NotNullWhen(true)] out TStaticTarget? value)
                {
                    if (source is TStaticSource staticSerialized && _func(staticSerialized, environment, defer, recurse) is { } requested)
                    {
                        value = requested;

                        return true;
                    }

                    value = default;

                    return false;
                }
            }

            private readonly Func<TStaticSource, IGremlinQueryEnvironment, ITransformer, ITransformer, TStaticTarget?> _func;

            public FixedTypeConverterFactory(Func<TStaticSource, IGremlinQueryEnvironment, ITransformer, ITransformer, TStaticTarget?> func)
            {
                _func = func;
            }

            public IConverter<TSource, TTarget>? TryCreate<TSource, TTarget>()
            {
                return ((typeof(TSource).IsAssignableFrom(typeof(TStaticSource)) || typeof(TStaticSource).IsAssignableFrom(typeof(TSource))) && typeof(TTarget).IsAssignableFrom(typeof(TStaticTarget)))
                    ? (IConverter<TSource, TTarget>)(object)new FixedTypeConverter<TSource>(_func)
                    : null;
            }
        }

        public static TryTransformToBuilder<TTarget> TryTransformTo<TTarget>(this ITransformer transformer)
            where TTarget : class => new(transformer);

        public static ITransformer Add<TSource, TTarget>(this ITransformer transformer, Func<TSource, IGremlinQueryEnvironment, ITransformer, TTarget?> func)
            where TTarget : class => transformer.Override<TSource, TTarget>((source, env, _, recurse) => func(source, env, recurse));

        public static ITransformer Override<TSource, TTarget>(this ITransformer transformer, Func<TSource, IGremlinQueryEnvironment, ITransformer, ITransformer, TTarget?> func)
            where TTarget : class => transformer.Add(new FixedTypeConverterFactory<TSource, TTarget>(func));
    }
}