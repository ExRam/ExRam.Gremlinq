﻿using System.Collections;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

using ExRam.Gremlinq.Core.ExpressionParsing;
using ExRam.Gremlinq.Core.GraphElements;

namespace ExRam.Gremlinq.Core
{
    internal static class ExpressionExtensions
    {
        // ReSharper disable ReturnValueOfPureMethodIsNotUsed
        private static readonly MethodInfo ObjectToString = Get<object>(static _ => _.ToString());
        private static readonly MethodInfo EnumerableAny = Get(static () => Enumerable.Any<object>(default!)).GetGenericMethodDefinition();
        private static readonly MethodInfo EnumerableIntersect = Get(static () => Enumerable.Intersect<object>(default!, default!)).GetGenericMethodDefinition();
#pragma warning disable 8625
        private static readonly MethodInfo EnumerableContainsElement = Get(static () => Enumerable.Contains<object>(default!, default)).GetGenericMethodDefinition();
#pragma warning restore 8625

        public static Expression StripConvert(this Expression expression)
        {
            while (true)
            {
                switch (expression)
                {
                    case MemberExpression { Member: PropertyInfo { Name: "Value" }, Expression: { } memberExpressionExpression } when Nullable.GetUnderlyingType(memberExpressionExpression.Type) is not null:
                    {
                        expression = memberExpressionExpression;
                        break;
                    }
                    case UnaryExpression unaryExpression when expression.NodeType == ExpressionType.Convert:
                    {
                        expression = unaryExpression.Operand;
                        break;
                    }
                    case MethodCallExpression { Object: { } objectExpression } methodCallExpression when methodCallExpression.Method == ObjectToString:
                    {
                        expression = objectExpression;
                        break;
                    }
                    default:
                    {
                        return expression;
                    }
                }
            }
        }

        public static MemberExpression AssumeMemberExpression(this Expression expression)
        {
            return expression.StripConvert() switch
            {
                LambdaExpression lambdaExpression => lambdaExpression.Body.AssumeMemberExpression(),
                MemberExpression memberExpression => memberExpression,
                _ => throw new ExpressionNotSupportedException(expression)
            };
        }

        public static MemberExpression AssumePropertyOrFieldMemberExpression(this Expression expression)
        {
            return expression.AssumeMemberExpression() is { Member: { } member } memberExpression && (member is FieldInfo || member is PropertyInfo)
                ? memberExpression
                : throw new ExpressionNotSupportedException(expression);
        }

        public static object? GetValue(this Expression expression)
        {
            return expression switch
            {
                ConstantExpression constantExpression => constantExpression.Value,
                MethodCallExpression methodCallExpression => methodCallExpression.Method.Invoke(
                    methodCallExpression.Object?.GetValue(),
                    methodCallExpression.GetArguments()),
                MemberExpression { Member: PropertyInfo propertyInfo } propertyExpression => propertyInfo.GetValue(propertyExpression.Expression?.GetValue()),
                MemberExpression { Member: FieldInfo fieldInfo } fieldExpression => fieldInfo.GetValue(fieldExpression.Expression?.GetValue()),
                NewExpression { Constructor: { } constructor, Members: null } newExpression => constructor.Invoke(newExpression.GetArguments()),
                NewArrayExpression newArrayExpression => newArrayExpression.GetValue(),
                _ => Expression.Lambda<Func<object>>(expression.Type.IsClass ? expression : Expression.Convert(expression, typeof(object))).Compile()()
            };
        }

        public static WellKnownMember? TryGetWellKnownMember(this MemberExpression expression)
        {
            var member = expression.Member;

            if (typeof(Property).IsAssignableFrom(member.DeclaringType) && member.Name == nameof(Property<object>.Value))
                return WellKnownMember.PropertyValue;

            if (typeof(Property).IsAssignableFrom(member.DeclaringType) && member.Name == nameof(Property<object>.Key))
                return WellKnownMember.PropertyKey;

            if (typeof(StepLabel).IsAssignableFrom(member.DeclaringType) && member.Name == nameof(StepLabel<object>.Value))
                return WellKnownMember.StepLabelValue;

            if (typeof(IVertexProperty).IsAssignableFrom(member.DeclaringType) && member.Name == nameof(VertexProperty<object>.Label))
                return WellKnownMember.VertexPropertyLabel;

            return null;
        }

