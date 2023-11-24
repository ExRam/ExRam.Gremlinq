﻿using System.Collections.Immutable;

using Gremlin.Net.Process.Traversal;

using static ExRam.Gremlinq.Core.ExceptionHelper;

namespace ExRam.Gremlinq.Core.Serialization
{
    public readonly struct GroovyExpression
    {
        private readonly string? _identifier;
        private readonly ImmutableArray<Instruction>? _instructions;

        private GroovyExpression(string identifier, ImmutableArray<Instruction> instructions)
        {
            if (identifier.Length == 0)
                throw new ArgumentException();

            _identifier = identifier;
            _instructions = instructions;
        }

        public static GroovyExpression From(string identifier, ImmutableArray<Instruction> instructions) => new(identifier, instructions);

        public string Identifier => _identifier ?? throw UninitializedStruct();
        public ImmutableArray<Instruction> Instructions => _instructions ?? throw UninitializedStruct();
    }
}
