using System;
using System.Collections.Generic;
using HotChocolate;

namespace Demo.Inventory
{
    public class Query
    {
        public InventoryInfo GetInventoryInfo(
            int upc, 
            [Service] InventoryInfoRepository repository) =>
            repository.GetInventoryInfo(upc);

        public double GetShippingEstimate(int price, int weight) =>
            price > 1000 ? 0 : weight * 0.5;
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