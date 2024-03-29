﻿using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Reflection;
using ExRam.Gremlinq.Core.Models;
using Gremlin.Net.Process.Traversal;

namespace ExRam.Gremlinq.Core
{
    internal static class ObjectExtensions
    {
        private static readonly ConcurrentDictionary<Type, Func<object, IGremlinQueryEnvironment, SerializationBehaviour, IEnumerable<(Key key, object? value)>>> SerializerDict = new();

        public static TSource Apply<TSource>(this TSource source, Action<TSource> application)
        {
            application(source);
            return source;
        }

        public static TSource Apply<TSource, TState>(this TSource source, Action<TSource, TState> application, TState state)
        {
            application(source, state);
            return source;
        }

        public static TResult Map<TSource, TResult>(this TSource source, Func<TSource, TResult> transformation) => transformation(source);

        public static TResult Map<TSource, TResult, TState>(this TSource source, Func<TSource, TState, TResult> transformation, TState state) => transformation(source, state);

        public static IEnumerable<(Key key, object? value)> Serialize(
            this object? obj,
            IGremlinQueryEnvironment environment,
            SerializationBehaviour ignoreMask)
        {
            if (obj == null)
                return Array.Empty<(Key key, object? value)>();

            var func = SerializerDict
                .GetOrAdd(
                    obj.GetType(),
                    static closureType =>
                    {
                        var interfaces = closureType.GetInterfaces();

                        foreach (var iface in interfaces)
                        {
                            if (iface.IsGenericType && iface.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                            {
                                return (Func<object, IGremlinQueryEnvironment, SerializationBehaviour, IEnumerable<(Key key, object? value)>>)typeof(ObjectExtensions)
                                    .GetMethod(nameof(CreateSerializeDictionaryFunc), BindingFlags.Static | BindingFlags.NonPublic)!
                                    .MakeGenericMethod(iface.GetGenericArguments())
                                    .Invoke(null, Array.Empty<object>())!;
                            }
                        }

                        return SerializeObject;
                    });

            return func(obj, environment, ignoreMask);
        }

        public static object GetId(this object element, IGremlinQueryEnvironment environment)
        {
            var (propertyInfo, _) = environment.GetCache().GetSerializationData(element.GetType())
                .FirstOrDefault(static info => info.metadata.Key.RawKey is T t && T.Id.Equals(t));

            return propertyInfo?.GetValue(element) ?? throw new InvalidOperationException($"Unable to determine Id for {element}");
        }

        private static Func<object, IGremlinQueryEnvironment, SerializationBehaviour, IEnumerable<(Key key, object? value)>> CreateSerializeDictionaryFunc<TKey, TValue>()
        {
            return static (dict, _, _) => SerializeDictionary((IDictionary<TKey, TValue>)dict);
        }

        private static IEnumerable<(Key key, object? value)> SerializeDictionary<TKey, TValue>(IDictionary<TKey, TValue> dict)
        {
            foreach (var kvp in dict)
            {
                if (kvp is { Key: {} key, Value: var value } && key.ToString() is { } stringKey)
                    yield return (stringKey, value);
            }
        }

        private static IEnumerable<(Key key, object? value)> SerializeObject(object obj, IGremlinQueryEnvironment environment, SerializationBehaviour ignoreMask)
        {
            var serializationBehaviourOverrides = environment.Options
                .GetValue(GremlinqOption.TSerializationBehaviourOverrides);

            foreach (var (propertyInfo, metadata) in environment.GetCache().GetSerializationData(obj.GetType()))
            {
                var actualSerializationBehaviour = metadata.SerializationBehaviour;

                if (metadata.Key.RawKey is T t)
                {
                    actualSerializationBehaviour |= serializationBehaviourOverrides
                        .GetValueOrDefault(t, SerializationBehaviour.Default);
                }

                if ((actualSerializationBehaviour & ignoreMask) == 0)
                    yield return (metadata.Key, propertyInfo.GetValue(obj));
            }
        }
    }
}
