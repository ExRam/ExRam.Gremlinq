﻿using System;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace ExRam.Gremlinq.Tests
{
    public class GremlinQueryTest
    {
        private readonly IGraphModel _model;

        public GremlinQueryTest()
        {
            _model = GraphModel
                .FromAssembly<Vertex, Edge>(Assembly.GetExecutingAssembly(), GraphElementNamingStrategy.Simple)
                .WithIdPropertyName("Id");
        }

        [Fact]
        public void AddV()
        {
            var query = g
                .AddV(new Language { Id = "id", IetfLanguageTag = "en" })
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.addV(_P1).property(_P2, _P3).property(T.id, _P4)");

            query.parameters
                .Should()
                .Contain("_P1", "Language").And
                .Contain("_P2", "IetfLanguageTag").And
                .Contain("_P3", "en").And
                .Contain("_P4", "id");
        }

        [Fact]
        public void AddV_with_nulls()
        {
            var query = g
                .AddV(new Language {Id = "id"})
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.addV(_P1).property(T.id, _P2)");

            query.parameters
                .Should()
                .Contain("_P1", "Language").And
                .Contain("_P2", "id");
        }

        [Fact]
        public void AddV_with_multi_property()
        {
            var query = g
                .AddV(new User { Id = "id", PhoneNumbers = new[] { "+4912345", "+4923456" } })
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.addV(_P1).property(_P2, _P3).property(_P4, _P5).property(_P6, _P7).property(_P8, _P9).property(_P8, _P10).property(T.id, _P11)");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Age").And
                .Contain("_P3", 0).And
                .Contain("_P4", "Gender").And
                .Contain("_P5", 0).And
                .Contain("_P6", "RegistrationDate").And
                .Contain("_P7", DateTimeOffset.MinValue).And
                .Contain("_P8", "PhoneNumbers").And
                .Contain("_P9", "+4912345").And
                .Contain("_P10", "+4923456").And
                .Contain("_P11", "id");
        }

        [Fact]
        public void AddV_with_Meta_without_properties()
        {
            var query = g
                .AddV(new Country { Id = "id", Name = "GER"})
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.addV(_P1).property(_P2, _P3).property(T.id, _P4)");

            query.parameters
                .Should()
                .Contain("_P1", "Country").And
                .Contain("_P2", "Name").And
                .Contain("_P3", "GER").And
                .Contain("_P4", "id");
        }

        [Fact]
        public void AddV_with_Meta_with_properties()
        {
            var query = g
                .AddV(new Country
                {
                    Id = "id",
                    Name = new Meta<string>("GER")
                    {
                        Properties =
                        {
                            { "de", "Deutschland" },
                            { "en", "Germany" }
                        }
                    }
                })
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.addV(_P1).property(_P2, _P3, _P4, _P5, _P6, _P7).property(T.id, _P8)");

            query.parameters
                .Should()
                .Contain("_P1", "Country").And
                .Contain("_P2", "Name").And
                .Contain("_P3", "GER").And
                .Contain("_P4", "de").And
                .Contain("_P5", "Deutschland").And
                .Contain("_P6", "en").And
                .Contain("_P7", "Germany").And
                .Contain("_P8", "id");
        }


        [Fact]
        public void AddV_with_enum_property()
        {
            var query = g
                .AddV(new User { Id = "id", Gender = Gender.Female })
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.addV(_P1).property(_P2, _P3).property(_P4, _P5).property(_P6, _P7).property(T.id, _P8)");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Age").And
                .Contain("_P3", 0).And
                .Contain("_P4", "Gender").And
                .Contain("_P5", 1).And
                .Contain("_P6", "RegistrationDate").And
                .Contain("_P7", DateTimeOffset.MinValue).And
                .Contain("_P8", "id");
        }

        [Fact]
        public void V_ofType_contains_specific_phoneNumber()
        {
            var query = g
                .V<User>()
                .Where(t => t.PhoneNumbers.Contains("+4912345"))
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).has(_P2, P.eq(_P3))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "PhoneNumbers").And
                .Contain("_P3", "+4912345");
        }

        [Fact]
        public void V_ofType_does_not_contain_specific_phoneNumber()
        {
            var query = g
                .V<User>()
                .Where(t => !t.PhoneNumbers.Contains("+4912345"))
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).not(__.has(_P2, P.eq(_P3)))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "PhoneNumbers").And
                .Contain("_P3", "+4912345");
        }

        [Fact]
        public void V_ofType_contains_a_phoneNumber()
        {
            var query = g
                .V<User>()
                .Where(t => t.PhoneNumbers.Any())
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).has(_P2)");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "PhoneNumbers");
        }

        [Fact]
        public void V_ofType_contains_no_phoneNumber()
        {
            var query = g
                .V<User>()
                .Where(t => !t.PhoneNumbers.Any())
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).not(__.has(_P2))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "PhoneNumbers");
        }

        [Fact]
        public void V_ofType_intersects_phoneNumbers()
        {
            var query = g
                .V<User>()
                .Where(t => t.PhoneNumbers.Intersects(new[] { "+4912345", "+4923456" }))
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).has(_P2, P.within(_P3, _P4))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "PhoneNumbers").And
                .Contain("_P3", "+4912345").And
                .Contain("_P4", "+4923456");
        }

        [Fact]
        public void V_ofType_not_intersects_phoneNumbers()
        {
            var query = g
                .V<User>()
                .Where(t => !t.PhoneNumbers.Intersects(new[] { "+4912345", "+4923456" }))
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).not(__.has(_P2, P.within(_P3, _P4)))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "PhoneNumbers").And
                .Contain("_P3", "+4912345").And
                .Contain("_P4", "+4923456");
        }

        [Fact]
        public void V_ofType_Age_is_contained_in_some_array()
        {
            var query = g
                .V<User>()
                .Where(t => new[] { 36, 37, 38 }.Contains(t.Age))
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).has(_P2, P.within(_P3, _P4, _P5))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Age").And
                .Contain("_P3", 36).And
                .Contain("_P4", 37).And
                .Contain("_P5", 38);
        }

        [Fact]
        public void V_ofType_Age_is_contained_in_some_enumerable()
        {
            var enumerable = new[] { "36", "37", "38" }
                .Select(int.Parse);

            var query = g
                .V<User>()
                .Where(t => enumerable.Contains(t.Age))
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).has(_P2, P.within(_P3, _P4, _P5))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Age").And
                .Contain("_P3", 36).And
                .Contain("_P4", 37).And
                .Contain("_P5", 38);
        }

        [Fact]
        public void V_ofType_Age_is_not_contained_in_some_array()
        {
            var query = g
                .V<User>()
                .Where(t => !new[] { 36, 37, 38 }.Contains(t.Age))
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).not(__.has(_P2, P.within(_P3, _P4, _P5)))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Age").And
                .Contain("_P3", 36).And
                .Contain("_P4", 37).And
                .Contain("_P5", 38);
        }

        [Fact]
        public void V_ofType_Age_is_not_contained_in_some_enumerable()
        {
            var enumerable = new[] { "36", "37", "38" }
                .Select(int.Parse);

            var query = g
                .V<User>()
                .Where(t => !enumerable.Contains(t.Age))
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).not(__.has(_P2, P.within(_P3, _P4, _P5)))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Age").And
                .Contain("_P3", 36).And
                .Contain("_P4", 37).And
                .Contain("_P5", 38);
        }

        [Fact]
        public void CountryCallingCode_is_prefix_of_some_string()
        {
            var query = g
                .V<CountryCallingCode>()
                .Where(c => "+49123".StartsWith(c.Prefix))
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).has(_P2, P.within(_P3, _P4, _P5, _P6, _P7, _P8, _P9))");

            query.parameters
                .Should()
                .Contain("_P1", "CountryCallingCode").And
                .Contain("_P2", "Prefix").And
                .Contain("_P3", "").And
                .Contain("_P4", "+").And
                .Contain("_P5", "+4").And
                .Contain("_P6", "+49").And
                .Contain("_P7", "+491").And
                .Contain("_P8", "+4912").And
                .Contain("_P9", "+49123");
        }

        [Fact]
        public void CountryCallingCode_is_prefix_of_some_string_variable()
        {
            const string str = "+49123";

            var query = g
                .V<CountryCallingCode>()
                .Where(c => str.StartsWith(c.Prefix))
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).has(_P2, P.within(_P3, _P4, _P5, _P6, _P7, _P8, _P9))");

            query.parameters
                .Should()
                .Contain("_P1", "CountryCallingCode").And
                .Contain("_P2", "Prefix").And
                .Contain("_P3", "").And
                .Contain("_P4", "+").And
                .Contain("_P5", "+4").And
                .Contain("_P6", "+49").And
                .Contain("_P7", "+491").And
                .Contain("_P8", "+4912").And
                .Contain("_P9", "+49123");
        }

        [Fact]
        public void CountryCallingCode_is_prefix_of_some_string_processed_variable()
        {
            const string str = "+49123xxx";

            var query = g
                .V<CountryCallingCode>()
                .Where(c => str.Substring(0, 6).StartsWith(c.Prefix))
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).has(_P2, P.within(_P3, _P4, _P5, _P6, _P7, _P8, _P9))");

            query.parameters
                .Should()
                .Contain("_P1", "CountryCallingCode").And
                .Contain("_P2", "Prefix").And
                .Contain("_P3", "").And
                .Contain("_P4", "+").And
                .Contain("_P5", "+4").And
                .Contain("_P6", "+49").And
                .Contain("_P7", "+491").And
                .Contain("_P8", "+4912").And
                .Contain("_P9", "+49123");
        }

        [Fact]
        public void User_PhoneNumber_has_some_prefix()
        {
            var query = g
                .V<User>()
                .Where(c => c.PhoneNumber.StartsWith("+49123"))
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).has(_P2, P.between(_P3, _P4))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "PhoneNumber").And
                .Contain("_P3", "+49123").And
                .Contain("_P4", "+49124");
        }

        [Fact]
        public void User_PhoneNumber_has_empty_prefix()
        {
            var query = g
                .V<User>()
                .Where(c => c.PhoneNumber.StartsWith(""))
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).has(_P2, P.between(_P3, _P4))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "PhoneNumber").And
                .Contain("_P3", "").And
                .Contain("_P4", char.MinValue.ToString());
        }

        [Fact]
        public void V_ofType_has_disjunction()
        {
            var query = g
                .V<User>()
                .Where(t => t.Age == 36 || t.Age == 42)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).or(__.has(_P2, P.eq(_P3)), __.has(_P2, P.eq(_P4)))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Age").And
                .Contain("_P3", 36).And
                .Contain("_P4", 42);
        }

        [Fact]
        public void V_ofType_has_disjunction_with_different_fields()
        {
            var query = g
                .V<User>()
                .Where(t => t.Name == "Some name" || t.Age == 42)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).or(__.has(_P2, P.eq(_P3)), __.has(_P4, P.eq(_P5)))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Name").And
                .Contain("_P3", "Some name").And
                .Contain("_P4", "Age").And
                .Contain("_P5", 42);
        }

        [Fact]
        public void V_ofType_has_conjunction()
        {
            var query = g
                .V<User>()
                .Where(t => t.Age == 36 && t.Age == 42)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).and(__.has(_P2, P.eq(_P3)), __.has(_P2, P.eq(_P4)))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Age").And
                .Contain("_P3", 36).And
                .Contain("_P4", 42);
        }

        [Fact]
        public void V_ofType_complex_logical_expression()
        {
            var query = g
                .V<User>()
                .Where(t => t.Name == "Some name" && (t.Age == 42 || t.Age == 99))
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).and(__.has(_P2, P.eq(_P3)), __.or(__.has(_P4, P.eq(_P5)), __.has(_P4, P.eq(_P6))))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Name").And
                .Contain("_P3", "Some name").And
                .Contain("_P4", "Age").And
                .Contain("_P5", 42).And
                .Contain("_P6", 99);
        }

        [Fact]
        public void V_ofType_complex_logical_expression_with_null()
        {
            var query = g
                .V<User>()
                .Where(t => t.Name == null && (t.Age == 42 || t.Age == 99))
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).and(__.not(__.has(_P2)), __.or(__.has(_P3, P.eq(_P4)), __.has(_P3, P.eq(_P5))))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Name").And
                .Contain("_P3", "Age").And
                .Contain("_P4", 42).And
                .Contain("_P5", 99);
        }

        [Fact]
        public void V_ofType_has_conjunction_of_three()
        {
            var query = g
                .V<User>()
                .Where(t => t.Age == 36 && t.Age == 42 && t.Age == 99)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).and(__.has(_P2, P.eq(_P3)), __.has(_P2, P.eq(_P4)), __.has(_P2, P.eq(_P5)))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Age").And
                .Contain("_P3", 36).And
                .Contain("_P4", 42).And
                .Contain("_P5", 99);
        }

        [Fact]
        public void V_ofType_has_disjunction_of_three()
        {
            var query = g
                .V<User>()
                .Where(t => t.Age == 36 || t.Age == 42 || t.Age == 99)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).or(__.has(_P2, P.eq(_P3)), __.has(_P2, P.eq(_P4)), __.has(_P2, P.eq(_P5)))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Age").And
                .Contain("_P3", 36).And
                .Contain("_P4", 42).And
                .Contain("_P5", 99);
        }

        [Fact]
        public void V_ofType_has_conjunction_with_different_fields()
        {
            var query = g
                .V<User>()
                .Where(t => t.Name == "Some name" && t.Age == 42)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).and(__.has(_P2, P.eq(_P3)), __.has(_P4, P.eq(_P5)))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Name").And
                .Contain("_P3", "Some name").And
                .Contain("_P4", "Age").And
                .Contain("_P5", 42);
        }

        [Fact]
        public void V_ofType()
        {
            var query = g
                .V<User>()
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1)");

            query.parameters
                .Should()
                .Contain("_P1", "User");
        }

        [Fact]
        public void V_ofType_does_not_include_abstract_types()
        {
            var query = g
                .V<Authority>()
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1, _P2)");

            query.parameters
                .Should()
                .Contain("_P1", "Company").And
                .Contain("_P2", "User");
        }

        [Fact]
        public void V_ofType_has_int_property()
        {
            var query = g
                .V<User>()
                .Where(t => t.Age == 36)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).has(_P2, P.eq(_P3))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Age").And
                .Contain("_P3", 36);
        }

        [Fact]
        public void V_ofType_has_int_expression_property()
        {
            const int i = 18;

            var query = g
                .V<User>()
                .Where(t => t.Age == i + i)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).has(_P2, P.eq(_P3))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Age").And
                .Contain("_P3", 36);
        }

        [Fact]
        public void V_ofType_has_converted_int_property()
        {
            var query = g
                .V<User>()
                .Where(t => (object)t.Age == (object)36)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).has(_P2, P.eq(_P3))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Age").And
                .Contain("_P3", 36);
        }

        [Fact]
        public void V_ofType_has_unequal_int_property()
        {
            var query = g
                .V<User>()
                .Where(t => t.Age != 36)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).has(_P2, P.neq(_P3))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Age").And
                .Contain("_P3", 36);
        }

        [Fact]
        public void V_ofType_has_no_string_property()
        {
            var query = g
                .V<User>()
                .Where(t => t.Name == null)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).not(__.has(_P2))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Name");
        }

        [Fact]
        public void V_ofType_string_property_exists()
        {
            var query = g
                .V<User>()
                .Where(t => t.Name != null)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).has(_P2)");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Name");
        }

        [Fact]
        public void V_ofType_has_lower_int_property()
        {
            var query = g
                .V<User>()
                .Where(t => t.Age < 36)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).has(_P2, P.lt(_P3))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Age").And
                .Contain("_P3", 36);
        }

        [Fact]
        public void V_ofType_has_lower_or_equal_int_property()
        {
            var query = g
                .V<User>()
                .Where(t => t.Age <= 36)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).has(_P2, P.lte(_P3))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Age").And
                .Contain("_P3", 36);
        }

        [Fact]
        public void V_ofType_has_bool_property_with_explicit_comparison1()
        {
            var query = g
                .V<TimeFrame>()
                // ReSharper disable once RedundantBoolCompare
                .Where(t => t.Enabled == true)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).has(_P2, P.eq(_P3))");

            query.parameters
                .Should()
                .Contain("_P1", "TimeFrame").And
                .Contain("_P2", "Enabled").And
                .Contain("_P3", true);
        }

        [Fact]
        public void V_ofType_has_bool_property_with_explicit_comparison2()
        {
            var query = g
                .V<TimeFrame>()
                .Where(t => t.Enabled == false)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).has(_P2, P.eq(_P3))");

            query.parameters
                .Should()
                .Contain("_P1", "TimeFrame").And
                .Contain("_P2", "Enabled").And
                .Contain("_P3", false);
        }

        [Fact]
        public void V_ofType_has_bool_property_with_implicit_comparison1()
        {
            var query = g
                .V<TimeFrame>()
                .Where(t => t.Enabled)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).has(_P2, P.eq(_P3))");

            query.parameters
                .Should()
                .Contain("_P1", "TimeFrame").And
                .Contain("_P2", "Enabled").And
                .Contain("_P3", true);
        }

        [Fact]
        public void V_ofType_has_bool_property_with_implicit_comparison2()
        {
            var query = g
                .V<TimeFrame>()
                .Where(t => !t.Enabled)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).not(__.has(_P2, P.eq(_P3)))");

            query.parameters
                .Should()
                .Contain("_P1", "TimeFrame").And
                .Contain("_P2", "Enabled").And
                .Contain("_P3", true);
        }

        [Fact]
        public void V_ofType_has_greater_int_property()
        {
            var query = g
                .V<User>()
                .Where(t => t.Age > 36)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).has(_P2, P.gt(_P3))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Age").And
                .Contain("_P3", 36);
        }

        [Fact]
        public void V_ofType_has_greater_or_equal_int_property()
        {
            var query = g
                .V<User>()
                .Where(t => t.Age >= 36)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).has(_P2, P.gte(_P3))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Age").And
                .Contain("_P3", 36);
        }

        [Fact]
        public void V_ofType_has_string_property()
        {
            var query = g
                .V<Language>()
                .Where(t => t.Id == "languageId")
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).has(T.id, P.eq(_P2))");

            query.parameters
                .Should()
                .Contain("_P1", "Language").And
                .Contain("_P2", "languageId");
        }

        [Fact]
        public void V_ofType_where_with_local_string()
        {
            const string local = "languageId";

            var query = g
                .V<Language>()
                .Where(t => t.Id == local)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).has(T.id, P.eq(_P2))");

            query.parameters
                .Should()
                .Contain("_P1", "Language").And
                .Contain("_P2", "languageId");
        }

        [Fact]
        public void V_ofType_where_with_local_anonymous_type()
        {
            var local = new { Value = "languageId" };

            var query = g
                .V<Language>()
                .Where(t => t.Id == local.Value)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).has(T.id, P.eq(_P2))");

            query.parameters
                .Should()
                .Contain("_P1", "Language").And
                .Contain("_P2", "languageId");
        }

        [Fact]
        public void V_of_type_where_with_expression_parameter_on_both_sides()
        {
            g
                .V<Language>()
                .Invoking(query => query.Where(t => t.Id == t.IetfLanguageTag))
                .Should()
                .Throw<InvalidOperationException>();
        }

        [Fact]
        public void V_of_type_where_with_stepLabel()
        {
            var l = new StepLabel<Language>();

            var query = g
                .V<Language>()
                .As(l)
                .V<Language>()
                .Where(l2 => l2 == l)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).as(_P2).V().hasLabel(_P1).where(P.eq(_P2))");

            query.parameters
                .Should()
                .Contain("_P1", "Language").And
                .Contain("_P2", "l1");
        }

        [Fact]
        public void V_of_type_where_value_equals_stepLabel()
        {
            var l = new StepLabel<string>();

            var query = g
                .V<Language>()
                .Values(x => x.IetfLanguageTag)
                .As(l)
                .V<Language>()
                .Where(l2 => l2.IetfLanguageTag == l)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).values(_P2).as(_P3).V().hasLabel(_P1).has(_P2, __.where(P.eq(_P3)))");

            query.parameters
                .Should()
                .Contain("_P1", "Language").And
                .Contain("_P2", "IetfLanguageTag").And
                .Contain("_P3", "l1");
        }

        [Fact]
        public void V_of_type_where_with_stepLabel_not_inlined()
        {
            var l = new StepLabel<Language>();

            var query = g
                .V<Language>()
                .As(l)
                .V<Language>()
                .Where(l2 => l2 == l)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).as(_P2).V().hasLabel(_P1).where(P.eq(_P2))");

            query
                .parameters
                .Should()
                .Contain("_P1", "Language").And
                .Contain("_P2", "l1");
        }

        [Fact]
        public void V_where_with_scalar()
        {
            var query = g
                .V<User>()
                .Values(x => x.Age)
                .Where(_ => _ == 36)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).values(_P2).is(P.eq(_P3))");

            query
                .parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Age").And
                .Contain("_P3", 36);
        }

        [Fact]
        public void V_where_with_traversal()
        {
            var query = g
                .V<User>()
                .Where(_ => _.Out<LivesIn>())
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).where(__.out(_P2))");

            query
                .parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "LivesIn");
        }

        [Fact]
        public void AddE_to_traversal()
        {
            var now = DateTimeOffset.UtcNow;

            var query = g
                .AddV(new User
                {
                    Name = "Bob",
                    RegistrationDate = now
                })
                .AddE(new LivesIn())
                .To(__ => __
                    .V<Country>()
                    .Where(t => t.CountryCallingCode == "+49"))
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.addV(_P1).property(_P2, _P3).property(_P4, _P5).property(_P6, _P7).property(_P8, _P9).addE(_P10).to(__.V().hasLabel(_P11).has(_P12, P.eq(_P13)))");

            query
                .parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Age").And
                .Contain("_P3", 0).And
                .Contain("_P4", "Gender").And
                .Contain("_P5", 0).And
                .Contain("_P6", "RegistrationDate").And
                .Contain("_P7", now).And
                .Contain("_P8", "Name").And
                .Contain("_P9", "Bob").And
                .Contain("_P10", "LivesIn").And
                .Contain("_P11", "Country").And
                .Contain("_P12", "CountryCallingCode").And
                .Contain("_P13", "+49");
        }

        [Fact]
        public void AddE_to_StepLabel()
        {
            var query = g
                .AddV(new Language { IetfLanguageTag = "en" })
                .As((_, l) => _
                    .AddV(new Country { CountryCallingCode = "+49" })
                    .AddE(new IsDescribedIn { Text = "Germany" })
                    .To(l))
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.addV(_P1).property(_P2, _P3).as(_P4).addV(_P5).property(_P6, _P7).addE(_P8).property(_P9, _P10).to(_P4)");

            query
                .parameters
                .Should()
                .Contain("_P1", "Language").And
                .Contain("_P2", "IetfLanguageTag").And
                .Contain("_P3", "en").And
                .Contain("_P4", "l1").And
                .Contain("_P5", "Country").And
                .Contain("_P6", "CountryCallingCode").And
                .Contain("_P7", "+49").And
                .Contain("_P8", "IsDescribedIn").And
                .Contain("_P9", "Text").And
                .Contain("_P10", "Germany");
        }

        [Fact]
        public void AddE_from_traversal()
        {
            var now = DateTimeOffset.UtcNow;

            var query = g
                .AddV(new User
                {
                    Name = "Bob",
                    RegistrationDate = now
                })
                .AddE(new LivesIn())
                .From(__ => __
                    .V<Country>()
                    .Where(t => t.CountryCallingCode == "+49"))
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.addV(_P1).property(_P2, _P3).property(_P4, _P5).property(_P6, _P7).property(_P8, _P9).addE(_P10).from(__.V().hasLabel(_P11).has(_P12, P.eq(_P13)))");

            query
                .parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Age").And
                .Contain("_P3", 0).And
                .Contain("_P4", "Gender").And
                .Contain("_P5", 0).And
                .Contain("_P6", "RegistrationDate").And
                .Contain("_P7", now).And
                .Contain("_P8", "Name").And
                .Contain("_P9", "Bob").And
                .Contain("_P10", "LivesIn").And
                .Contain("_P11", "Country").And
                .Contain("_P12", "CountryCallingCode").And
                .Contain("_P13", "+49");
        }

        [Fact]
        public void AddE_from_StepLabel()
        {
            var query = g
                .AddV(new Country { CountryCallingCode = "+49" })
                .As((_, c) => _
                    .AddV(new Language { IetfLanguageTag = "en" })
                    .AddE(new IsDescribedIn { Text = "Germany" })
                    .From(c))
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.addV(_P1).property(_P2, _P3).as(_P4).addV(_P5).property(_P6, _P7).addE(_P8).property(_P9, _P10).from(_P4)");

            query.parameters
                .Should()
                .Contain("_P1", "Country").And
                .Contain("_P2", "CountryCallingCode").And
                .Contain("_P3", "+49").And
                .Contain("_P4", "l1").And
                .Contain("_P5", "Language").And
                .Contain("_P6", "IetfLanguageTag").And
                .Contain("_P7", "en").And
                .Contain("_P8", "IsDescribedIn").And
                .Contain("_P9", "Text").And
                .Contain("_P10", "Germany");
        }

        [Fact]
        public void AddE_InV()
        {
            var query = g
                .AddV<User>()
                .AddE<LivesIn>()
                .To(__ => __
                    .V<Country>("id"))
                .InV()
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.addV(_P1).property(_P2, _P3).property(_P4, _P5).property(_P6, _P7).addE(_P8).to(__.V(_P9).hasLabel(_P10)).inV()");
        }

        [Fact]
        public void AddE_OutV()
        {
            var query = g
                .AddV<User>()
                .AddE<LivesIn>()
                .To(__ => __
                    .V<Country>("id"))
                .OutV()
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.addV(_P1).property(_P2, _P3).property(_P4, _P5).property(_P6, _P7).addE(_P8).to(__.V(_P9).hasLabel(_P10)).outV()");
        }

        [Fact]
        public void And()
        {
            var query = g
                .V<User>()
                .And(
                    __ => __
                        .InE<Knows>(),
                    __ => __
                        .OutE<LivesIn>())
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).and(__.inE(_P2), __.outE(_P3))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Knows").And
                .Contain("_P3", "LivesIn");
        }

        [Fact]
        public void Drop()
        {
            var query = g
                .V<User>()
                .Drop()
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).drop()");

            query.parameters
                .Should()
                .Contain("_P1", "User");
        }

        [Fact]
        public void FilterWithLambda()
        {
            var query = g
                .V<User>()
                .Filter("it.property('str').value().length() == 2")
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).filter({it.property('str').value().length() == 2})");

            query.parameters
                .Should()
                .Contain("_P1", "User");
        }

        [Fact]
        public void Out()
        {
            var query = g
                .V<User>()
                .Out<Knows>()
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).out(_P2)");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Knows");
        }

        [Fact]
        public void Out_does_not_include_abstract_edge()
        {
            var query = g
                .V<User>()
                .Out<Edge>()
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).out(_P2, _P3, _P4, _P5, _P6)");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "IsDescribedIn").And
                .Contain("_P3", "Knows").And
                .Contain("_P4", "LivesIn").And
                .Contain("_P5", "Speaks").And
                .Contain("_P6", "WorksFor");
        }

        [Fact]
        public void V_ofType_order_ByMember()
        {
            var query = g
                .V<User>()
                .OrderBy(x => x.Name)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).order().by(_P2, Order.incr)");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Name");
        }

        [Fact]
        public void V_ofType_order_ByTraversal()
        {
            var query = g
                .V<User>()
                .OrderBy(__ => __.Values(x => x.Name))
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).order().by(__.values(_P2), Order.incr)");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Name");
        }

        [Fact]
        public void V_ofType_order_ByLambda()
        {
            var query = g
                .V<User>()
                .OrderBy("it.property('str').value().length()")
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).order().by({it.property('str').value().length()})");

            query.parameters
                .Should()
                .Contain("_P1", "User");
        }

        [Fact]
        public void V_ofType_sum_With_local_scope()
        {
            var query = g
                .V<User>()
                .Values(x => x.Age)
                .Sum(Scope.Local)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).values(_P2).sum(Scope.local)");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Age");
        }

        [Fact]
        public void V_ofType_sum_With_global_scope()
        {
            var query = g
                .V<User>()
                .Values(x => x.Age)
                .Sum(Scope.Global)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).values(_P2).sum(Scope.global)");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Age");
        }

        [Fact]
        public void V_ofType_values_of_one_property()
        {
            var query = g
                .V<User>()
                .Values(x => x.Name)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).values(_P2)");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Name");
        }

        [Fact]
        public void V_ofType_values_of_Id_property()
        {
            var query = g
                .V<User>()
                .Values(x => x.Id)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).id()");

            query.parameters
                .Should()
                .Contain("_P1", "User");
        }

        [Fact]
        public void V_ofType_values_of_two_properties()
        {
            var query = g
                .V<User>()
                .Values(x => x.Name, x => x.Id)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).union(__.values(_P2), __.id())");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Name");
        }

        [Fact]
        public void V_ofType_values_of_three_properties()
        {
            var query = g
                .V<User>()
                .Values(x => (object)x.Name, x => x.Gender, x => x.Id)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).union(__.values(_P2, _P3), __.id())");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Name").And
                .Contain("_P3", "Gender");
        }

        [Fact]
        public void V_without_type()
        {
            var query = g
                .V()
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V()");

            query.parameters
                .Should()
                .BeEmpty();
        }

        [Fact]
        public void V_OfType_with_inheritance()
        {
            var query = g
                .V()
                .OfType<Authority>()
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1, _P2)");

            query.parameters
                .Should()
                .Contain("_P1", "Company").And
                .Contain("_P2", "User");
        }

        [Fact]
        public void V_ofType_Repeat_out_traversal()
        {
            var query = g
                .V<User>()
                .Repeat(__ => __
                    .Out<Knows>()
                    .OfType<User>())
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).repeat(__.out(_P2).hasLabel(_P1))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Knows");
        }

        [Fact]
        public void V_ofType_Union_two_out_traversals()
        {
            var query = g
                .V<User>()
                .Union(
                    __ => __.Out<Knows>(),
                    __ => __.Out<LivesIn>())
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).union(__.out(_P2), __.out(_P3))");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Knows").And
                .Contain("_P3", "LivesIn");
        }

        [Fact]
        public void V_ofType_Optional_one_out_traversal()
        {
            var query = g
                .V()
                .Optional(
                    __ => __.Out<Knows>())
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().optional(__.out(_P1))");

            query.parameters
                .Should()
                .Contain("_P1", "Knows");
        }

        [Fact]
        public void V_ofType_Not_one_out_traversal()
        {
            var query = g
                .V()
                .Not(__ => __.Out<Knows>())
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().not(__.out(_P1))");

            query.parameters
                .Should()
                .Contain("_P1", "Knows");
        }

        [Fact]
        public void V_ofType_Optional_one_out_traversal_1()
        {
            var query = g
                .V()
                .Not(__ => __.OfType<Language>())
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().not(__.hasLabel(_P1))");

            query.parameters
                .Should()
                .Contain("_P1", "Language");
        }

        [Fact]
        public void V_ofType_Optional_one_out_traversal_2()
        {
            var query = g
                .V()
                .Not(__ => __.OfType<Authority>())
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().not(__.hasLabel(_P1, _P2))");

            query.parameters
                .Should()
                .Contain("_P1", "Company").And
                .Contain("_P2", "User");
        }

        [Fact]
        public void V_as()
        {
            var query = g
                .V<User>()
                .As(new StepLabel<User>())
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).as(_P2)");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "l1");
        }

        [Fact]
        public void V_as_not_inlined()
        {
            var query = g
                .V<User>()
                .As(new StepLabel<User>())
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).as(_P2)");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "l1");
        }

        [Fact]
        public void V_as_select()
        {
            var stepLabel = new StepLabel<User>();

            var query = g
                .V<User>()
                .As(stepLabel)
                .Select(stepLabel)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).as(_P2).select(_P2)");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "l1");
        }

        [Fact]
        public void V_as_select_not_inlined()
        {
            var stepLabel = new StepLabel<User>();

            var query = g
                .V<User>()
                .As(stepLabel)
                .Select(stepLabel)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).as(_P2).select(_P2)");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "l1");
        }

        [Fact]
        public void V_as_as_select()
        {
            var query = g
                .V<User>()
                .As((_, stepLabel1) => _
                    .As((__, stepLabel2) => __
                        .Select(stepLabel1, stepLabel2)))
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).as(_P2).as(_P3).select(_P2, _P3)");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Item1").And
                .Contain("_P3", "Item2");
        }

        //[Fact]
        //public void Branch()
        //{
        //    var query = g
        //        .V<User>()
        //        .Branch(
        //            _ => _.Values(x => x.Name),
        //            _ => _.Out<Knows>(),
        //            _ => _.In<Knows>())
        //        .Resolve(_model)
        //        .Serialize();

        //    query.queryString
        //        .Should()
        //        .Be("g.V().hasLabel(_P1).branch(__.values(_P2)).option(__.out(_P3)).option(__.in(_P3))");

        //    query.parameters
        //        .Should()
        //        .Contain("_P1", "User").And
        //        .Contain("_P2", "Name").And
        //        .Contain("_P3", "Knows");
        //}

        //[Fact]
        //public void BranchOnIdentity()
        //{
        //    var query = g
        //        .V<User>()
        //        .BranchOnIdentity(
        //            _ => _.Out<Knows>(),
        //            _ => _.In<Knows>())
        //        .Resolve(_model)
        //        .Serialize();

        //    query.queryString
        //        .Should()
        //        .Be("g.V().hasLabel(_P1).branch(__.identity()).option(__.out(_P2)).option(__.in(_P2))");

        //    query.parameters
        //        .Should()
        //        .Contain("_P1", "User").And
        //        .Contain("_P2", "Knows");
        //}

        [Fact]
        public void Set_Property()
        {
            var query = g
                .V<User>()
                .Property(x => x.Age, 36)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).property(_P2, _P3)");

            query.parameters
                .Should()
                .Contain("_P1", "User").And
                .Contain("_P2", "Age").And
                .Contain("_P3", 36);
        }

        [Fact]
        public void Coalesce()
        {
            var query = g
                .V()
                .Coalesce(
                     _ => _
                        .Identity())
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().coalesce(__.identity())");
        }

        [Fact]
        public void Properties()
        {
            var query = g
                .V()
                .Properties()
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().properties()");
        }

        [Fact]
        public void Properties_with_projection()
        {
            var query = g
                .V<Country>()
                .Properties(x => x.Name)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).properties(_P2)");

            query.parameters
                .Should()
                .Contain("_P1", "Country").And
                .Contain("_P2", "Name");
        }

        [Fact]
        public void Meta_Properties()
        {
            var query = g
                .V<Country>()
                .Properties(x => x.Name)
                .Properties()
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).properties(_P2).properties()");

            query.parameters
                .Should()
                .Contain("_P1", "Country").And
                .Contain("_P2", "Name");
        }

        [Fact]
        public void Meta_Properties_with_key()
        {
            var query = g
                .V<Country>()
                .Properties(x => x.Name)
                .Properties("metaKey")
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).properties(_P2).properties(_P3)");

            query.parameters
                .Should()
                .Contain("_P1", "Country").And
                .Contain("_P2", "Name").And
                .Contain("_P3", "metaKey");
        }

        [Fact]
        public void Properties_Where()
        {
            var query = g
                .V<Country>()
                .Properties(x => x.Languages)
                .Where(x => x.Value == "de")
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).properties(_P2).hasValue(P.eq(_P3))");
        }

        [Fact]
        public void Properties_Where_reversed()
        {
            var query = g
                .V<Country>()
                .Properties(x => x.Languages)
                .Where(x => "de" == x.Value)
                .Resolve(_model)
                .Serialize();

            query.queryString
                .Should()
                .Be("g.V().hasLabel(_P1).properties(_P2).hasValue(P.eq(_P3))");
        }

        [Fact]
        public void Limit_overflow()
        {
            g
                .V()
                .Invoking(_ => _.Limit((long)int.MaxValue + 1))
                .Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void Anonymous()
        {
            var query = GremlinQuery.Anonymous
                .Serialize();

            query.queryString.Should().Be("__.identity()");
        }

        [Fact]
        public void WithSubgraphStrategyTest()
        {
            var query = g
                .WithSubgraphStrategy(_ => _.OfType<User>(), _ => _);

            query.Steps.Should().HaveCount(2);
            query.Steps[1].Should().BeOfType<MethodStep>();
            query.Steps[1].As<MethodStep>().Name.Should().Be("withStrategies");
            query.Steps[1].As<MethodStep>().Parameters.Should().HaveCount(1);
        }

        [Fact]
        public void WithSubgraphStrategy_is_ommitted_if_empty()
        {
            var query = g
                .WithSubgraphStrategy(_ => _, _ => _);

            query.Steps.Should().HaveCount(1);
            query.Steps[0].Should().BeOfType<IdentifierStep>();
        }
    }
}
