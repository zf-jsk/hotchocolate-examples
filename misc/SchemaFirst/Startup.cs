using Demo.Data;
using Demo.Resolvers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Services
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<PersonRepository>()
                .AddGraphQLServer()
                .AddDocumentFromFile("./Schema.graphql")
                .AddResolver<Query>(); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           // if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting(); 
        }
    }
}

//    var builder = Host.CreateDefaultBuilder(args);

//    builder.Services
//        .AddSingleton<PersonRepository>()
//    .AddGraphQLServer()
//    .AddDocumentFromFile("./Schema.graphql")
//    .AddResolver<Query>();


