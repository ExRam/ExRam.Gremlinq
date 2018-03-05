using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ExRam.Gremlinq
{
    internal struct GremlinPredicate : IGremlinSerializable
    {
        private static readonly IReadOnlyDictionary<ExpressionType, Func<object, GremlinPredicate>> SupportedComparisons = new Dictionary<ExpressionType, Func<object, GremlinPredicate>>
        {
            { ExpressionType.Equal, GremlinPredicate.Eq },
            { ExpressionType.NotEqual, GremlinPredicate.Neq },
            { ExpressionType.LessThan, GremlinPredicate.Lt },
            { ExpressionType.LessThanOrEqual, GremlinPredicate.Lte },
            { ExpressionType.GreaterThanOrEqual, GremlinPredicate.Gte },
            { ExpressionType.GreaterThan, GremlinPredicate.Gt }
        };

        public GremlinPredicate(object name, params object[] arguments)
        {
            this.Name = name;
            this.Arguments = arguments;
        }

        public (string queryString, IDictionary<string, object> parameters) Serialize(IParameterCache parameterCache)
        {
            var builder = new StringBuilder();
            var dict = new Dictionary<string, object>();

            builder.Append(this.Name);
            builder.Append("(");

            for (var i = 0; i < this.Arguments.Length; i++)
            {
                var parameter = this.Arguments[i];

                if (i != 0)
                    builder.Append(", ");

                if (parameter is IGremlinSerializable serializable)
                    builder.Append(serializable.Serialize(parameterCache).queryString);
                else
                {
                    var parameterName = parameterCache.Cache(parameter);
                    dict[parameterName] = parameter;
                    builder.Append(parameterName);
                }
            }

            builder.Append(")");
            return (builder.ToString(), dict);
        }

        public static GremlinPredicate Eq(object argument)
        {
            return new GremlinPredicate("P.eq", argument);
        }

        public static GremlinPredicate Neq(object argument)
        {
            return new GremlinPredicate("P.neq", argument);
        }

        public static GremlinPredicate Lt(object argument)
        {
            return new GremlinPredicate("P.lt", argument);
        }

        public static GremlinPredicate Lte(object argument)
        {
            return new GremlinPredicate("P.lte", argument);
        }

        public static GremlinPredicate Gt(object argument)
        {
            return new GremlinPredicate("P.gt", argument);
        }

        public static GremlinPredicate Gte(object argument)
        {
            return new GremlinPredicate("P.gte", argument);
        }

        public static GremlinPredicate ForExpressionType(ExpressionType expressionType, object argument)
        {
            return GremlinPredicate.SupportedComparisons[expressionType](argument);
        }

        public static GremlinPredicate Within(params object[] arguments)
        {
            return new GremlinPredicate("P.within", arguments);
        }

        public object Name { get; }
        public object[] Arguments { get; }
    }
}