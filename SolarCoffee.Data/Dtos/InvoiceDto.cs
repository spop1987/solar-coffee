using System;
using System.Collections.Generic;

namespace SolarCoffee.Data.Dtos
{
    public class InvoiceDto
    {
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int CustomerId { get; set; }
        public List<SalesOrderItemDto> LineItems { get; set; }
    }
}