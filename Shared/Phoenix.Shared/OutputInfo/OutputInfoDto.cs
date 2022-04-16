using Phoenix.Shared.InputInfo;
using System;
using System.Collections.Generic;

namespace Phoenix.Shared.OutputInfo
{
    public class OutputInfoDto
    {
        public int Id { get; set; }
        public int IdOutput { get; set; }
        public int IdMedicine { get; set; }
       
        public int Count { get; set; }
        public double Total { get; set; }
        public int? Inventory_Id { get; set; }
        public double OutputPrice { get; set; }
        //add
        public string MedicineName { get; set; }
        public int Batch { get; set; }
        public DateTime DueDate { get; set; }
    }
}
