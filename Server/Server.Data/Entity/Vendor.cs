using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Server.Data.Entity
{
    [Table("Vendors")]
    public class Vendor
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int? Logo { get; set; }
        [ForeignKey("Logo")]
        public virtual Medicine_Image Medicine_Image { get; set; }
        public string Nation { get; set; }
        public bool Deleted { get; set; }
    }
}
