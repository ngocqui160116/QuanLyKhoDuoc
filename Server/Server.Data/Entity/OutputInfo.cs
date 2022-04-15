using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Server.Data.Entity
{
    [Table("OutputInfo")]
    public class OutputInfo
    {
        [Key]
        public int Id { get; set; }
        public int IdOutput { get; set; }
        [ForeignKey("IdOutput")]
        public virtual Output Output { get; set; }
        public int IdMedicine { get; set; }
        [ForeignKey("IdMedicine")]
        public virtual Medicine Medicine { get; set; }
        public int? Inventory_Id { get; set; }
        [ForeignKey("Inventory_Id")]
        public virtual Inventory Inventory { get; set; }
        public int Count { get; set; }
        public double Total { get; set; }


      
    }
}
