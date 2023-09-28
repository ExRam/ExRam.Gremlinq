﻿using System;
using System.Threading.Tasks;
using ExRam.Gremlinq.Core;
using ExRam.Gremlinq.Core.Models;
using ExRam.Gremlinq.Providers.Core;
using static ExRam.Gremlinq.Core.GremlinQuerySource;

namespace ExRam.Gremlinq.Templates.Console
{
    public class Program
    {
        private readonly IGremlinQuerySource _g;

        public Program()
        {
            _g = g
#if (provider == "GremlinServer")
                .UseGremlinServer(configurator => configurator
                    .AtLocalhost())
#elif (provider == "Neptune")
                .UseNeptune(configurator => configurator
                    .At(new Uri("wss://your.neptune.endpoint/")))
#elif (provider == "CosmosDb")
                .UseCosmosDb(configurator => configurator
                    .At(new Uri("wss://your.cosmosdb.endpoint/"))
                    .OnDatabase("your database name")
                    .OnGraph("your graph name")
                    .AuthenticateBy("your auth key"))
#elif (provider == "JanusGraph")
                .UseJanusGraph(configurator => configurator
                    .AtLocalhost())
#endif
                .ConfigureEnvironment(env => env
                    .UseNewtonsoftJson()
                    .UseModel(GraphModel
                        .FromBaseTypes<Vertex, Edge>(lookup => lookup
#if (provider == "CosmosDb")                
                            .IncludeAssembliesOfBaseTypes())
                        //For CosmosDB, we exclude the 'PartitionKey' property from being included in updates.
                        .ConfigureProperties(model => model
                            .ConfigureElement<Vertex>(conf => conf
                                .IgnoreOnUpdate(x => x.PartitionKey)))));
#else
                            .IncludeAssembliesOfBaseTypes())));
#endif
        }

        public async Task Run()
        {
            var petCount = await _g
                .V<Pet>()
                .Count()
                .FirstAsync();

            /***** Uncomment to actually add Jenny and her cat to the database
            var jenny = await _g
                .AddV(new Person { Name = "Jenny", Age = 41 })
                .SideEffect(__ => __
                    .AddE<Owns>()
                    .To(__ => __
                        .AddV(new Cat() { Name = "Catman John" })))
                .FirstAsync();
            ********/

            System.Console.WriteLine($"There are {petCount} pets in the database.");
        }

        private static async Task Main()
        {
            var program = new Program();

            await program.Run();

            System.Console.Write("Press any key...");
            System.Console.Read();
        }
    }
}
