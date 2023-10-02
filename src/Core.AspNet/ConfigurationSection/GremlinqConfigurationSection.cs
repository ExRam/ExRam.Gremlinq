﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace ExRam.Gremlinq.Core.AspNet
{
    internal sealed class GremlinqConfigurationSection : IGremlinqConfigurationSection, IEffectiveGremlinqConfigurationSection
    {
        private readonly IConfigurationSection _baseSection;

        public GremlinqConfigurationSection(IConfiguration configuration, string? maybeSectionName = null)
        {
            configuration = maybeSectionName is { Length: > 0 } sectionName
                ? configuration.GetSection(sectionName)
                : configuration;

            _baseSection = configuration
                .GetSection("Gremlinq");
        }

        IEnumerable<IConfigurationSection> IConfiguration.GetChildren() => _baseSection.GetChildren();

        IChangeToken IConfiguration.GetReloadToken() => _baseSection.GetReloadToken();

        IConfigurationSection IConfiguration.GetSection(string key) => _baseSection.GetSection(key);

        string? IConfiguration.this[string key]
        {
            get => _baseSection[key];
            set => _baseSection[key] = value;
        }

        string? IConfigurationSection.Value
        {
            get => _baseSection.Value;
            set => _baseSection.Value = value;
        }

        string IConfigurationSection.Key => _baseSection.Key;

        string IConfigurationSection.Path => _baseSection.Path;
    }
}
