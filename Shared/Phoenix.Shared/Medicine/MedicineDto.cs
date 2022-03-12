using System;

namespace Phoenix.Shared.Medicine
{
    public class MedicineDto
    {
        public int IdMedicine { get; set; }
        public string RegistrationNumber { get; set; }
        public string Name { get; set; }
        public int IdGroup { get; set; }
        public string Active { get; set; }
        public string Content { get; set; }
        public string Packing { get; set; }
        public int IdSupplier { get; set; }
        public int IdUnit { get; set; }
        public int Amount { get; set; }
        public int? Image { get; set; }
        public string Status { get; set; }
        public string SupplierName { get; set; }
        public string GroupName { get; set; }
    }
}
