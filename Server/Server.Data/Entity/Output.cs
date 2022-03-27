using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Server.Data.Entity
{
    [Table("Output")]
    public class Output
    {
        [Key]
        public string Id { get; set; }
        public int IdStaff { get; set; }
        [ForeignKey("IdStaff")]
        public virtual Staff Staff { get; set; }
        public DateTime DateOutput { get; set; }
        public int IdReason { get; set; }
        [ForeignKey("IdReason")]
        public virtual Reason Reason { get; set; }
        public bool Status { get; set; }

        //List
        public virtual ICollection<OutputInfo> OutputInfo { get; set; }
    }
    
}
