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
        public string IdOutput { get; set; }
        [ForeignKey("IdOutput")]
        public virtual Output Output { get; set; }
        public int IdMedicine { get; set; }
        [ForeignKey("IdMedicine")]
        public virtual Medicine Medicine { get; set; }

        public int IdInputInfo { get; set; }
        [ForeignKey("IdInputInfo")]
        public virtual InputInfo InputInfo { get; set; }

        public int Count { get; set; }
        public double Total { get; set; }
        public bool Status { get; set; }

      
    }
}
