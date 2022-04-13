const { gql } = require("apollo-server");

const typeDefs = gql`
  ####### Model types #######
  type Product {
    id: ID!
    name: String!
  }

  type Member @cacheControl(maxAge: 15) {
    memberId: ID!
    firstName: String!
    lastName: String!
    relationship: String
    dateOfBirth: String
  }

  type Membership {
    id: ID!
    firstName: String!
    lastName: String!
    state: String
    members: [Member]
  }

  type AuthPayLoad {
    access_token: String
    expires_in: Int
    membership: Membership # Note: This is where clients can request data for Home page
  }

  type HelloResponse @cacheControl(maxAge: 10) {
    value: String
  }

  ####### END Model types #######

  ####### Input types #######
  input UserLoginInput {
    memberId: String
    password: String
  }
  ####### END Input types #######

  type Query {
    hello: HelloResponse
    membership: Membership
  }

  type Mutation {
    loginUser(input: UserLoginInput): AuthPayLoad!
  }
`;

module.exports = typeDefs;
