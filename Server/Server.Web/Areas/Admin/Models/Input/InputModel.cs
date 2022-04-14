using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phoenix.Server.Web.Areas.Admin.Models.Input
{
    public class InputModel
    {
        public int Id { get; set; }
        public int IdStaff { get; set; }
        public int IdSupplier { get; set; }
        public DateTime DateInput { get; set; }
        public string Status { get; set; }
        public string TableContent { get; set; }
    }
}