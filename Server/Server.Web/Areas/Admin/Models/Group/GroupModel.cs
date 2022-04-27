using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Phoenix.Server.Web.Areas.Admin.Models.Group
{
    public class GroupModel
    {
        public int IdGroup { get; set; }
        [Required(ErrorMessage = "Tên nhóm không được bỏ trống")]
        public string Name { get; set; }
    }
}