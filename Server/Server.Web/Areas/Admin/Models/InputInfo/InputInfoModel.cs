using System;
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
        public int IdBatch { get; set; }
        public int Count { get; set; }
        public double InputPrice { get; set; }
        public double Total { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public DateTime DueDate { get; set; }
        public string Id { get; set; }
        public int IdStaff { get; set; }
        public DateTime DateInput { get; set; }
        public int IdUnit { get; set; }
    }
}