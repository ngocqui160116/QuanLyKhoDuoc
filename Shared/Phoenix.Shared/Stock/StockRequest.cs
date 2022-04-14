using Phoenix.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Shared.Inventory
{
    public class StockRequest : BaseRequest
    {
        public int Id { get; set; }
        public int IdStaff { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
    }
}
