using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phoenix.Server.Web.Areas.Admin.Models.Input
{
    public class OutputModel
    {
        public string Id { get; set; }
        public int IdStaff { get; set; }
        public DateTime DateOutput { get; set; }
        public int IdReason { get; set; }
        public bool Status { get; set; }
    }
}