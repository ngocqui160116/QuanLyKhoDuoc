using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phoenix.Server.Web.Areas.Admin.Models.InventoryTags
{
    public class InventoryTagsModel
    {
        public int Id { get; set; }
        public string DocumentId { get; set; }
        public DateTime DocumentDate { get; set; }
        public int DocumentType { get; set; }
        public int MedicineId { get; set; }
        public int LotNumber { get; set; }
        public DateTime ExpiredDate { get; set; }
        public int Qty_Before { get; set; }
        public int Qty { get; set; }
        public int Qty_After { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public int SupplierId { get; set; }

        //
        public string MedicineName { get; set; }
        public string DocumentTypeName { get; set; }
    }
}