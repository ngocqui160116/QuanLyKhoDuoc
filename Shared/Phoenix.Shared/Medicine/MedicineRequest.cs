using System;

namespace Phoenix.Shared.Medicine
{
    public class MedicineRequest
    {
        public string RegistrationNumber { get; set; }
        public string Name { get; set; }
        public int IdGroup { get; set; }
        public int IdSupplier { get; set; }
        public int IdUnit { get; set; }
        public string Status { get; set; }
    }
}
