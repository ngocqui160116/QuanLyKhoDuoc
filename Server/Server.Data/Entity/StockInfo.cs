
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Server.Data.Entity
{
    [Table("StockInfo")]
    public class StockInfo
    {
        [Key]
        public int Id { get; set; }
        public int Stock_Id { get; set; }
        [ForeignKey("Stock_Id")]
        public virtual Stock Stock { get; set; }
        public int Inventory_Id { get; set; }
        [ForeignKey("Inventory_Id")]
        public virtual Inventory Inventory { get; set; }
        public int Medicine_Id { get; set; }
        public int? Amount { get; set; }
        public int ActualAmount { get; set; }
    }
}
