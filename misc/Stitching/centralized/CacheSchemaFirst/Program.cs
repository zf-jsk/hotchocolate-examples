using Demo.Data;
using Demo.Resolvers;
using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

builder.Services
            .AddSingleton<PersonRepository>()
            .AddGraphQLServer()
            .AddDocumentFromFile("./Schema.graphql")
            .AddResolver<Query>()
            .AddHttpQueryCache()
            .UseQueryCachePipeline()
            .ModifyCacheControlOptions(options => {  });

WebApplication? app = builder.Build();


app.MapGraphQL();

app.Run();
