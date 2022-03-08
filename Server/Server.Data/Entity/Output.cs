using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Server.Data.Entity
{
    [Table("Output")]
    public class Output
    {
        [Key]
        public string Id { get; set; }
        public DateTime DateOutput { get; set; }

        //public int IdStaff { get; set; }
        //[ForeignKey("IdStaff")]
        //public virtual Staff Staff { get; set; }
        //public int IdCustomer { get; set; }
        //[ForeignKey("IdCustomer")]
        //public virtual Customer Customer { get; set; }
        //public double Total { get; set; }
        //public bool Deleted { get; set; }
    }
}
