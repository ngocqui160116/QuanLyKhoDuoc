using Phoenix.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Shared.MedicineItem
{
    public class MedicineItemRequest : BaseRequest
    {
        public int Id { get; set; }
        public int Medicine_Id { get; set; }
        public string MedicineName { get; set; }
        public int Batch { get; set; }
        public int Count { get; set; }
        public double InputPrice { get; set; }
        public double Total { get; set; }
        public DateTime DueDate { get; set; }
        public string UnitName { get; set; }
        public int? Inventory_Id { get; set; }
        public int Amount { get; set; }
    }
}
