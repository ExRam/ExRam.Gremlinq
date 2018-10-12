﻿using System.Text;

namespace ExRam.Gremlinq
{
    public struct Lambda : IGroovySerializable
    {
        private readonly string _lambda;

        public Lambda(string lambda)
        {
            _lambda = lambda;
        }

        public GroovyExpressionState Serialize(StringBuilder stringBuilder, GroovyExpressionState state)
        {
            return state.AppendLambda(stringBuilder, _lambda);
        }
    }
}