using Phoenix.Shared.Common;
using System;

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
    }
}
