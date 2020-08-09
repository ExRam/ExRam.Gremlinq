﻿using System;
using System.Collections.Generic;
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
            private readonly ConditionalWeakTable<IGremlinQueryFragmentDeserializer, JsonSerializer> PopulatingSerializers = new ConditionalWeakTable<IGremlinQueryFragmentDeserializer, JsonSerializer>();
            private readonly ConditionalWeakTable<IGremlinQueryFragmentDeserializer, JsonSerializer> IgnoringSerializers = new ConditionalWeakTable<IGremlinQueryFragmentDeserializer, JsonSerializer>();

            public EnvironmentCacheImpl(IGremlinQueryEnvironment environment)
            {
                _environment = environment;
            }

            public JsonSerializer GetPopulatingJsonSerializer(IGremlinQueryFragmentDeserializer fragmentDeserializer)
            {
                return PopulatingSerializers
                    .GetValue(
                        fragmentDeserializer,
                        closureRecurse => new GraphsonJsonSerializer(
                            DefaultValueHandling.Populate,
                            _environment,
                            fragmentDeserializer));
            }

            public JsonSerializer GetIgnoringJsonSerializer(IGremlinQueryFragmentDeserializer fragmentDeserializer)
            {
                return IgnoringSerializers
                    .GetValue(
                        fragmentDeserializer,
                        closureRecurse => new GraphsonJsonSerializer(
                            DefaultValueHandling.Ignore,
                            _environment,
                            fragmentDeserializer));
            }
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
