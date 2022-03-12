using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phoenix.Server.Web.Areas.Admin.Models.InputInfo
{
    public class OutputInfoModel
    {
        public string IdOutput { get; set; }
        public int IdMedicine { get; set; }
        public int IdCustomer { get; set; }
        public int Count { get; set; }
        public string Status { get; set; }
    }
}