using System;
using System.Collections.Generic;

namespace SolarCoffee.Data.Dtos
{
    public class SnapshotResponseDto
    {
        public List<SnapshotHistoryDto> ListOfSnapshotHistoryDto { get; set; }
        public List<DateTime> Timeline { get; set; }
    }
}