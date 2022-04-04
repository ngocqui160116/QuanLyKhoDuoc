using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Server.Data.Entity
{
    [Table("InputInfo")]
    public class InputInfo
    {
        [Key]
        public int Id { get; set; }
        public int IdInput { get; set; }
        [ForeignKey("IdInput")]
        public virtual Input Input { get; set; }
        public int IdMedicine { get; set; }
        [ForeignKey("IdMedicine")]
        public virtual Medicine Medicine { get; set; }
        public int IdBatch { get; set; }
        public int Count { get; set; }
        public double InputPrice { get; set; }
        public double Total { get; set; }
        public DateTime DueDate { get; set; }
        //public List<InputInfo> list { get; set; }
       
    }
}
