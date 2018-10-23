﻿using System.Collections.Immutable;
using System.Text;

namespace ExRam.Gremlinq
{
    public class MethodStep : TerminalStep
    {
        public MethodStep(string name, params object[] parameters) : this(name, ImmutableList.Create(parameters))
        {

        }

        public MethodStep(string name, IImmutableList<object> parameters)
        {
            Name = name;
            Parameters = parameters;
        }

        public override GroovyExpressionState Serialize(StringBuilder stringBuilder, GroovyExpressionState state)
        {
            return state.AppendMethod(stringBuilder, Name, Parameters);
        }

        public string Name { get; }
        public IImmutableList<object> Parameters { get; }
    }
}
