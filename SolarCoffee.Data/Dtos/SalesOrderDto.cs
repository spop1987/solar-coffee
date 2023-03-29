using System;
using System.Collections.Generic;

namespace SolarCoffee.Data.Dtos
{
    public class SalesOrderDto
    {
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public CustomerDto Customer { get; set; }
        public List<SalesOrderItemDto> SalesOrderItems { get; set; }
        public bool IsPaid { get; set; }
    }
}