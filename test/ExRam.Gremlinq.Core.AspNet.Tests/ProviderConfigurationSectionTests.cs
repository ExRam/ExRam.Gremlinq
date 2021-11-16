﻿using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ExRam.Gremlinq.Core.AspNet.Tests
{
    public class ProviderConfigurationSectionTests
    {
        public ProviderConfigurationSectionTests() : base()
        {

        }

        [Fact]
        public void Indexer_can_be_null()
        {
            var serviceCollection = new ServiceCollection()
                .AddSingleton<IConfiguration>(new ConfigurationBuilder()
                    .AddInMemoryCollection()
                    .Build())
                .AddGremlinq(s => s
                    .UseGremlinServer())
                .BuildServiceProvider();

            var section = serviceCollection
                .GetRequiredService<IProviderConfigurationSection>();

            section["Key"]
                .Should()
                .BeNull();
        }
    }
}
