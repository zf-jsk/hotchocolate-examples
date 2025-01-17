using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo.Inventory
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<InventoryInfoRepository>()
                .AddGraphQLServer()
                .AddQueryType<Query>();

            services
               .AddGraphQLServer("subgraph")
               .AddQueryType<SubQuery>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL($"/subgraph-schema", "subgraph")
                      .WithOptions(
                       new GraphQLServerOptions
                       {
                           AllowedGetOperations = AllowedGetOperations.Query,
                           EnableGetRequests = true,
                           EnableMultipartRequests = false,
                           EnableSchemaRequests = true,

                       }
                      );
                endpoints.MapGraphQL();
            });
        }
    }
}
