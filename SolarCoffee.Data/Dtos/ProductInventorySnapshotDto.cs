using System;

namespace SolarCoffee.Data.Dtos
{
    public class ProductInventorySnapshotDto
    {
        public DateTime SnapshotTime { get; set; }
        public int QuantityOnHand { get; set; }
        public ProductDto Product { get; set; }
    }
}