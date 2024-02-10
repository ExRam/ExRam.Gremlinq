﻿using System.Collections;
using System.Linq.Expressions;

namespace ExRam.Gremlinq.Core.ExpressionParsing
{
    internal readonly struct ExpressionFragment
    {
        private readonly object? _value;

        public static readonly ExpressionFragment True = new(true, Expression.Constant(true));
        public static readonly ExpressionFragment False = new(false, Expression.Constant(false));
        public static readonly ExpressionFragment Null = new(default, Expression.Constant(null, typeof(object)));

        private ExpressionFragment(object? value, Expression? expression = default)
        {
            _value = value;
            Expression = expression;
        }

        public bool Equals(ExpressionFragment other) => Equals(_value, other._value) && Equals(Expression, other.Expression);

        public override bool Equals(object? obj) => obj is ExpressionFragment other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _value != null ? _value.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Expression != null ? Expression.GetHashCode() : 0);
                return hashCode;
            }
        }

        public Expression? Expression { get; }

        public static ExpressionFragment Create(Expression expression, IGremlinQueryEnvironment environment)
        {
            return new(default, expression.StripConvert());
        }
    }
}
