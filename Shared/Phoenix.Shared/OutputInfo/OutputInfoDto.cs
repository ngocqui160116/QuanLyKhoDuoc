using System;

namespace Phoenix.Shared.OutputInfo
{
    public class OutputInfoDto
    {
        public string IdOutput { get; set; }
        public int IdMedicine { get; set; }
        public int IdCustomer { get; set; }
        public int Count { get; set; }
        public string Status { get; set; }
        public string MedicineName { get; set; }
        public string CustomerName { get; set; }
    }
}
