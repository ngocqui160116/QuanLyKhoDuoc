using System;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Server.Data.Entity
{
    [Table("Input")]
    public class Input
    {
        [Key]
        public int Id { get; set; }
        public int IdStaff { get; set; }
        [ForeignKey("IdStaff")]
        public virtual Staff Staff { get; set; }
        public int IdSupplier { get; set; }
        [ForeignKey("IdSupplier")]
        public virtual Supplier Supplier { get; set; }
        public DateTime DateInput { get; set; }
        public string Status { get; set; }

        //List
        public virtual ICollection<InputInfo> InputInfo { get; set; }
    }
}
