using Demo.Data;
using Demo.Resolvers;
using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

builder.Services
            .AddSingleton<PersonRepository>()
            .AddGraphQLServer()
            .AddResolver<Query>()
            .AddDocumentFromString(@"
        type Query {
            users: [User!] @cacheControl(maxAge: 6000 scope: PRIVATE)
        }
type User  {
  birthdate: DateTime!
  id: Int!
  name: String!
  username: String!
}

    ")
                .UseQueryCachePipeline()
                .AddHttpQueryCache();
                //.ModifyCacheControlOptions(o => o.ApplyDefaults ); 

WebApplication? app = builder.Build();


app.MapGraphQL();

app.Run();
