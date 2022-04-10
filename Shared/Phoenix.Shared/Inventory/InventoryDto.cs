using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Shared.Inventory
{
    public class InventoryDto
    {
        public int Id { get; set; }
        public int IdMedicine { get; set; }
        public int Count { get; set; }
        public int? IdInputInfo { get; set; }
    //
        public string MedicineName { get; set; }
        public int LotNumber { get; set; }
        public double UnitPrice { get; set; }
        public DateTime HSD { get; set; }
    }
}
