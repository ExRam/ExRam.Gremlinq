﻿using System;
using ExRam.Gremlinq.Core.Serialization;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace ExRam.Gremlinq.Core.Tests
{
    public static class GremlinQueryExtensions
    {
        public sealed class GremlinQueryAssertions : ReferenceTypeAssertions<IGremlinQuery, GremlinQueryAssertions>
        {
            public GremlinQueryAssertions(IGremlinQuery query)
            {
                Subject = query;
            }

            protected override string Identifier
            {
                get => typeof(IGremlinQuery).Name;
            }

            public SerializedGremlinQueryAssertions SerializeToGroovy(string serialization)
            {
                var visitor = Subject.AsAdmin().Visitors
                    .TryGet<SerializedGremlinQuery>()
                    .IfNone(() => throw new InvalidOperationException("No visitor for SerializedGremlinQuery."));

                visitor.Visit(Subject);

                var serializedQuery = visitor.Build();

                serializedQuery.QueryString
                    .Should()
                    .Be(serialization);

                return new SerializedGremlinQueryAssertions(serializedQuery);
            }
        }

        public sealed class SerializedGremlinQueryAssertions : ObjectAssertions
        {
            private readonly SerializedGremlinQuery _serializedQuery;

            public SerializedGremlinQueryAssertions(SerializedGremlinQuery serializedQuery) : base(serializedQuery)
            {
                _serializedQuery = serializedQuery;
            }

            public SerializedGremlinQueryAssertions WithParameters(params object[] parameters)
            {
                _serializedQuery.Bindings.Should().HaveCount(parameters.Length);

                for (var i = 0; i < parameters.Length; i++)
                {
                    var label = i;
                    string key = null;

                    while (label > 0 || key == null)
                    {
                        key = (char)('a' + label % 26) + key;
                        label = label / 26;
                    }

                    key = "_" + key;
                    _serializedQuery.Bindings.Should().Contain(key, parameters[i]);
                }

                return this;
            }

            public SerializedGremlinQueryAssertions WithoutParameters()
            {
                _serializedQuery.Bindings.Should().BeEmpty();

                return this;
            }
        }

        public static GremlinQueryAssertions Should(this IGremlinQuery query)
        {
            return new GremlinQueryAssertions(query);
        }
    }
}
