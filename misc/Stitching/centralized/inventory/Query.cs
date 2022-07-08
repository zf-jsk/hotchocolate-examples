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
        public IEnumerable<Product> GetTopProducts(
           int first,
           [Service] InventoryInfoRepository repository) =>
           repository.GetTopProducts(first);

        //[UseGQLResponseCache] 
        public Product GetProduct(
            int upc,
            [Service] InventoryInfoRepository repository)
        {
            return repository.GetProduct(upc);
        }
         
        //public IEnumerable<Product> GetProducts(
        //    Product product,
        //    [Service] InventoryInfoRepository repository)
        //{
        //    return repository.GetTopProducts(3);
        //}
    }


    public class SubQuery
    {
        public SubGraph Get_service() => new SubGraph();
    }

    public class SubGraph
    {
        public String sdl { get; set; } = System.IO.File.ReadAllText("./inventory.graphql");
    }
}