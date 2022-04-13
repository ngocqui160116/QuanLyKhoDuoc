using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Server.Data.Entity
{
    [Table("MedicineItem")]
    public class MedicineItem
    {
        [Key]
        public int Id { get; set; }
        public int Medicine_Id { get; set; }
        [ForeignKey("Medicine_Id")]
        public virtual Medicine Medicine { get; set; }
        public int Batch { get; set; }
        public int Count { get; set; }
        public double InputPrice { get; set; }
        public double Total { get; set; }
        public DateTime DueDate { get; set; }
        public string UnitName { get; set; }
        public int? Inventory_Id { get; set; }
    }
}
