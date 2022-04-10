
using System;
using System.Collections.ObjectModel;

namespace Phoenix.Shared.StockInfo
{
    public class StockInfoDto
    {
        public int Id { get; set; }
        public int Stock_Id { get; set; }

        public int Inventory_Id { get; set; }
        public int Medicine_Id { get; set; }
        public int Amount { get; set; }
        public int ActualAmount { get; set; }
    }
}
