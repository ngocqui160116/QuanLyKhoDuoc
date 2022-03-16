using Phoenix.Shared.Common;
using System;

namespace Phoenix.Shared.OutputInfo
{
    public class OutputInfoRequest : BaseRequest
    {
        public string IdOutput { get; set; }
        public int IdMedicine { get; set; }
        public string IdInputInfo { get; set; }
        public int IdReason { get; set; }
        public int Count { get; set; }
        public double Total { get; set; }
        public string Status { get; set; }
    }
}
