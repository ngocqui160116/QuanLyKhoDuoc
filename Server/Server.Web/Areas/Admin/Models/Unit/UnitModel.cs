using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Phoenix.Server.Web.Areas.Admin.Models.Unit
{
    public class UnitModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên đơn vị tính không được bỏ trống")]
        public string Name { get; set; }
    }
}