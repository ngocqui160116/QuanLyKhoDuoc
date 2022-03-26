
using System;
using System.Collections.ObjectModel;

namespace Phoenix.Shared.InputInfo
{
    public class InputInfoDto
    {
        public int Id { get; set; }
        public string IdInput{ get; set; }
        public int IdMedicine { get; set; }

        public string IdBatch { get; set; }
        public int Count { get; set; }
        public double InputPrice { get; set; }
        public double Total { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }

        //Add

        public string InputId { get; set; }
        public string SDK { get; set; }
        public string MedicineName { get; set; }
        public string DateInput { get; set; }
    }
}
