using Phoenix.Shared.Common;
using System;

namespace Phoenix.Shared.OutputInfo
{
    public class OutputInfoRequest : BaseRequest
    {
        public string IdOutput { get; set; }
        public int IdMedicine { get; set; }
        public int IdInputInfo { get; set; }
        public int IdReason { get; set; }
        public int Count { get; set; }
        public double Total { get; set; }
        public bool Status { get; set; }

        public string Id { get; set; }
        public int IdStaff { get; set; }
        public DateTime DateOutput { get; set; }
    }
}