        public static WellKnownOperation? TryGetWellKnownOperation(this MethodCallExpression expression)
        {
            var methodInfo = expression.Method;

            if (methodInfo.IsStatic)
            {
                var thisExpression = expression.Arguments[0].StripConvert();

                if (methodInfo.IsGenericMethod && methodInfo.GetGenericMethodDefinition() == EnumerableAny)
                {
                    return thisExpression is MethodCallExpression { Method.IsGenericMethod: true } previousMethodCallExpression && previousMethodCallExpression.Method.GetGenericMethodDefinition() == EnumerableIntersect
                        ? WellKnownOperation.EnumerableIntersectAny
                        : WellKnownOperation.EnumerableAny;
                }

                if (methodInfo.IsGenericMethod && methodInfo.GetGenericMethodDefinition() == EnumerableContainsElement)
                    return WellKnownOperation.EnumerableContains;
            }
            else
            {
                if (typeof(IList).IsAssignableFrom(methodInfo.DeclaringType) && methodInfo.Name == nameof(List<object>.Contains))
                    return WellKnownOperation.ListContains;

                if (methodInfo.DeclaringType is { IsGenericType: true } declaringType && declaringType.GetGenericArguments() is [_, _] && methodInfo.Name == "get_Item")
                    return WellKnownOperation.IndexerGet;

                if (methodInfo.DeclaringType == typeof(string) && methodInfo.GetParameters() is { Length: 1 or 2 } parameters)
                {
                    if (parameters[0].ParameterType == typeof(string) && (parameters.Length == 1 || parameters[1].ParameterType == typeof(StringComparison)))
                    {
                        switch (methodInfo.Name)
                        {
                            case nameof(object.Equals):
                                return WellKnownOperation.StringEquals;
                            case nameof(string.StartsWith):
                                return WellKnownOperation.StringStartsWith;
                            case nameof(string.EndsWith):
                                return WellKnownOperation.StringEndsWith;
                            case nameof(string.Contains):
                                return WellKnownOperation.StringContains;
                        }
                    }
                }

                if (methodInfo.Name == nameof(object.Equals) && methodInfo.GetParameters().Length == 1 && methodInfo.ReturnType == typeof(bool))
                    return WellKnownOperation.Equals;

                if (methodInfo.Name == nameof(IComparable.CompareTo) && methodInfo.GetParameters().Length == 1 && methodInfo.ReturnType == typeof(int))
                    return WellKnownOperation.ComparableCompareTo;
            }

            return null;
        }

        public static bool TryParseStepLabelExpression(this Expression expression, out StepLabel? stepLabel, out MemberExpression? stepLabelValueMemberExpression)
        {
            stepLabel = null;
            stepLabelValueMemberExpression = null;

            if (typeof(StepLabel).IsAssignableFrom(expression.Type))
            {
                stepLabel = (StepLabel?)expression.GetValue();

                return true;
            }

            if (expression is MemberExpression outerMemberExpression)
            {
                if (outerMemberExpression.TryGetWellKnownMember() == WellKnownMember.StepLabelValue)
                {
                    stepLabel = (StepLabel?)outerMemberExpression.Expression?.GetValue();

                    return true;
                }

                if (outerMemberExpression.Expression is MemberExpression innerMemberExpression && innerMemberExpression.TryGetWellKnownMember() == WellKnownMember.StepLabelValue)
                {
                    stepLabelValueMemberExpression = outerMemberExpression;
                    stepLabel = (StepLabel?)innerMemberExpression.Expression?.GetValue();

                    return true;
                }
            }

            return false;
        }

