using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Server.Data.Entity
{
    [Table("Invoice_Detail")]
    public class Invoice_Detail
    {
        [Key]
        public string IdInvoice { get; set; }
        public int IdMedicine { get; set; }
        [ForeignKey("IdMedicine")]
        public virtual Medicine Medicine { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public int Unit { get; set; }
        [ForeignKey("Unit")]
        public virtual Unit Id { get; set; }
        public bool Deleted { get; set; }
    }
}
