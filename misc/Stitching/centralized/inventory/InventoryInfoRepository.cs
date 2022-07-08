using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Inventory
{
    public class InventoryInfoRepository
    {
        private readonly Dictionary<int, InventoryInfo> _infos;
        private readonly Dictionary<int, Product> _products;

        
        public IEnumerable<Product> GetTopProducts(int first) =>
            _products.Values.OrderBy(t => t.Upc).Take(first);

        public Product GetProduct(int upc) => _products[upc];
        public InventoryInfoRepository()
        {
            _infos = new InventoryInfo[]
            {
                new InventoryInfo(1, true),
                new InventoryInfo(2, false),
                new InventoryInfo(3, true)
            }.ToDictionary(t => t.Upc);
            _products = new Product[]
           {
                new Product(1, "Table", 899, 100),
                new Product(2, "Couch", 1299, 1000),
                new Product(3, "Chair", 54, 50)
           }.ToDictionary(t => t.Upc);
        }

        public InventoryInfo GetInventoryInfo(int upc) => _infos[upc];
    }
}