const _ = require('lodash');
const { ApolloServer } = require('apollo-server');
const typeDefs = require('./type-defs');
const resolvers = require('./resolvers');
const createLoaders = require('./data-loaders');
const responseCachePlugin = require('apollo-server-plugin-response-cache');

const server = new ApolloServer({
  // hello query is indeeed cached if you comment default cache control.
  // cacheControl: {
  //   defaultMaxAge: 15,
  // },
  context: async ({ req }) => {
    // Assuming any client is authenticated.
    const authorization = req.headers.authorization || '';
    const token = _.replace(authorization, 'Bearer ', '');

    // console.info('TOKEN::: ', token);

    // This object will be available to each resolver.
    return {
      isAuth: Boolean(true),
      loaders: createLoaders(),
      req,
      membershipId: _.head(_.split(token, ':')),
      token
    };
  },
  typeDefs,
  resolvers,
  plugins: [responseCachePlugin()]
});

const hostname = 'localhost';
const port = 4051;

server.listen().then(({ url }) => {
  // console.clear();
   console.log(`Server running at http://${hostname}:${port}/`);
});
