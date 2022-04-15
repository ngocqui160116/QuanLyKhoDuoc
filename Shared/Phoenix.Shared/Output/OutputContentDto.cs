using Phoenix.Shared.OutputInfo;
using System;
using System.Collections.Generic;

namespace Phoenix.Shared.Output
{
    public class OutputContentDto
    {
        
        public int medicineId { get; set; }
        public int InventoryCount { get; set; }
        public int LotNumber { get; set; }
        public int Count { get; set; }
    }
}
