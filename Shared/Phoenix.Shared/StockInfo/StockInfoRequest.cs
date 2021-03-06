using Phoenix.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Shared.StockInfo
{
    public class StockInfoRequest : BaseRequest
    {
        public int Id { get; set; }
        public int Stock_Id { get; set; }
        public int Inventory_Id { get; set; }
        public int Medicine_Id { get; set; }
        public int Amount { get; set; }
        public int ActualAmount { get; set; }

        // Stock
        public int IdStaff { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
    }
}
