const { ApolloServer } = require('apollo-server');
const { ApolloGateway, RemoteGraphQLDataSource } = require('@apollo/gateway');
const { readFileSync } = require('fs');
const { ApolloServerPluginLandingPageGraphQLPlayground } = require('apollo-server-core');
const responseCachePlugin = require('apollo-server-plugin-response-cache');

const supergraphSdl = readFileSync('./supergraph.graphql').toString();

class AuthenticatedDataSource extends RemoteGraphQLDataSource {
  willSendRequest({ request, context }) {
    const headers=context;
    //console.log(context.req);
    // Pass the user's id from the context to each subgraph
    // as a header called `user-id`
    for (const key in headers) {
      const value = headers[key];
      if (value) {
          request.http?.headers.set(key, String(value));
      }
  }      
    request.http.headers.set('X-ContextUser-Id', context.userId);
  }
}

const gateway = new ApolloGateway({
  supergraphSdl,
  buildService({ name, url }) {
    return new AuthenticatedDataSource({ url });
  },
  buildService({ name, url }) {
    return new RemoteGraphQLDataSource({
      url,
      honorSubgraphCacheControlHeader: name === "accounts" ? false : true
    });
  }
});

const server = new ApolloServer({
  gateway,

  context: ({ req }) => {
    // Get the user token from the headers
    const token = req.headers || '';
    //console.log(req.headers);
    // Try to retrieve a user with the token
    const userId = "Vijay";//getUserId(token);
    // Add the user ID to the context
    return token ;
  },
  plugins: [
    ApolloServerPluginLandingPageGraphQLPlayground({
      // options
    }),
    responseCachePlugin
  ]
});

const hostname = 'localhost';
const port = 4000;
// The `listen` method launches a web server.
//server.listen().then(({ url }) => {
 // console.log(`🚀  Server ready at ${url}`);
//})

server.listen(port, hostname, () => {
  console.log(`Server running at http://${hostname}:${port}/`);
});