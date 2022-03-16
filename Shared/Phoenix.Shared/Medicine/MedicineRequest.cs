using Phoenix.Shared.Common;
using System;

namespace Phoenix.Shared.Medicine
{
    public class MedicineRequest : BaseRequest
    {
        public string RegistrationNumber { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
