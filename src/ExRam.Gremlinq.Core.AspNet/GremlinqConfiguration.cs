﻿using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace ExRam.Gremlinq.Core.AspNet
{
    internal sealed class GremlinqConfiguration : IGremlinqConfiguration
    {
        private readonly IConfigurationSection _baseConfiguration;

        public GremlinqConfiguration(IConfigurationSection baseConfiguration)
        {
            _baseConfiguration = baseConfiguration;
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            return _baseConfiguration.GetChildren();
        }

        public IChangeToken GetReloadToken()
        {
            return _baseConfiguration.GetReloadToken();
        }

        public IConfigurationSection GetSection(string key)
        {
            return _baseConfiguration.GetSection(key);
        }

        public string? this[string key]
        {
            get => _baseConfiguration[key];
            set => _baseConfiguration[key] = value;
        }

        public string Key => _baseConfiguration.Key;

        public string Path => _baseConfiguration.Path;

        public string Value { get => _baseConfiguration.Value; set => _baseConfiguration.Value = value; }
    }
}
