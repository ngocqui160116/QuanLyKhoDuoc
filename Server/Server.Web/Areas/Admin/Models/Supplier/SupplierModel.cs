using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Phoenix.Server.Web.Areas.Admin.Models.Supplier
{
    public class SupplierModel
    {
        public int IdSupplier { get; set; }
        [Required(ErrorMessage = "Tên nhà cung cấp không được bỏ trống")]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được bỏ trống")]
        public string Address { get; set; }
        public bool Deleted { get; set; }
    }
}