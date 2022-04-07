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
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }
        public string GroupName { get; set; }
    }
}
