using System;

namespace Demo.Inventory
{
    public record InventoryInfo(int Upc, bool IsInStock);
    public record Product(int Upc, string Name, int Price, int Weight);
}