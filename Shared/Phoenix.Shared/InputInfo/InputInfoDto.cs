using System;

namespace Phoenix.Shared.InputInfo
{
    public class InputInfoDto
    {
        public string IdInput{ get; set; }
       // public int IdMedicine { get; set; }
        public string NameMedicine { get; set; }
        public string NameSupplier { get; set; }
        //public int IdSupplier { get; set; }
        public string IdBatch { get; set; }
        public int Count { get; set; }
        public double InputPrice { get; set; }
        public double OutputPrice { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public DateTime DueDate { get; set; }
        public string SDK { get; set; }
        public string Status { get; set; }
    }
}
