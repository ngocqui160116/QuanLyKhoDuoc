using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Server.Data.Entity
{
    [Table("Supplier")]
    public class Supplier
    {
        [Key]
        public int IdSupplier { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool Deleted { get; set; }
    }
}
