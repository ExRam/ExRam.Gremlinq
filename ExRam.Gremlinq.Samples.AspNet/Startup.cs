using ExRam.Gremlinq.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ExRam.Gremlinq.Core.AspNet;
using ExRam.Gremlinq.Samples.Shared;

namespace ExRam.Gremlinq.Samples.AspNet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddGremlinq(setup =>
                {
                    setup
                        .UseCosmosDb()
                        .UseModel(GraphModel
                            .FromBaseTypes<Vertex, Edge>(lookup => lookup
                                .IncludeAssembliesOfBaseTypes())
                            //For CosmosDB, we exclude the 'PartitionKey' property from being included in updates.
                            .ConfigureProperties(model => model
                                .ConfigureElement<Vertex>(conf => conf
                                    .IgnoreOnUpdate(x => x.PartitionKey))));
                })
                .AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
