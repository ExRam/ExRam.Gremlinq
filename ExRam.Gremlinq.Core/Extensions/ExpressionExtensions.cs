﻿using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ExRam.Gremlinq.Core.GraphElements;

namespace ExRam.Gremlinq.Core
{
    internal static class ExpressionExtensions
    {
        // ReSharper disable ReturnValueOfPureMethodIsNotUsed
        private static readonly MethodInfo EnumerableAny = Get(() => Enumerable.Any<object>(default))?.GetGenericMethodDefinition()!;
        private static readonly MethodInfo EnumerableIntersect = Get(() => Enumerable.Intersect<object>(default, default))?.GetGenericMethodDefinition()!;
#pragma warning disable 8625
        private static readonly MethodInfo EnumerableContainsElement = Get(() => Enumerable.Contains<object>(default, default))?.GetGenericMethodDefinition()!;
#pragma warning restore 8625
        // ReSharper disable once RedundantTypeSpecificationInDefaultExpression
        private static readonly MethodInfo StringStartsWith = Get(() => string.Empty.StartsWith(string.Empty));
        private static readonly MethodInfo StringContains = Get(() => string.Empty.Contains(string.Empty));
        private static readonly MethodInfo StringEndsWith = Get(() => string.Empty.EndsWith(string.Empty));
        private static readonly MethodInfo StringCompareTo = Get(() => string.Empty.CompareTo(string.Empty));
        // ReSharper restore ReturnValueOfPureMethodIsNotUsed

        public static Expression Strip(this Expression expression)
        {
            while (true)
            {
                switch (expression)
                {
                    case UnaryExpression unaryExpression when expression.NodeType == ExpressionType.Convert:
                    {
                        expression = unaryExpression.Operand;
                        break;
                    }
                    case MemberExpression memberExpression when memberExpression.TryGetWellKnownMember() == WellKnownMember.StepLabelValue:
                    {
                        return memberExpression.Expression;
                    }
                    default:
                    {
                        return expression;
                    }
                }
            }
        }

        public static object GetValue(this Expression expression, IGraphModel model)
        {
            var value = expression switch
            {
                ConstantExpression constantExpression => constantExpression.Value,
                MemberExpression memberExpression when memberExpression.Member is FieldInfo fieldInfo && memberExpression.Expression is ConstantExpression constant => fieldInfo.GetValue(constant.Value),
                LambdaExpression lambdaExpression => lambdaExpression.Compile().DynamicInvoke(),
                _ => Expression.Lambda<Func<object>>(expression.Type.IsClass ? expression : Expression.Convert(expression, typeof(object))).Compile()()
            };

            if (value is IEnumerable enumerable && !(value is ICollection) && !model.NativeTypes.Contains(enumerable.GetType()))
                value = enumerable.Cast<object>().ToArray();

            return value;
        }

        public static bool TryParseStepLabelExpression(this Expression expression, IGraphModel model, out StepLabel? stepLabel, out MemberExpression? stepLabelValueMemberExpression)
        {
            stepLabel = null;
            stepLabelValueMemberExpression = null;

            if (typeof(StepLabel).IsAssignableFrom(expression.Type))
            {
                stepLabel = (StepLabel)expression.GetValue(model);

                return true;
            }

            if (expression is MemberExpression outerMemberExpression)
            {
                if (outerMemberExpression.TryGetWellKnownMember() == WellKnownMember.StepLabelValue)
                {
                    stepLabel = (StepLabel)outerMemberExpression.Expression.GetValue(model);

                    return true;
                }

                stepLabelValueMemberExpression = outerMemberExpression;

                if (outerMemberExpression.Expression is MemberExpression innerMemberExpression)
                {
                    if (innerMemberExpression.TryGetWellKnownMember() == WellKnownMember.StepLabelValue)
                    {
                        stepLabel = (StepLabel)innerMemberExpression.Expression.GetValue(model);

                        return true;
                    }
                }
            }

            return false;
        }

        public static bool RefersToParameter(this Expression expression)
        {
            while (true)
            {
                expression = expression.Strip();

                switch (expression)
                {
                    case ParameterExpression _:
                    {
                        return true;
                    }
                    case LambdaExpression lambdaExpression:
                    {
                        expression = lambdaExpression.Body;
                        break;
                    }
                    case MemberExpression memberExpression:
                    {
                        expression = memberExpression.Expression;
                        break;
                    }
                    case MethodCallExpression methodCallExpression:
                    {
                        expression = methodCallExpression.Object;
                        break;
                    }
                    default:
                    {
                        return false;
                    }
                }
            }
        }

        public static bool IsIdentityExpression(this LambdaExpression expression)
        {
            return expression.Parameters.Count == 1 && expression.Body.Strip() == expression.Parameters[0];
        }

