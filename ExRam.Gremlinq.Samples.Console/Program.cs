﻿#define GremlinServer
//#define CosmosDB
//#define AWSNeptune
//#define JanusGraph

using System;
using System.Threading.Tasks;
using ExRam.Gremlinq.Core;
using ExRam.Gremlinq.Core.Models;
using ExRam.Gremlinq.Providers.WebSocket;
using ExRam.Gremlinq.Samples.Shared;
using Microsoft.Extensions.Logging;

// Put this into static scope to access the default GremlinQuerySource as "g". 
using static ExRam.Gremlinq.Core.GremlinQuerySource;

namespace ExRam.Gremlinq.Samples
{
    public class Program
    {
        private static async Task Main()
        {
            var gremlinQuerySource = g
                .ConfigureEnvironment(env => env //We call ConfigureEnvironment twice so that the logger is set on the environment from now on.
                    .UseLogger(LoggerFactory
                        .Create(builder => builder
                            .AddFilter(__ => true)
                            .AddConsole())
                        .CreateLogger("Queries")))
                .ConfigureEnvironment(env => env
                    .UseModel(GraphModel
                        .FromBaseTypes<Vertex, Edge>(lookup => lookup
                            .IncludeAssembliesOfBaseTypes())
                        //For CosmosDB, we exclude the 'PartitionKey' property from being included in updates.
                        .ConfigureProperties(model => model
                            .ConfigureElement<Vertex>(conf => conf
                                .IgnoreOnUpdate(x => x.PartitionKey))))
                    //Disable query logging for a noise free console output.
                    //Enable logging by setting the verbosity to anything but None.
                    .ConfigureOptions(options => options
                        .SetValue(GremlinqOption.QueryLogVerbosity, QueryLogVerbosity.IncludeBindings)))

#if GremlinServer
                .UseGremlinServer(configurator => configurator
                    .AtLocalhost());
#elif AWSNeptune
                .UseNeptune(configurator => configurator
                    .AtLocalhost());
#elif CosmosDB
                .UseCosmosDb(configurator => configurator
                    .At(new Uri("wss://your_gremlin_endpoint.gremlin.cosmos.azure.com:443/"))
                    .OnDatabase("your database name")
                    .OnGraph("your graph name")
                    .AuthenticateBy("your auth key"));
#elif JanusGraph
                .UseJanusGraph(configurator => configurator
                    .AtLocalhost());
#endif

            await new Logic(gremlinQuerySource, Console.Out)
                .Run();

            Console.Write("Press any key...");
            Console.Read();
        }
    }
}
