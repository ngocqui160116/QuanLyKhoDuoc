using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Server.Data.Entity
{
    [Table("Reason")]
    public class Reason
    {
        [Key]
        public int IdReason { get; set; }
        public string NameReason { get; set; }

    }
  
}
