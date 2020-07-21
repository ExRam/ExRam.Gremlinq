﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ExRam.Gremlinq.Core
{
    public sealed class GroovyGremlinQuery
    {
        private static readonly Regex BindingRegex = new Regex("_[a-z]+", RegexOptions.Compiled);

        private readonly bool _createdInternally;

        public GroovyGremlinQuery(string script, Dictionary<string, object> bindings) : this(script, bindings, false)
        {
        }

        internal GroovyGremlinQuery(string script, Dictionary<string, object> bindings, bool createdInternally)
        {
            Script = script;
            Bindings = bindings;
            _createdInternally = createdInternally;
        }


        public override string ToString()
        {
            return Script;
        }

        public GroovyGremlinQuery Inline()
        {
            if (!_createdInternally)
                throw new InvalidOperationException($"Can't inline this {nameof(GroovyGremlinQuery)} since it was created by user code.");

            var newBindings = new Dictionary<string, object>();

            var newScript = BindingRegex.Replace(
                Script,
                match =>
                {
                    var key = match.Value;
                    var value = Bindings[key];

                    switch (value)
                    {
                        case string str:
                        {
                            return $"'{str}'";
                        }
                        case int number:
                        {
                            return number.ToString();
                        }
                        case long number:
                        {
                            return number.ToString();
                        }
                        case bool boolean:
                        {
                            return boolean.ToString().ToLower();
                        }
                        case { } obj when obj.GetType().IsEnum && obj.GetType().GetEnumUnderlyingType() == typeof(int):
                        {
                            return ((int)obj).ToString();
                        }
                        default:
                        {
                            newBindings[key] = value;

                            return key;
                        }
                    }
                });

            return new GroovyGremlinQuery(newScript, newBindings, true);
        }

        public string Script { get; }
        public Dictionary<string, object> Bindings { get; }
    }
}
