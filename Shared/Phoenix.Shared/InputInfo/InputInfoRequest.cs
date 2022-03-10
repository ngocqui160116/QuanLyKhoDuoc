using System;

namespace Phoenix.Shared.InputInfo
{
    public class InputInfoRequest
    {
        public string IdInput { get; set; }
        public int IdMedicine { get; set; }
        public int IdSupplier { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public DateTime DueDate { get; set; }
    }
}
