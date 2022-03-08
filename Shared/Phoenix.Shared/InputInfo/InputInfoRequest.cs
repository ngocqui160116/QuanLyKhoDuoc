using System;

namespace Phoenix.Shared.InputInfo
{
    public class InputInfoRequest
    {
        public string IdInputInfo { get; set; }
        public int IdMedicine { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public DateTime DueDate { get; set; }
    }
}
