using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Server.Data.Entity
{
    [Table("OutputInfo")]
    public class OutputInfo
    {
        [Key]
        public string IdOutput { get; set; }
        public int IdMedicine { get; set; }
        [ForeignKey("IdMedicine")]
        public virtual Medicine Medicine { get; set; }

        public string IdInputInfo { get; set; }
        [ForeignKey("IdInputInfo")]
        public virtual InputInfo InputInfo { get; set; }
        public int IdReason { get; set; }
        [ForeignKey("IdReason")]
        public virtual Reason Reason { get; set; }
        public int Count { get; set; }
        public double Total { get; set; }
        public string Status { get; set; }

       
    }
}