        public static ParameterExpression? TryGetReferredParameter(this Expression expression)
        {
            var actualExpression = (Expression?)expression;

            while (true)
            {
                if (actualExpression is null)
                    break;

                actualExpression = actualExpression.StripConvert();

                switch (actualExpression)
                {
                    case ParameterExpression parameterExpression:
                    {
                        return parameterExpression;
                    }
                    case LambdaExpression lambdaExpression:
                    {
                        actualExpression = lambdaExpression.Body;
                        break;
                    }
                    case MemberExpression memberExpression:
                    {
                        actualExpression = memberExpression.Expression;
                        break;
                    }
                    case MethodCallExpression methodCallExpression:
                    {
                        actualExpression = methodCallExpression.Object;
                        break;
                    }
                    case UnaryExpression unaryExpression:
                    {
                        actualExpression = unaryExpression.Operand;
                        break;
                    }
                    default:
                    {
                        actualExpression = null;
                        break;
                    }
                }
            }

            return null;
        }

        public static bool IsIdentityExpression(this LambdaExpression expression)
        {
            return expression.Parameters.Count == 1 && expression.Body.StripConvert() == expression.Parameters[0];
        }

        public static GremlinExpression? TryToGremlinExpression(this Expression body, IGremlinQueryEnvironment environment)
        {
            var maybeExpression = body.TryToGremlinExpressionImpl(environment);

            if (maybeExpression is { } expression)
            {
                if (expression.Left.Expression is MethodCallExpression leftMethodCallExpression)
                {
                    var wellKnownOperation = leftMethodCallExpression.TryGetWellKnownOperation();

                    if (wellKnownOperation == WellKnownOperation.ComparableCompareTo && expression.Right.TryGetValue() is IConvertible convertible)
                    {
                        var maybeComparison = default(int?);

                        try
                        {
                            maybeComparison = convertible.ToInt32(CultureInfo.InvariantCulture);
                        }
                        catch (FormatException)
                        {
                            
                        }

                        if (maybeComparison is { } comparison)
                        {
                            if (expression.Semantics is ObjectExpressionSemantics objectExpressionSemantics)
                            {
                                var transformed = objectExpressionSemantics.TransformCompareTo(comparison);

                                return transformed switch
                                {
                                    TrueExpressionSemantics => GremlinExpression.True,
                                    FalseExpressionSemantics => GremlinExpression.False,
                                    _ => new GremlinExpression(
                                        ExpressionFragment.Create(leftMethodCallExpression.Object!, environment),
                                        transformed,
                                        ExpressionFragment.Create(leftMethodCallExpression.Arguments[0], environment))
                                };
                            }
                        }
                    }
                }
            }

            return maybeExpression;
        }

