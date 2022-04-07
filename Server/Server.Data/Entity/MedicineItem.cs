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
    }
}
