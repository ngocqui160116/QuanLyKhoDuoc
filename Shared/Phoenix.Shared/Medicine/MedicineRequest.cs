using System;

namespace Phoenix.Shared.Medicine
{
    public class MedicineRequest
    {
        public int IdMedicine { get; set; }
        public string RegistrationNumber { get; set; }
        public string Name { get; set; }
        public int IdGroup { get; set; }
        public int IdCustomer { get; set; }
        public int Unit { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
    }
}
