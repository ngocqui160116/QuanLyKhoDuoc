using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Server.Data.Entity
{
    [Table("InputInfo")]
    public class InputInfo
    {
        [Key]
        public string IdInput { get; set; }
        [ForeignKey("IdInput")]
        public virtual Input Input { get; set; }
        public int IdMedicine { get; set; }
        [ForeignKey("IdMedicine")]
        public virtual Medicine Medicine { get; set; }
        public int IdSupplier { get; set; }
        [ForeignKey("IdSupplier")]
        public virtual Supplier Supplier { get; set; }
        public string IdBatch { get; set; }
        public int Count { get; set; }
        public double InputPrice { get; set; }
        public double OutputPrice { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }

        //public int Amount { get; set; }
        //public int Unit { get; set; }
        //[ForeignKey("Unit")]
        //public virtual Unit Id { get; set; }
        //public bool Deleted { get; set; }
    }
}
