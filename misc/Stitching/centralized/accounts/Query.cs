using System;
using System.Collections.Generic;
using HotChocolate;

namespace Demo.Accounts
{
    public class Query
    {
        public IEnumerable<User> GetUsers([Service] UserRepository repository) =>
            repository.GetUsers();

        public User GetUser(int id, [Service] UserRepository repository) => 
            repository.GetUser(id);
    }

    public class SubQuery
    {
        public SubGraph Get_service() => new SubGraph();
    }

    public class SubGraph
    {
        public String sdl { get; set; } = @"schema {
  query: Query
    }

    type Product
    {
        upc: Int!
  name: String!
  price: Int!
  weight: Int!
}

    type Query
    {
        topProducts(first: Int!): [Product!]!
  product(upc: Int!): Product!
}";
    }
}