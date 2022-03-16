using Phoenix.Shared.Common;
using System;

namespace Phoenix.Shared.Medicine
{
    public class MedicineRequest : BaseRequest
    {
        public int IdMedicine { get; set; }
        public string RegistrationNumber { get; set; }
        public string Name { get; set; }
        public int IdGroup { get; set; }
        public string Active { get; set; }
        public string Content { get; set; }
        public string Packing { get; set; }
        public int IdUnit { get; set; }
        public int? Image { get; set; }
        public string Status { get; set; }
    }
}
