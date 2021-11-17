﻿using System;
using System.Linq.Expressions;

using ExRam.Gremlinq.Core.Models;
using ExRam.Gremlinq.Providers.CosmosDb;
using Microsoft.Extensions.Configuration;

namespace ExRam.Gremlinq.Core.AspNet
{
    public static class GremlinqSetupExtensions
    {
        public static GremlinqSetup UseCosmosDb(this GremlinqSetup setup, Func<ICosmosDbConfigurator, IConfiguration, ICosmosDbConfigurator>? extraConfiguration = null)
        {
            return setup 
                .UseProvider<ICosmosDbConfigurator>(
                    "CosmosDb",
                    (source, configuratorTransformation) => source.UseCosmosDb(configuratorTransformation),
                    (configurator, configuration) =>
                    {
                        if (configuration["Database"] is { } databaseName)
                            configurator = configurator.OnDatabase(databaseName);

                        if (configuration["Graph"] is { } graphName)
                            configurator = configurator.OnGraph(graphName);

                        if (configuration["AuthKey"] is { } authKey)
                            configurator = configurator.AuthenticateBy(authKey);

                        return extraConfiguration?.Invoke(configurator, configuration) ?? configurator;
                    });
        }

        public static GremlinqSetup UseCosmosDb<TVertex, TEdge>(this GremlinqSetup setup, Expression<Func<TVertex, object>> partitionKeyExpression, Func<ICosmosDbConfigurator, IConfiguration, ICosmosDbConfigurator>? extraConfiguration = null)
        {
            return setup
                .UseCosmosDb(extraConfiguration)
                .ConfigureEnvironment(env => env
                    .UseModel(GraphModel
                        .FromBaseTypes<TVertex, TEdge>(lookup => lookup
                            .IncludeAssembliesOfBaseTypes())
                        .ConfigureProperties(model => model
                            .ConfigureElement<TVertex>(conf => conf
                                .IgnoreOnUpdate(partitionKeyExpression)))));
        }
    }
}
