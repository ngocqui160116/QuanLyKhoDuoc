﻿

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
        
       
        //add
        public string MedicineName { get; set; }
        public string Batch { get; set; }

        public DateTime DueDate { get; set; }
        public double InputPrice { get; set; }
    }
}
