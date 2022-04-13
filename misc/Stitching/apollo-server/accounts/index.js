const { ApolloServer, gql } = require('apollo-server');
const { ApolloServerPluginCacheControl } = require("apollo-server-core");
const { ApolloServerPluginLandingPageGraphQLPlayground } = require('apollo-server-core');

const responseCachePlugin = require('apollo-server-plugin-response-cache');
// A schema is a collection of type definitions (hence "typeDefs")
// that together define the "shape" of queries that are executed against
// your data.

const usersData = [
  {
    name: 'Ada Lovelace',
    username: '@ada',
	id:'1',
	birthdate:'1815-12-10T00:00:00.000+05:30'
  },
   {
    name: 'Alan Turing',
    username: '@complete',
	id:'2',
	birthdate:'1912-06-23T00:00:00.000+05:30'
  },
   {
    name: 'Vijayakumar K',
    username: '@vijay',
	id:'3',
	birthdate:'1815-12-10T00:00:00.000+05:30'
  }
];

const typeDefs = gql`
  schema {
  query: Query
}
enum CacheControlScope {
PUBLIC
PRIVATE
}

directive @cacheControl(
maxAge: Int
scope: CacheControlScope
inheritMaxAge: Boolean
) on FIELD_DEFINITION | OBJECT | INTERFACE | UNION


type Query {
  users: [User]	@cacheControl(maxAge: 600, scope: PUBLIC)
  user(id: Int): User @cacheControl(maxAge: 600, scope: PUBLIC)
}

type User {
  id: Int! @cacheControl(maxAge: 30)
  name: String @cacheControl(maxAge: 30)
  birthdate: DateTime!
  username: String!
}

"The @defer directive may be provided for fragment spreads and inline fragments to inform the executor to delay the execution of the current fragment to indicate deprioritization of the current fragment. A query with @defer directive will cause the request to potentially return multiple responses, where non-deferred data is delivered in the initial response and data deferred is delivered in a subsequent response. @include and @skip take precedence over @defer."
directive @defer("If this argument label has a value other than null, it will be passed on to the result of this defer directive. This label is intended to give client applications a way to identify to which fragment a deferred result belongs to." label: String "Deferred when true." if: Boolean) on FRAGMENT_SPREAD | INLINE_FRAGMENT

"The @specifiedBy directive is used within the type system definition language to provide a URL for specifying the behavior of custom scalar definitions."
directive @specifiedBy("The specifiedBy URL points to a human-readable specification. This field will only read a result for scalar types." url: String!) on SCALAR

"The @stream directive may be provided for a field of List type so that the backend can leverage technology such as asynchronous iterators to provide a partial list in the initial response, and additional list items in subsequent responses. @include and @skip take precedence over @stream."
directive @stream("If this argument label has a value other than null, it will be passed on to the result of this stream directive. This label is intended to give client applications a way to identify to which fragment a streamed result belongs to." label: String "The initial elements that shall be send down to the consumer." initialCount: Int! = 0 "Streamed when true." if: Boolean) on FIELD

"The DateTime scalar represents an ISO-8601 compliant date time type."
scalar DateTime @specifiedBy(url: "https:\/\/www.graphql-scalars.com\/date-time")
`;

const resolvers = {
  Query: {
	  
    users: (info)=>{
		console.log(info);
		//info.cacheControl.setCacheHint({ maxAge: 60, scope: 'PUBLIC' });
     
		 console.log("application called");
		return usersData;
	},
	 user: (arg1, { id }, arg2, info)=>{
		 console.log(id);
		 //console.log(info);
		info.cacheControl.setCacheHint({ maxAge: 600, scope: 'PUBLIC' });
		 //return find(usersData, { id });
		  console.log(info.cacheControl); 
	  var newArray = usersData.filter(function (el)
{
  return el.id ==id;
}
);
console.log(newArray);
		return newArray[0];
	}
  },
};
/*
const server = new ApolloServer(
			{ 	typeDefs, 
				resolvers ,
        cacheControl:{defaultMaxAge:20},
				plugins: [ 
							ApolloServerPluginLandingPageGraphQLPlayground(
												{ 
													// options
												}),
												ApolloServerPluginCacheControl({ defaultMaxAge: 5 }),
							ApolloServerPluginCacheControl({
      // Cache everything for 1 second by default.
      defaultMaxAge: 1000,
      // Don't send the `cache-control` response header.
      calculateHttpHeaders: false,
    })],});
 
*/

const server = new ApolloServer({
  typeDefs,
  resolvers,
  plugins: [
	ApolloServerPluginLandingPageGraphQLPlayground(
												{ 
													// options
												}),
    ApolloServerPluginCacheControl({
      // Cache everything for 1 second by default.
      defaultMaxAge: 1000,
      // Don't send the `cache-control` response header.
     // calculateHttpHeaders: false,
    }),
	responseCachePlugin
  ],
});

;

const hostname = 'localhost';
const port = 4051;
// The `listen` method launches a web server.
//server.listen().then(({ url }) => {
 // console.log(`🚀  Server ready at ${url}`);
//})

server.listen(port, hostname, () => {
  console.log(`Server running at http://${hostname}:${port}/`);
});