﻿using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace ExRam.Gremlinq
{
    public sealed class AddElementPropertiesStep : NonTerminalStep
    {
        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> TypeProperties = new ConcurrentDictionary<Type, PropertyInfo[]>();

        public AddElementPropertiesStep(object element)
        {
            Element = element;
        }

        public override IEnumerable<TerminalStep> Resolve(IGraphModel model)
        {
            return TypeProperties.GetOrAdd(
                Element.GetType(),
                type => type             
                    .GetProperties()
                    .Where(property => IsMetaType(property.PropertyType) ||  IsNativeType(property.PropertyType))
                    .ToArray())
                .Select(property =>
                {
                    var value = property.GetValue(Element);

                    return value != null
                        ? new PropertyStep(property, value)
                        : null;
                })
                .Where(step => step != null)
                .SelectMany(_ => _.Resolve(model));
        }

        private static bool IsNativeType(Type type)   //TODO: Native types are a matter of...what?
        {
            return type.GetTypeInfo().IsValueType || type == typeof(string) || type == typeof(object) || type.IsArray && IsNativeType(type.GetElementType());
        }

        private static bool IsMetaType(Type type)
        {
            return typeof(IMeta).IsAssignableFrom(type);
        }

        public object Element { get; }
    }
}
