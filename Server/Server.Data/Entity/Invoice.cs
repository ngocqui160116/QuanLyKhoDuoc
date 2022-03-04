using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Server.Data.Entity
{
    [Table("Invoice")]
    public class Invoice
    {
        [Key]
        public string IdInvoice { get; set; }
        public DateTime Date { get; set; }
        public int IdStaff { get; set; }
        [ForeignKey("IdStaff")]
        public virtual Staff Staff { get; set; }
        public int IdCustomer { get; set; }
        [ForeignKey("IdCustomer")]
        public virtual Customer Customer { get; set; }
        public double Total { get; set; }
        public bool Deleted { get; set; }
    }
}
