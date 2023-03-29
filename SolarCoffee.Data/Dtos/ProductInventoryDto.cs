using System;

namespace SolarCoffee.Data.Dtos
{
    public class ProductInventoryDto
    {
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int QuantityOnHand { get; set; }
        public int IdealQuantity { get; set; }
        public ProductDto Product { get; set; }
    }
}