        public static GremlinExpression? TryToGremlinExpression(this Expression body, IGraphModel model)
        {
            var maybeExpression = body.TryToGremlinExpressionImpl(model);

            if (maybeExpression is { } expression)
            {
                if (expression.Left.Expression is MethodCallExpression leftMethodCallExpression)
                {
                    var wellKnownMember = leftMethodCallExpression.TryGetWellKnownMember();

                    if (wellKnownMember == WellKnownMember.StringCompareTo && expression.Right.GetValue(model) is int comparison)
                    {
                        var semantics = expression.Semantics;
                        comparison = Math.Min(2, Math.Max(-2, comparison));

                        semantics = semantics switch
                        {
                            ExpressionSemantics.LowerThan => comparison switch
                            {
                                -2 => ExpressionSemantics.False,
                                -1 => ExpressionSemantics.False,
                                 0 => ExpressionSemantics.LowerThan,
                                 1 => ExpressionSemantics.LowerThanOrEqual,
                                 2 => ExpressionSemantics.True,
                                _ => throw new ArgumentOutOfRangeException()
                            },
                            ExpressionSemantics.LowerThanOrEqual => comparison switch
                            {
                                -2 => ExpressionSemantics.False,
                                -1 => ExpressionSemantics.LowerThan,
                                 0 => ExpressionSemantics.LowerThanOrEqual,
                                 1 => ExpressionSemantics.True,
                                 2 => ExpressionSemantics.True,
                                _ => throw new ArgumentOutOfRangeException()
                            },
                            ExpressionSemantics.Equals => comparison switch
                            {
                                -2 => ExpressionSemantics.False,
                                -1 => ExpressionSemantics.LowerThan,
                                 0 => ExpressionSemantics.Equals,
                                 1 => ExpressionSemantics.GreaterThan,
                                 2 => ExpressionSemantics.False,
                                _ => throw new ArgumentOutOfRangeException()
                            },
                            ExpressionSemantics.GreaterThanOrEqual => comparison switch
                            {
                                -2 => ExpressionSemantics.True,
                                -1 => ExpressionSemantics.True,
                                0 => ExpressionSemantics.GreaterThanOrEqual,
                                1 => ExpressionSemantics.GreaterThan,
                                2 => ExpressionSemantics.False,
                                _ => throw new ArgumentOutOfRangeException()
                            },
                            ExpressionSemantics.GreaterThan => comparison switch
                            {
                                -2 => ExpressionSemantics.True,
                                -1 => ExpressionSemantics.GreaterThanOrEqual,
                                0 => ExpressionSemantics.GreaterThan,
                                1 => ExpressionSemantics.False,
                                2 => ExpressionSemantics.False,
                                _ => throw new ArgumentOutOfRangeException()
                            },
                            _ => throw new ArgumentOutOfRangeException()
                        };

                        if (semantics == ExpressionSemantics.True)
                            return GremlinExpression.True;

                        if (semantics == ExpressionSemantics.False)
                            return GremlinExpression.False;

                        return new GremlinExpression(
                            ExpressionFragment.Create(leftMethodCallExpression.Object, model),
                            semantics,
                            ExpressionFragment.Create(leftMethodCallExpression.Arguments[0], model));
                    }
                }
            }

            return maybeExpression;
        }

