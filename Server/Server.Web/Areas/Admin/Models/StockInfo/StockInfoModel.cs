using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phoenix.Server.Web.Areas.Admin.Models.StockInfo
{
    public class StockInfoModel
    {
        public int Id { get; set; }
        public int IdStaff { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
    }
}