﻿using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Dynamic;
using System.Numerics;
using System.Reflection;
using System.Xml;
using ExRam.Gremlinq.Core.GraphElements;
using Gremlin.Net.Process.Traversal;
using Gremlin.Net.Structure.IO.GraphSON;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ExRam.Gremlinq.Core.Deserialization
{
    public static class GremlinQueryFragmentDeserializer
    {
        private sealed class GremlinQueryFragmentDeserializerImpl : IGremlinQueryFragmentDeserializer
        {
            private static readonly MethodInfo CreateFuncMethod1 = typeof(GremlinQueryFragmentDeserializerImpl).GetMethod(nameof(CreateFunc1), BindingFlags.NonPublic | BindingFlags.Static)!;
            private static readonly MethodInfo CreateFuncMethod2 = typeof(GremlinQueryFragmentDeserializerImpl).GetMethod(nameof(CreateFunc2), BindingFlags.NonPublic | BindingFlags.Static)!;
            private static readonly MethodInfo CreateFuncMethod3 = typeof(GremlinQueryFragmentDeserializerImpl).GetMethod(nameof(CreateFunc3), BindingFlags.NonPublic | BindingFlags.Static)!;

            private readonly IImmutableDictionary<Type, Delegate> _dict;
            private readonly ConcurrentDictionary<(Type staticType, Type actualType), Delegate> _unconvertedDelegates = new();
            private readonly ConcurrentDictionary<(Delegate expression, Type staticType, Type fragmentType), Delegate> _convertedDelegates = new();

            public GremlinQueryFragmentDeserializerImpl(IImmutableDictionary<Type, Delegate> dict)
            {
                _dict = dict;
            }

            public object? TryDeserialize<TSerialized>(TSerialized serializedData, Type fragmentType, IGremlinQueryEnvironment environment)
            {
                return GetConvertedDeserializer(typeof(TSerialized), serializedData!.GetType(), fragmentType) is BaseGremlinQueryFragmentDeserializerDelegate<TSerialized> del
                    ? del(serializedData, fragmentType, environment, this)
                    : throw new ArgumentException($"Could not find a deserializer for {fragmentType.FullName}.");
            }

            public IGremlinQueryFragmentDeserializer Override<TSerialized>(GremlinQueryFragmentDeserializerDelegate<TSerialized> deserializer)
            {
                return new GremlinQueryFragmentDeserializerImpl(
                    _dict.SetItem(
                        typeof(TSerialized),
                        GetUnconvertedDeserializer(typeof(TSerialized), typeof(TSerialized)) is BaseGremlinQueryFragmentDeserializerDelegate<TSerialized> existingFragmentDeserializer
                            ? (fragment, type, env, _, recurse) => deserializer(fragment, type, env, existingFragmentDeserializer, recurse)
                            : deserializer));
            }

            private Delegate GetConvertedDeserializer(Type staticType, Type actualType, Type fragmentType)
            {
                return _convertedDelegates
                    .GetOrAdd(
                        (GetUnconvertedDeserializer(staticType, actualType), staticType, fragmentType),
                        static typeTuple =>
                        {
                            var (del, staticType, fragmentType) = typeTuple;

                            return (Delegate)CreateFuncMethod3
                                .MakeGenericMethod(staticType, fragmentType)
                                .Invoke(null, new object[] { del })!;
                        });
            }

            private Delegate GetUnconvertedDeserializer(Type staticType, Type actualType)
            {
                return _unconvertedDelegates
                    .GetOrAdd(
                        (staticType, actualType),
                        static (typeTuple, @this) =>
                        {
                            var (staticType, actualType) = typeTuple;
                        
                            if (@this.InnerLookup(actualType) is { } del)
                            {
                                var effectiveType = del
                                    .GetType()
                                    .GetGenericArguments()[0];

                                return (Delegate)CreateFuncMethod1
                                    .MakeGenericMethod(staticType, effectiveType)
                                    .Invoke(null, new object[] { del })!;
                            }

                            return (Delegate)CreateFuncMethod2
                                .MakeGenericMethod(staticType)
                                .Invoke(null, Array.Empty<object>())!;
                        },
                        this);
            }

            private static BaseGremlinQueryFragmentDeserializerDelegate<TStatic> CreateFunc1<TStatic, TEffective>(GremlinQueryFragmentDeserializerDelegate<TEffective> del)
            {
                return (serialized, fragmentType, environment, recurse) => del((TEffective)(object)serialized!, fragmentType, environment, static (serialized, _, _, _) => serialized, recurse);
            }

            private static BaseGremlinQueryFragmentDeserializerDelegate<TStatic> CreateFunc2<TStatic>()
            {
                return static (serialized, _, _, _) => serialized;
            }

            private static BaseGremlinQueryFragmentDeserializerDelegate<TSerialized> CreateFunc3<TSerialized, TFragment>(BaseGremlinQueryFragmentDeserializerDelegate<TSerialized> del)
            {
                return (serialized, fragmentType, environment, recurse) => (TFragment)del(serialized!, fragmentType, environment, recurse)!;
            }

            private Delegate? InnerLookup(Type actualType)
            {
                if (_dict.TryGetValue(actualType, out var ret))
                    return ret;

                var baseType = actualType.BaseType;

                foreach (var implementedInterface in actualType.GetInterfaces())
                {
                    if ((baseType == null || !implementedInterface.IsAssignableFrom(baseType)) && InnerLookup(implementedInterface) is { } interfaceSerializer)
                        return interfaceSerializer;
                }

                return baseType != null && InnerLookup(baseType) is { } baseSerializer
                    ? baseSerializer
                    : null;
            }
        }

        private static readonly Dictionary<string, Type> GraphSONTypes = new()
        {
            { "g:Int32", typeof(int) },
            { "g:Int64", typeof(long) },
            { "g:Float", typeof(float) },
            { "g:Double", typeof(double) },
            { "g:Direction", typeof(Direction) },
            { "g:Merge", typeof(Merge) },
            { "g:UUID", typeof(Guid) },
            { "g:Date", typeof(DateTimeOffset) },
            { "g:Timestamp", typeof(DateTimeOffset) },
            { "g:T", typeof(T) },

            //Extended
            { "gx:BigDecimal", typeof(decimal) },
            { "gx:Duration", typeof(TimeSpan) },
            { "gx:BigInteger", typeof(BigInteger) },
            { "gx:Byte",typeof(byte) },
            { "gx:ByteBuffer", typeof(byte[]) },
            { "gx:Char", typeof(char) },
            { "gx:Int16", typeof(short) }
        };

        public static readonly IGremlinQueryFragmentDeserializer Identity = new GremlinQueryFragmentDeserializerImpl(ImmutableDictionary<Type, Delegate>.Empty);

        public static IGremlinQueryFragmentDeserializer AddToStringFallback(this IGremlinQueryFragmentDeserializer deserializer) => deserializer
            .Override<object>(static (data, type, env, overridden, recurse) => type == typeof(string)
                ? data.ToString()
                : overridden(data, type, env, recurse));

        // ReSharper disable ConvertToLambdaExpression
        public static IGremlinQueryFragmentDeserializer AddNewtonsoftJson(this IGremlinQueryFragmentDeserializer deserializer) => deserializer
            .Override<JToken>(static (jToken, type, env, overridden, recurse) =>
            {
                if (!type.IsAssignableFrom(jToken.GetType()))
                {
                    var envCache = env
                        .GetCache();

                    var populatingSerializer = envCache
                        .GetPopulatingJsonSerializer(recurse);

                    var ret = jToken.ToObject(type, populatingSerializer);

                    if (ret is not null && ret is not JToken && ret is not Property && jToken is JObject element)
                    {
                        if (element.TryGetElementProperties() is { } propertiesToken)
                        {
                            envCache
                                .GetIgnoringJsonSerializer(recurse)
                                .Populate(new JTokenReader(propertiesToken), ret);
                        }
                    }

                    return ret;
                }

                return overridden(jToken, type, env, recurse);
            })
            .Override<JToken>(static (jToken, type, env, overridden, recurse) =>
            {
                if (type.IsArray && !env.GetCache().FastNativeTypes.ContainsKey(type))
                {
                    type = type.GetElementType()!;

                    var array = Array.CreateInstance(type, 1);
                    array.SetValue(recurse.TryDeserialize(jToken, type, env), 0);

                    return array;
                }

                return overridden(jToken, type, env, recurse);
            })
            .Override<JToken>(static (jToken, type, env, overridden, recurse) =>
            {
                return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)
                    ? jToken.Type == JTokenType.Null
                        ? null
                        : recurse.TryDeserialize(jToken, type.GetGenericArguments()[0], env)
                    : overridden(jToken, type, env, recurse);
            })
            .Override<JValue>(static (jToken, type, env, overridden, recurse) =>
            {
                return typeof(Property).IsAssignableFrom(type) && type.IsGenericType
                    ? Activator.CreateInstance(type, recurse.TryDeserialize(jToken, type.GetGenericArguments()[0], env))
                    : overridden(jToken, type, env, recurse);
            })
            .Override<JValue, TimeSpan>(static (jValue, type, env, overridden, recurse) => jValue.Type == JTokenType.String
                ? XmlConvert.ToTimeSpan(jValue.Value<string>()!)
                : overridden(jValue, type, env, recurse))
            .Override<JValue, DateTimeOffset>(static (jValue, type, env, overridden, recurse) =>
            {
                switch (jValue.Value)
                {
                    case DateTime dateTime:
                        return new DateTimeOffset(dateTime);
                    case DateTimeOffset dateTimeOffset:
                        return dateTimeOffset;
                    default:
                    {
                        if (jValue.Type == JTokenType.Integer)
                            return DateTimeOffset.FromUnixTimeMilliseconds(jValue.Value<long>());

                        break;
                    }
                }

                return overridden(jValue, type, env, recurse);
            })
            .Override<JValue, DateTime>(static (jValue, type, env, overridden, recurse) =>
            {
                switch (jValue.Value)
                {
                    case DateTime dateTime:
                        return dateTime;
                    case DateTimeOffset dateTimeOffset:
                        return dateTimeOffset.UtcDateTime;
                }

                if (jValue.Type == JTokenType.Integer)
                    return new DateTime(DateTimeOffset.FromUnixTimeMilliseconds(jValue.Value<long>()).Ticks, DateTimeKind.Utc);

                return overridden(jValue, type, env, recurse);
            })
            .Override<JValue, byte[]>(static (jValue, type, env, overridden, recurse) =>
            {
                return jValue.Type == JTokenType.String
                    ? Convert.FromBase64String(jValue.Value<string>()!)
                    : overridden(jValue, type, env, recurse);
            })
            .Override<JValue>(static (jToken, type, env, overridden, recurse) =>
            {
                return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)
                    ? jToken.Value is null
                        ? null
                        : recurse.TryDeserialize(jToken, type.GetGenericArguments()[0], env)
                    : overridden(jToken, type, env, recurse);
            })
            .Override<JValue>(static (jValue, type, env, overridden, recurse) =>
            {
                if (jValue.Value is { } value)
                {
                    if (type.IsInstanceOfType(value))
                        return value;

                    if (type == typeof(int) || type == typeof(byte) || type == typeof(sbyte) || type == typeof(ushort) || type == typeof(short) || type == typeof(uint) || type == typeof(ulong) || type == typeof(long) || type == typeof(float) || type == typeof(double))
                        return Convert.ChangeType(value, type);

                    return overridden(jValue, type, env, recurse);
                }

                return null;
            })
            .Override<JObject, object>(static (jObject, _, env, _, recurse) =>
            {
                var expando = (IDictionary<string, object?>)new ExpandoObject();

                foreach (var property in jObject)
                {
                    expando.Add(property.Key, recurse.TryDeserialize(property.Value, typeof(object), env));
                }

                return expando;
            })
            .Override<JObject>(static (jObject, type, env, overridden, recurse) =>
            {
                if (!type.IsSealed)
                {
                    // Elements
                    var modelTypes = env.GetCache().ModelTypesForLabels;
                    var label = jObject["label"]?.ToString();

                    var modelType = label != null && modelTypes.TryGetValue(label, out var types)
                        ? types.FirstOrDefault(possibleType => type.IsAssignableFrom(possibleType))
                        : default;

                    if (modelType != null && modelType != type)
                        return recurse.TryDeserialize(jObject, modelType, env);
                }

                return overridden(jObject, type, env, recurse);
            })
            .Override<JObject>(static (jObject, type, env, overridden, recurse) =>
            {
                //Vertex Properties
                var nativeTypes = env.GetCache().FastNativeTypes;

                if (nativeTypes.ContainsKey(type) || (type.IsEnum && nativeTypes.ContainsKey(type.GetEnumUnderlyingType())))
                {
                    if (jObject.TryGetValue("value", out var valueToken))
                        return recurse.TryDeserialize(valueToken, type, env);
                }

                return overridden(jObject, type, env, recurse);
            })
            .Override<JObject>(static (jObject, type, env, overridden, recurse) =>
            {
                if (jObject.TryGetValue("@type", out var typeName) && jObject.TryGetValue("@value", out var valueToken))
                {
                    if (typeName.Type == JTokenType.String && typeName.Value<string>() is { } typeNameString && GraphSONTypes.TryGetValue(typeNameString, out var moreSpecificType))
                    {
                        if (type != moreSpecificType && type.IsAssignableFrom(moreSpecificType))
                            type = moreSpecificType;
                    }

                    return recurse.TryDeserialize(valueToken, type, env);
                }

                return overridden(jObject, type, env, recurse);
            })
            .Override<JObject>(static (jObject, type, env, overridden, recurse) =>
            {
                //@type == "g:Map"
                return jObject.TryUnmap() is { } unmappedObject
                    ? recurse.TryDeserialize(unmappedObject, type, env)
                    : overridden(jObject, type, env, recurse);
            })
            .Override<JObject>(static (jObject, type, env, overridden, recurse) =>
            {
                if (type.IsArray && !env.GetCache().FastNativeTypes.ContainsKey(type))
                {
                    var elementType = type.GetElementType()!;

                    if (jObject.TryGetValue("@type", out var typeToken) && "g:BulkSet".Equals(typeToken.Value<string>(), StringComparison.OrdinalIgnoreCase))
                    {
                        if (jObject.TryGetValue("@value", out var valueToken) && valueToken is JArray setArray)
                        {
                            var array = new ArrayList();

                            for (var i = 0; i < setArray.Count; i += 2)
                            {
                                var element = recurse.TryDeserialize(setArray[i], elementType, env);
                                var bulk = (int)recurse.TryDeserialize(setArray[i + 1], typeof(int), env)!;

                                for (var j = 0; j < bulk; j++)
                                {
                                    array.Add(element);
                                }
                            }

                            return array.ToArray(elementType);
                        }
                    }
                }

                return overridden(jObject, type, env, recurse);
            })
            .Override<JArray>(static (jArray, type, env, overridden, recurse) =>
            {
                if ((!type.IsArray || env.GetCache().FastNativeTypes.ContainsKey(type)) && !type.IsInstanceOfType(jArray))
                {
                    return jArray.Count != 1
                        ? jArray.Count == 0 && type.IsClass
                            ? default
                            : throw new JsonReaderException($"Cannot convert array\r\n\r\n{jArray}\r\n\r\nto scalar value of type {type}.")
                        : recurse.TryDeserialize(jArray[0], type, env);
                }

                return overridden(jArray, type, env, recurse);
            })
            .Override<JArray>(static (jArray, type, env, overridden, recurse) =>
            {
                return type.IsAssignableFrom(typeof(object[])) && recurse.TryDeserialize(jArray, typeof(object[]), env) is object[] tokens
                    ? tokens
                    : overridden(jArray, type, env, recurse);
            })
            .Override<JArray>(static (jArray, type, env, overridden, recurse) =>
            {
                //Traversers
                if (!type.IsArray || env.GetCache().FastNativeTypes.ContainsKey(type))
                    return overridden(jArray, type, env, recurse);

                var array = default(ArrayList);
                var elementType = type.GetElementType()!;

                for (var i = 0; i < jArray.Count; i++)
                {
                    var bulk = 1;
                    var effectiveArrayItem = jArray[i];

                    if (effectiveArrayItem is JObject traverserObject && traverserObject.TryGetValue("@type", out var nestedType) && "g:Traverser".Equals(nestedType.Value<string>(), StringComparison.OrdinalIgnoreCase) && traverserObject.TryGetValue("@value", out var valueToken) && valueToken is JObject nestedTraverserObject)
                    {
                        if (nestedTraverserObject.TryGetValue("bulk", out var bulkToken) && recurse.TryDeserialize(bulkToken, typeof(int), env) is int bulkObject)
                            bulk = bulkObject;

                        if (nestedTraverserObject.TryGetValue("value", out var traverserValue))
                            effectiveArrayItem = traverserValue;
                    }

                    if (recurse.TryDeserialize(effectiveArrayItem, elementType, env) is { } item)
                    {
                        if (jArray.Count == 1 && bulk == 1)
                        {
                            var ret = Array.CreateInstance(elementType, 1);
                            ret.SetValue(item, 0);

                            return ret;
                        }

                        array ??= new ArrayList(jArray.Count);

                        for (var j = 0; j < bulk; j++)
                        {
                            array.Add(item);
                        }
                    }
                }

                return array?.ToArray(elementType) ?? Array.CreateInstance(elementType, 0);
            });
        // ReSharper restore ConvertToLambdaExpression

        public static IGremlinQueryFragmentDeserializer Override<TSerialized, TNative>(this IGremlinQueryFragmentDeserializer fragmentDeserializer, GremlinQueryFragmentDeserializerDelegate<TSerialized> deserializerDelegate)
        {
            return fragmentDeserializer
                .Override<TSerialized>((token, type, env, overridden, recurse) => type == typeof(TNative)
                    ? deserializerDelegate(token, type, env, overridden, recurse)
                    : overridden(token, type, env, recurse));
        }

        internal static IGremlinQueryFragmentDeserializer ToGraphsonString(this IGremlinQueryFragmentDeserializer deserializer)
        {
            return deserializer
                .Override<object>(static (data, type, env, overridden, recurse) => type.IsAssignableFrom(typeof(string))
                    ? new GraphSON2Writer().WriteObject(data)
                    : overridden(data, type, env, recurse));
        }
    }
}
