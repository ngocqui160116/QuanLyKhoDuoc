using Phoenix.Shared.Common;
using System;

namespace Phoenix.Shared.OutputInfo
{
    public class OutputInfoRequest : BaseRequest
    {
        public int IdOutput { get; set; }
        public int IdMedicine { get; set; }
        public int IdInputInfo { get; set; }
        public int Count { get; set; }
        public double Total { get; set; }
        public string Status { get; set; }
        public int IdBatch { get; set; }
        public int? Inventory_Id { get; set; }
        public double OutputPrice { get; set; }
        //Output
        public int IdStaff { get; set; }
        public int IdReason { get; set; }
        public DateTime DateOutput { get; set; }
    }
}
