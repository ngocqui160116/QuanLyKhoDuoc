﻿
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
        public int IdUnit { get; set; }
        public int Count { get; set; }
        public double InputPrice { get; set; }
        public double Total { get; set; }
        public DateTime DueDate { get; set; }

        //Add
        public string MedicineName { get; set; }
    }
}
