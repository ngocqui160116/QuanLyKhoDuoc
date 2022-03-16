using Phoenix.Shared.Common;
using System;

namespace Phoenix.Shared.InputInfo
{
    public class InputInfoRequest : BaseRequest
    {
        public string IdInput { get; set; }
        public int IdMedicine { get; set; }
        public int IdSupplier { get; set; }
        public string IdBatch { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public DateTime DueDate { get; set; }
        //public string MedicineName { get; set; }
    }
}
