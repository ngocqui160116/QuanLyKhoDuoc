using System;

namespace Phoenix.Shared.OutputInfo
{
    public class OutputInfoRequest
    {
        public string IdOutput { get; set; }
        public int IdMedicine { get; set; }
        public int IdCustomer { get; set; }
        public string Status { get; set; }
    }
}
