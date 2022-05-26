using Demo.Data;
using Demo.Resolvers;
using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

builder.Services
            .AddSingleton<PersonRepository>()
            .AddGraphQLServer()
            .AddResolver<Query>()
            .AddHttpQueryCache()
            .UseQueryCachePipeline()
            .AddDocumentFromString(@"
        type Query {
            users: [User!] @cacheControl(maxAge: 6000 scope: PUBLIC)
        }
type User  {
  birthdate: DateTime!
  id: Int!
  name: String!
  username: String!
}

    ")
            //.AddDocumentFromFile("./Schema.graphql")
            .ModifyCacheControlOptions(options => { });

WebApplication? app = builder.Build();


app.MapGraphQL();

app.Run();
