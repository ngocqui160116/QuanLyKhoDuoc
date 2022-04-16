using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Shared.Stock
{
    public class StockContentDto
    {
        public int medicineId { get; set; }
        public int LotNumber { get; set; }
        public int Count { get; set; }
        public string Note { get; set; }
    }
}
