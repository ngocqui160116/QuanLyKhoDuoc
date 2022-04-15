using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Shared.StockInfo
{
    public class StockInfoDto
    {
        public int Id { get; set; }
        public int Stock_Id { get; set; }
        public int Inventory_Id { get; set; }
        public int Medicine_Id{ get; set; }
        public int Amount { get; set; }
        public int ActualAmount { get; set; }

        //Mapper
        public string MedicineName { get; set; }
        public int Batch { get; set; }
        public string UnitName { get; set; }
    }
}
