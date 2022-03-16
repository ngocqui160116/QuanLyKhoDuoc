﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phoenix.Server.Web.Areas.Admin.Models.InputInfo
{
    public class InputInfoModel
    {
        public string IdInput { get; set; }
        public int IdMedicine { get; set; }
        public int IdSupplier { get; set; }
        public string IdBatch { get; set; }
        public int Count { get; set; }
        public double InputPrice { get; set; }
        public double Total { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
    }
}