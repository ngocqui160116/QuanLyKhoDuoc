using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phoenix.Server.Web.Areas.Admin.Models.Medicine
{
    public class MedicineModel
    {
        public int IdMedicine { get; set; }
        public string RegistrationNumber { get; set; }
        public string Name { get; set; }
        public int IdGroup { get; set; }         //
        public string Active { get; set; }
        public string Content { get; set; }
        public string Packing { get; set; }
        public int IdCustomer { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public DateTime DueDate { get; set; }
        public string Unit { get; set; }                //
        public double Price { get; set; }
        public double UnitPrice { get; set; }
        public int Amount { get; set; }
        public int? Image { get; set; }
        public string Status { get; set; }
    }
}