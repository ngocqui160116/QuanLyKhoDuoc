using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Server.Data.Entity
{
    [Table("Group")]
    public class Group
    {
        [Key]
        public int IdGroup { get; set; }
        public string Name { get; set; }
       
    }
}