        private static GremlinExpression? TryToGremlinExpressionImpl(this Expression body, IGraphModel model)
        {
            switch (body)
            {
                case MemberExpression memberExpression when memberExpression.RefersToParameter() && memberExpression.Member is PropertyInfo property && property.PropertyType == typeof(bool):
                {
                    return new GremlinExpression(
                        ExpressionFragment.Create(memberExpression, model),
                        ExpressionSemantics.Equals,
                        ExpressionFragment.True);
                }
                case BinaryExpression binaryExpression when binaryExpression.NodeType != ExpressionType.AndAlso && binaryExpression.NodeType != ExpressionType.OrElse:
                {
                    return new GremlinExpression(
                        ExpressionFragment.Create(binaryExpression.Left.Strip(), model),
                        binaryExpression.NodeType.ToSemantics(),
                        ExpressionFragment.Create(binaryExpression.Right.Strip(), model));
                }
                case MethodCallExpression methodCallExpression:
                {
                    var wellKnownMember = methodCallExpression.TryGetWellKnownMember();
                    var thisExpression = methodCallExpression.Arguments[0].Strip();

                    switch (wellKnownMember)
                    {
                        case WellKnownMember.EnumerableIntersectAny:
                        {
                            var arguments = ((MethodCallExpression)thisExpression).Arguments;

                            return new GremlinExpression(
                                ExpressionFragment.Create(arguments[0].Strip(), model),
                                ExpressionSemantics.Intersects,
                                ExpressionFragment.Create(arguments[1].Strip(), model));
                        }
                        case WellKnownMember.EnumerableAny:
                        {
                            return new GremlinExpression(
                                ExpressionFragment.Create(thisExpression, model),
                                ExpressionSemantics.NotEquals,
                                ExpressionFragment.Null);
                        }
                        case WellKnownMember.EnumerableContains:
                        {
                            return new GremlinExpression(
                                ExpressionFragment.Create(thisExpression, model),
                                ExpressionSemantics.Contains,
                                ExpressionFragment.Create(methodCallExpression.Arguments[1].Strip(), model));
                        }
                        case WellKnownMember.StringStartsWith:
                        case WellKnownMember.StringEndsWith:
                        case WellKnownMember.StringContains:
                        {
                            var instanceExpression = methodCallExpression.Object.Strip();
                            var argumentExpression = methodCallExpression.Arguments[0].Strip();

                            if (wellKnownMember == WellKnownMember.StringStartsWith && argumentExpression is MemberExpression)
                            {
                                if (instanceExpression.GetValue(model) is string stringValue)
                                {
                                    return new GremlinExpression(
                                        new ConstantExpressionFragment(stringValue),
                                        ExpressionSemantics.StartsWith,
                                        ExpressionFragment.Create(argumentExpression, model));
                                }
                            }
                            else if (instanceExpression is MemberExpression)
                            {
                                if (argumentExpression.GetValue(model) is string stringValue)
                                {
                                    return new GremlinExpression(
                                        ExpressionFragment.Create(instanceExpression, model),
                                        wellKnownMember switch
                                        {
                                            WellKnownMember.StringStartsWith => ExpressionSemantics.StartsWith,
                                            WellKnownMember.StringContains => ExpressionSemantics.HasInfix,
                                            WellKnownMember.StringEndsWith => ExpressionSemantics.EndsWith,
                                            _ => throw new ExpressionNotSupportedException(methodCallExpression)
                                        },
                                        new ConstantExpressionFragment(stringValue));
                                }
                            }

                            break;
                        }
                    }

                    break;
                }
            }

            return default;
        }

        public static WellKnownMember? TryGetWellKnownMember(this Expression expression)
        {
            switch (expression)
            {
                case MemberExpression memberExpression:
                {
                    var member = memberExpression.Member;

                    if (typeof(Property).IsAssignableFrom(member.DeclaringType) && member.Name == nameof(Property<object>.Value))
                        return WellKnownMember.PropertyValue;

                    if (typeof(Property).IsAssignableFrom(member.DeclaringType) && member.Name == nameof(Property<object>.Key))
                        return WellKnownMember.PropertyKey;

                    if (typeof(StepLabel).IsAssignableFrom(member.DeclaringType) && member.Name == nameof(StepLabel<object>.Value))
                        return WellKnownMember.StepLabelValue;

                    if (typeof(IVertexProperty).IsAssignableFrom(member.DeclaringType) && member.Name == nameof(VertexProperty<object>.Label))
                        return WellKnownMember.VertexPropertyLabel;
                    break;
                }
                case MethodCallExpression methodCallExpression:
                {
                    var methodInfo = methodCallExpression.Method;

                    if (methodInfo.IsStatic)
                    {
                        var thisExpression = methodCallExpression.Arguments[0].Strip();

                        if (methodInfo.IsGenericMethod && methodInfo.GetGenericMethodDefinition() == EnumerableAny)
                        {
                            return thisExpression is MethodCallExpression previousMethodCallExpression && previousMethodCallExpression.Method.IsGenericMethod && previousMethodCallExpression.Method.GetGenericMethodDefinition() == EnumerableIntersect
                                ? WellKnownMember.EnumerableIntersectAny
                                : WellKnownMember.EnumerableAny;
                        }

                        if (methodInfo.IsGenericMethod && methodInfo.GetGenericMethodDefinition() == EnumerableContainsElement)
                            return WellKnownMember.EnumerableContains;
                    }
                    else if (methodInfo == StringStartsWith)
                        return WellKnownMember.StringStartsWith;
                    else if (methodInfo == StringEndsWith)
                        return WellKnownMember.StringEndsWith;
                    else if (methodInfo == StringContains)
                        return WellKnownMember.StringContains;
                    else if (methodInfo == StringCompareTo)
                        return WellKnownMember.StringCompareTo;

                    break;
                }
            }

            return null;
        }

        public static MemberInfo GetMemberInfo(this LambdaExpression expression)
        {
            return expression.Body.Strip() is MemberExpression memberExpression
                ? memberExpression.Member
                : throw new ExpressionNotSupportedException(expression);
        }

        private static MethodInfo Get(Expression<Action> expression)
        {
            return ((MethodCallExpression)expression.Body).Method;
        }
    }
}
