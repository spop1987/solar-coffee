using System.Collections.Generic;

namespace SolarCoffee.Data.Dtos
{
    public class SnapshotHistoryDto
    {
        public List<int> QuantityOnHand { get; set; }
        public int ProductId { get; set; }
    }
}