using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phoenix.Server.Web.Areas.Admin.Models.Inventory
{
    public class InventoryModel
    {
        public int Id { get; set; }
        public int IdMedicine { get; set; }
        public int Count { get; set;}
        public int LotNumber { get; set; }
        public int IdInputInfo { get; set; }
    }
}