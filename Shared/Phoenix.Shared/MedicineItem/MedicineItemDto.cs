using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Shared.MedicineItem
{
    public class MedicineItemDto
    {
        //public int Id { get; set; }
        //public int Inputinfo_Id { get; set; }
        //public int LotNumber { get; set; }

        public int Id { get; set; }
        public int Medicine_Id { get; set; }
        public string MedicineName { get; set; }
        public string RegistrationNumber { get; set; }
        public int? Batch { get; set; }
        public int? Count { get; set; }
        public double? InputPrice { get; set; }
        public double? Total { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
