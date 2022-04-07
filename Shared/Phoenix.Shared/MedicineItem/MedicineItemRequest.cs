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
        //public int Id { get; set; }
        public int Inputinfo_Id { get; set; }
        //public int LotNumber { get; set; }

        public int Id { get; set; }
       
        public int MedicineId { get; set; }
        public string Name { get; set; }
        public int IdSupplier { get; set; }
        public string SupplierName { get; set; }
    }
}
