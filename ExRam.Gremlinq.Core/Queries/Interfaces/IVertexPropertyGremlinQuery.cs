﻿using System;
using System.Linq.Expressions;
using ExRam.Gremlinq.Core.GraphElements;

namespace ExRam.Gremlinq.Core
{
    public partial interface IVertexPropertyGremlinQuery<TProperty, TValue> : IElementGremlinQuery<TProperty>
    {
        IVertexPropertyGremlinQuery<VertexProperty<TValue, TMeta>, TValue, TMeta> Meta<TMeta>() where TMeta : class;

        IPropertyGremlinQuery<Property<TMetaValue>> Properties<TMetaValue>(params string[] keys);
        IPropertyGremlinQuery<Property<object>> Properties(params string[] keys);
        
        IValueGremlinQuery<TValue> Value();
    }

    public partial interface IVertexPropertyGremlinQuery<TProperty, TValue, TMeta> : IElementGremlinQuery<TProperty> where TMeta : class
    {
        IPropertyGremlinQuery<Property<TTarget>> Properties<TTarget>(params Expression<Func<TMeta, TTarget>>[] projections);
        IPropertyGremlinQuery<Property<object>> Properties(params string[] keys);

        IVertexPropertyGremlinQuery<TProperty, TValue, TMeta> Property<TMetaValue>(Expression<Func<TMeta, TMetaValue>> projection, TMetaValue value);

        IValueGremlinQuery<TValue> Value();
        IValueGremlinQuery<TMetaValue> Values<TMetaValue>(params Expression<Func<TMeta, TMetaValue>>[] projections);
        IGremlinQuery<TMeta> ValueMap();

        IVertexPropertyGremlinQuery<TProperty, TValue, TMeta> Where(Expression<Func<VertexProperty<TValue, TMeta>, bool>> predicate);
    }
}
