﻿using System.Diagnostics.CodeAnalysis;

using ExRam.Gremlinq.Core.Transformation;

namespace ExRam.Gremlinq.Core
{
    internal static class TransformerInternalExtensions
    {
        private sealed class IncompleteTransformer : ITransformer
        {
            private readonly ITransformer _transformer;

            public IncompleteTransformer(ITransformer transformer)
            {
                _transformer = transformer;
            }

            public ITransformer Add(IConverterFactory converterFactory) => _transformer
                .Add(converterFactory);

            public bool TryTransform<TSource, TTarget>(TSource source, IGremlinQueryEnvironment environment, [NotNullWhen(true)] out TTarget? value) => throw new InvalidOperationException("""
                No deserializer configured!
                In Gremlinq v12, query result deserialization has been decoupled from the core library.
                To keep using Newtonsoft.Json as Json-deserialization mechanism, add a reference to
                ExRam.Gremlinq.Support.NewtonsoftJson (or ExRam.Gremlinq.Support.NewtonsoftJson.AspNet on ASP.NET Core)
                and call 'UseNewtonsoftJson()' in the provider configuration.
               
                Examples:
               
                Provider configuration
               
                    IGremlinQuerySource g = ...
               
                    g = g.UseCosmosDb(c => c
                            .UseNewtonsoftJson());
               
                ASP.NET Core
               
                    IServiceCollection services = ...
               
                    services.AddGremlinq(setup => setup
                        .UseCosmosDb(providerSetup => providerSetup
                            .UseNewtonsoftJson()));
               
                Manual configuration
               
                    IGremlinQuerySource g = ...
               
                    g = g.ConfigureEnvironment(env => env
                        .UseNewtonsoftJson());
                
                """);
        }

        public static ITransformer AsIncomplete(this ITransformer transformer) => new IncompleteTransformer(transformer);
    }
}
