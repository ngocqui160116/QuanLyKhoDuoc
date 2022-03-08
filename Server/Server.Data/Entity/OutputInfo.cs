using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Server.Data.Entity
{
    [Table("OutputInfo")]
    public class OutputInfo
    {
        [Key]
        public string IdOutput { get; set; }
        public int IdMedicine { get; set; }
        [ForeignKey("IdMedicine")]
        public virtual Medicine Medicine { get; set; }

        public int IdCustomer { get; set; }
        [ForeignKey("IdCustomer")]
        public virtual Customer Customer { get; set; }
        public int Count { get; set; }
        public string Status { get; set; }

        //public int Amount { get; set; }
        //public int Unit { get; set; }
        //[ForeignKey("Unit")]
        //public virtual Unit Id { get; set; }
        //public bool Deleted { get; set; }
    }
}
