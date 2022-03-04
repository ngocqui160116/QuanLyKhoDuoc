using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Server.Data.Entity
{
    [Table("Medicine")]
    public class Medicine
    {
        [Key]
        public int IdMedicine { get; set; }
        public string RegistrationNumber { get; set; }
        public string Name { get; set; }
        public int IdGroup { get; set; }
        [ForeignKey("IdGroup")]
        public virtual Group Group { get; set; }
        public string Active { get; set; }
        public string Content { get; set; }
        public string Packing { get; set; }
        public int IdCustomer { get; set; }
        [ForeignKey("IdCustomer")]
        public virtual Customer Customer { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public DateTime DueDate { get; set; }
        public int Unit { get; set; }
        [ForeignKey("Unit")]
       public virtual Unit Id { get; set; }
        public double Price { get; set; }
        public double UnitPrice { get; set; }
        public int Amount { get; set; }
        public int? Image { get; set; }
        [ForeignKey("Image")]
        public virtual Medicine_Image Medicine_Image { get; set; }
        public string Status { get; set; }
    }
}
