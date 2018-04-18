﻿namespace ExRam.Gremlinq
{
    public struct Lambda : IGremlinSerializable
    {
        private readonly string _lambda;

        public Lambda(string lambda)
        {
            this._lambda = lambda;
        }

        public void Serialize(IMethodStringBuilder builder, IParameterCache parameterCache)
        {
            builder.AppendLambda(this._lambda);
        }
    }
}