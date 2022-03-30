using Phoenix.Shared.Common;
using System;

namespace Phoenix.Shared.InputInfo
{
    public class InputInfoRequest : BaseRequest
    {
        public int Id { get; set; }
        public string IdInput { get; set; }
        public int IdMedicine { get; set; }
        public string IdBatch { get; set; }
        public int Count { get; set; }
        public double InputPrice { get; set; }
        public double Total { get; set; }
        public DateTime DueDate { get; set; }
        public bool Status { get; set; }
        //Input
        public int IdStaff { get; set; }
        public int IdSupplier { get; set; }
        public DateTime DateInput { get; set; }
    }
}
