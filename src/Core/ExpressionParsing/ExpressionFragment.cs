﻿using System.Collections;
using System.Linq.Expressions;

namespace ExRam.Gremlinq.Core.ExpressionParsing
{
    internal readonly struct ExpressionFragment
    {
        public static readonly ExpressionFragment True = new(Expression.Constant(true));
        public static readonly ExpressionFragment False = new(Expression.Constant(false));
        public static readonly ExpressionFragment Null = new(Expression.Constant(null, typeof(object)));

        private ExpressionFragment(Expression? expression = default)
        {
            Expression = expression;
        }

        public bool Equals(ExpressionFragment other) => Equals(Expression, other.Expression);

        public override bool Equals(object? obj) => obj is ExpressionFragment other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 0;
                hashCode = (hashCode * 397) ^ (Expression != null ? Expression.GetHashCode() : 0);
                return hashCode;
            }
        }

        public Expression? Expression { get; }

        public static ExpressionFragment Create(Expression expression)
        {
            return new(expression.StripConvert());
        }
    }
}
