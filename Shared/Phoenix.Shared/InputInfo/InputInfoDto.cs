using System;

namespace Phoenix.Shared.InputInfoDto
{
    public class InputInfoDto
    {
        public string IdInput{ get; set; }
        public int IdMedicine { get; set; }
        public int Count { get; set; }
        public double InputPrice { get; set; }
        public double OutputPrice { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
    }
}