        private static GremlinExpression? TryToGremlinExpressionImpl(this Expression body, IGremlinQueryEnvironment environment)
        {
            switch (body)
            {
                case MemberExpression { Member: PropertyInfo property } memberExpression when property.PropertyType == typeof(bool) && memberExpression.TryGetReferredParameter() is not null:
                {
                    return new GremlinExpression(
                        ExpressionFragment.Create(memberExpression, environment),
                        EqualsExpressionSemantics.Instance,
                        ExpressionFragment.True);
                }
                case BinaryExpression binaryExpression when binaryExpression.NodeType.TryToSemantics(out var semantics):
                {
                    return new GremlinExpression(
                        ExpressionFragment.Create(binaryExpression.Left, environment),
                        semantics,
                        ExpressionFragment.Create(binaryExpression.Right, environment));
                }
                case MethodCallExpression { Object: { } targetExpression, Arguments: [var firstArgument, ..] } instanceMethodCallExpression:
                {
                    var wellKnownMember = instanceMethodCallExpression.TryGetWellKnownOperation();

                    switch (wellKnownMember)
                    {
                        case WellKnownOperation.Equals:
                        {
                            return new GremlinExpression(
                                ExpressionFragment.Create(targetExpression, environment),
                                EqualsExpressionSemantics.Instance,
                                ExpressionFragment.Create(firstArgument, environment));
                        }
                        case WellKnownOperation.ListContains:
                        {
                            return new GremlinExpression(
                                ExpressionFragment.Create(targetExpression, environment),
                                ContainsExpressionSemantics.Instance,
                                ExpressionFragment.Create(firstArgument, environment));
                        }
                        case WellKnownOperation.StringEquals:
                        case WellKnownOperation.StringStartsWith:
                        case WellKnownOperation.StringEndsWith:
                        case WellKnownOperation.StringContains:
                        {
                            var instanceExpression = targetExpression.StripConvert();
                            var argumentExpression = firstArgument.StripConvert();

                            var stringComparison = instanceMethodCallExpression.Arguments is [_, { } secondArgument, ..] && secondArgument.Type == typeof(StringComparison)
                                ? (StringComparison)secondArgument.GetValue()!
                                : StringComparison.Ordinal;

                            if (wellKnownMember == WellKnownOperation.StringStartsWith && argumentExpression.TryGetReferredParameter() is not null)
                            {
                                if (instanceExpression.GetValue()?.ToString() is { } stringValue)
                                {
                                    return new GremlinExpression(
                                        ExpressionFragment.Create(stringValue, environment),
                                        StartsWithExpressionSemantics.Get(stringComparison),
                                        ExpressionFragment.Create(argumentExpression, environment));
                                }
                            }
                            else if (instanceExpression.TryGetReferredParameter() is not null)
                            {
                                if (argumentExpression.GetValue() is string stringValue)
                                {
                                    return new GremlinExpression(
                                        ExpressionFragment.Create(instanceExpression, environment),
                                        wellKnownMember switch
                                        {
                                            WellKnownOperation.StringEquals => StringEqualsExpressionSemantics.Get(stringComparison),
                                            WellKnownOperation.StringStartsWith => StartsWithExpressionSemantics.Get(stringComparison),
                                            WellKnownOperation.StringContains => HasInfixExpressionSemantics.Get(stringComparison),
                                            WellKnownOperation.StringEndsWith => EndsWithExpressionSemantics.Get(stringComparison),
                                            _ => throw new ExpressionNotSupportedException(instanceMethodCallExpression)
                                        },
                                        ExpressionFragment.Create(stringValue, environment));
                                }
                            }

                            break;
                        }
                    }

                    break;
                }
                case MethodCallExpression { Object: null, Arguments: [var firstArgument, ..] } staticMethodCallExpression:
                {
                    var wellKnownMember = staticMethodCallExpression.TryGetWellKnownOperation();

                    switch (wellKnownMember)
                    {
                        case WellKnownOperation.EnumerableIntersectAny when firstArgument.StripConvert() is MethodCallExpression { Arguments: [var anyTarget, var anyArgument] }:
                        {
                            return new GremlinExpression(
                                ExpressionFragment.Create(anyTarget, environment),
                                IntersectsExpressionSemantics.Instance,
                                ExpressionFragment.Create(anyArgument, environment));
                        }
                        case WellKnownOperation.EnumerableAny:
                        {
                            return new GremlinExpression(
                                ExpressionFragment.Create(firstArgument, environment),
                                NotEqualsExpressionSemantics.Instance,
                                ExpressionFragment.Null);
                        }
                        case WellKnownOperation.EnumerableContains when staticMethodCallExpression.Arguments is [_, var secondArgument]:
                        {
                            return new GremlinExpression(
                                ExpressionFragment.Create(firstArgument, environment),
                                ContainsExpressionSemantics.Instance,
                                ExpressionFragment.Create(secondArgument, environment));
                        }
                    }

                    break;
                }
            }

            return default;
        }

        private static Array GetValue(this NewArrayExpression expression)
        {
            var array = Array.CreateInstance(
                expression.Type.GetElementType()!,
                expression.Expressions.Count);

            for (var i = 0; i < expression.Expressions.Count; i++)
            {
                array.SetValue(
                    expression.Expressions[i].GetValue(),
                    i);
            }

            return array;
        }

        private static object?[] GetArguments(this IArgumentProvider argumentProvider)
        {
            if (argumentProvider.ArgumentCount > 0)
            {
                var arguments = new object?[argumentProvider.ArgumentCount];

                for (var i = 0; i < arguments.Length; i++)
                {
                    arguments[i] = argumentProvider.GetArgument(i).GetValue();
                }

                return arguments;
            }

            return Array.Empty<object>();
        }

        private static MethodInfo Get(Expression<Action> expression)
        {
            return ((MethodCallExpression)expression.Body).Method;
        }

        private static MethodInfo Get<TSource>(Expression<Action<TSource>> expression)
        {
            return ((MethodCallExpression)expression.Body).Method;
        }
    }
}
