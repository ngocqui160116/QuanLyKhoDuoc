using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Server.Data.Entity
{
    [Table("InventoryTags")]
    public class InventoryTags
    {
        [Key]
        public int Id { get; set; }
        public string DocumentId { get; set; }
        public DateTime? DocumentDate { get; set; }
        public int DocumentType { get; set; }
        [ForeignKey("DocumentType")]
        public virtual DocumentType documentType { get; set; }
        public int MedicineId { get; set; }
        [ForeignKey("MedicineId")]
        public virtual Medicine Medicine { get; set; }
        public int LotNumber { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public int? Qty_Before { get; set; }
        public int? Qty { get; set; }
        public int? Qty_After { get; set; }
        public double? UnitPrice { get; set; }
        public double? TotalPrice { get; set; }
       
   
    }
 
}
