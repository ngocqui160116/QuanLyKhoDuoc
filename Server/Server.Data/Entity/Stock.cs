
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Server.Data.Entity
{
    [Table("Stock")]
    public class Stock
    {
        [Key]
        public int Id { get; set; }
        public int IdStaff { get; set; }
        [ForeignKey("IdStaff")]
        public virtual Staff Staff { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }


        //List
        public virtual ICollection<StockInfo> StockInfo { get; set; }
    }
}
