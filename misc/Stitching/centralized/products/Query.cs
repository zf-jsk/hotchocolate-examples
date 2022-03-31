using System;
using System.Collections.Generic;
using HotChocolate;

namespace Demo.Products
{
    public class Query
    {
        public IEnumerable<Product> GetTopProducts(
            int first, 
            [Service] ProductRepository repository) =>
            repository.GetTopProducts(first);

        public Product GetProduct(
            int upc, 
            [Service] ProductRepository repository) =>
            repository.GetProduct(upc);
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