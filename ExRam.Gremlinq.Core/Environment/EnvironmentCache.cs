﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

using ExRam.Gremlinq.Core.GraphElements;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace ExRam.Gremlinq.Core
{
    internal static class EnvironmentCache
    {
        private sealed class EnvironmentCacheImpl : IEnvironmentCache
        {
            private sealed class GraphsonJsonSerializer : JsonSerializer
            {
                #region Nested
                private sealed class GremlinContractResolver : DefaultContractResolver
                {
                    private sealed class EmptyListValueProvider : IValueProvider
                    {
                        private readonly object _defaultValue;
                        private readonly IValueProvider _innerProvider;

                        public EmptyListValueProvider(IValueProvider innerProvider, Type elementType)
                        {
                            _innerProvider = innerProvider;
                            _defaultValue = Array.CreateInstance(elementType, 0);
                        }

                        public void SetValue(object target, object? value)
                        {
                            _innerProvider.SetValue(target, value ?? _defaultValue);
                        }

                        public object GetValue(object target)
                        {
                            return _innerProvider.GetValue(target) ?? _defaultValue;
                        }
                    }

                    private sealed class EmptyDictionaryValueProvider : IValueProvider
                    {
                        private readonly object _defaultValue;
                        private readonly IValueProvider _innerProvider;

                        public EmptyDictionaryValueProvider(IValueProvider innerProvider)
                        {
                            _innerProvider = innerProvider;
                            _defaultValue = new Dictionary<string, object>();
                        }

                        public void SetValue(object target, object? value)
                        {
                            _innerProvider.SetValue(target, value ?? _defaultValue);
                        }

                        public object GetValue(object target)
                        {
                            return _innerProvider.GetValue(target) ?? _defaultValue;
                        }
                    }

                    private readonly IGraphElementPropertyModel _model;

                    public GremlinContractResolver(IGraphElementPropertyModel model)
                    {
                        _model = model;
                    }

                    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
                    {
                        var property = base.CreateProperty(member, memberSerialization);

                        if (_model.MemberMetadata.TryGetValue(member, out var key) && key.Key.RawKey is string name)
                            property.PropertyName = name;

                        return property;
                    }

                    protected override IValueProvider CreateMemberValueProvider(MemberInfo member)
                    {
                        var provider = base.CreateMemberValueProvider(member);

                        if (member is PropertyInfo propertyMember)
                        {
                            var propertyType = propertyMember.PropertyType;

                            if (propertyType == typeof(IDictionary<string, object>) && propertyMember.Name == nameof(VertexProperty<object>.Properties) && typeof(IVertexProperty).IsAssignableFrom(propertyMember.DeclaringType))
                                return new EmptyDictionaryValueProvider(provider);

                            if (propertyType.IsArray)
                                return new EmptyListValueProvider(provider, propertyType.GetElementType());
                        }

                        return provider;
                    }
                }

                private sealed class JTokenConverterConverter : JsonConverter
                {
                    private readonly IGremlinQueryEnvironment _environment;
                    private readonly IGremlinQueryFragmentDeserializer _deserializer;

                    [ThreadStatic]
                    // ReSharper disable once StaticMemberInGenericType
                    private static bool _canConvert;

                    public JTokenConverterConverter(
                        IGremlinQueryFragmentDeserializer deserializer,
                        IGremlinQueryEnvironment environment)
                    {
                        _deserializer = deserializer;
                        _environment = environment;
                    }

                    public override bool CanConvert(Type objectType)
                    {
                        if (!_canConvert)
                        {
                            _canConvert = true;

                            return false;
                        }

                        return true;
                    }

                    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
                    {
                        throw new NotSupportedException($"Cannot write to {nameof(JTokenConverterConverter)}.");
                    }

                    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
                    {
                        var token = JToken.Load(reader);

                        try
                        {
                            _canConvert = false;

                            return _deserializer.TryDeserialize(token, objectType, _environment);
                        }
                        finally
                        {
                            _canConvert = true;
                        }
                    }
                }
                #endregion

                public GraphsonJsonSerializer(
                    DefaultValueHandling defaultValueHandling,
                    IGremlinQueryEnvironment environment,
                    IGremlinQueryFragmentDeserializer fragmentDeserializer)
                {
                    DefaultValueHandling = defaultValueHandling;
                    ContractResolver = new GremlinContractResolver(environment.Model.PropertiesModel);
                    Converters.Add(new JTokenConverterConverter(fragmentDeserializer, environment));
                }
            }

            private readonly IGremlinQueryEnvironment _environment;
            private readonly ConditionalWeakTable<IGremlinQueryFragmentDeserializer, JsonSerializer> _populatingSerializers = new ConditionalWeakTable<IGremlinQueryFragmentDeserializer, JsonSerializer>();
            private readonly ConditionalWeakTable<IGremlinQueryFragmentDeserializer, JsonSerializer> _ignoringSerializers = new ConditionalWeakTable<IGremlinQueryFragmentDeserializer, JsonSerializer>();
            private readonly ConcurrentDictionary<Type, (PropertyInfo propertyInfo, Key key, SerializationBehaviour serializationBehaviour)[]> _typeProperties = new ConcurrentDictionary<Type, (PropertyInfo, Key, SerializationBehaviour)[]>();

            private static readonly ConcurrentDictionary<Type, string> EdgeLabels = new ConcurrentDictionary<Type, string>();
            private static readonly ConcurrentDictionary<Type, string> VertexLabels = new ConcurrentDictionary<Type, string>();

            public EnvironmentCacheImpl(IGremlinQueryEnvironment environment)
            {
                _environment = environment;

                ModelTypes = environment.Model
                    .VerticesModel
                    .Metadata
                    .Concat(environment.Model.EdgesModel.Metadata)
                    .GroupBy(x => x.Value.Label)
                    .ToDictionary(
                        group => group.Key,
                        group => group
                            .Select(x => x.Key)
                            .ToArray(),
                        StringComparer.OrdinalIgnoreCase);
            }

            public JsonSerializer GetPopulatingJsonSerializer(IGremlinQueryFragmentDeserializer fragmentDeserializer)
            {
                return _populatingSerializers
                    .GetValue(
                        fragmentDeserializer,
                        closureRecurse => new GraphsonJsonSerializer(
                            DefaultValueHandling.Populate,
                            _environment,
                            fragmentDeserializer));
            }

            public JsonSerializer GetIgnoringJsonSerializer(IGremlinQueryFragmentDeserializer fragmentDeserializer)
            {
                return _ignoringSerializers
                    .GetValue(
                        fragmentDeserializer,
                        closureRecurse => new GraphsonJsonSerializer(
                            DefaultValueHandling.Ignore,
                            _environment,
                            fragmentDeserializer));
            }

            public string GetVertexLabel(Type type) => GetLabel(VertexLabels, _environment.Model.VerticesModel, type);

            public string GetEdgeLabel(Type type) => GetLabel(EdgeLabels, _environment.Model.EdgesModel, type);

            public (PropertyInfo propertyInfo, Key key, SerializationBehaviour serializationBehaviour)[] GetSerializationData(Type type)
            {
                return _typeProperties
                    .GetOrAdd(
                        type,
                        (closureType, closureModel) => closureType
                            .GetTypeHierarchy()
                            .SelectMany(typeInHierarchy => typeInHierarchy
                                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly))
                            .Where(p => p.GetMethod.GetBaseDefinition() == p.GetMethod)
                            .Select(p =>
                            {
                                return (
                                    property: p,
                                    key: closureModel.GetKey(p),
                                    serializationBehaviour: closureModel.MemberMetadata
                                        .GetValueOrDefault(p, new MemberMetadata(p.Name)).SerializationBehaviour);
                            })
                            .OrderBy(x => x.property.Name)
                            .ToArray(),
                        _environment.Model.PropertiesModel);
            }
            
            private string GetLabel(ConcurrentDictionary<Type, string> dict, IGraphElementModel elementModel, Type type)
            {
                return dict
                    .GetOrAdd(
                        type,
                        (closureType, closureModel) => closureType
                            .GetTypeHierarchy()
                            .Where(type => !type.IsAbstract)
                            .Select(type => closureModel.Metadata.TryGetValue(type, out var metadata)
                                ? metadata.Label
                                : null)
                            .FirstOrDefault() ?? closureType.Name,
                        elementModel);
            }

            public IReadOnlyDictionary<string, Type[]> ModelTypes { get; }
        }

        private static readonly ConditionalWeakTable<IGremlinQueryEnvironment, IEnvironmentCache> Caches = new ConditionalWeakTable<IGremlinQueryEnvironment, IEnvironmentCache>();

        public static IEnvironmentCache GetCache(this IGremlinQueryEnvironment environment)
        {
            return Caches.GetValue(
                environment,
                closure => new EnvironmentCacheImpl(environment));
        }
    }
}
