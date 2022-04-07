
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Server.Data.Entity
{
    [Table("Inventory")]
    public class Inventory
    {
        [Key]
        public int Id { get; set; }
        public int IdMedicine { get; set; }
        [ForeignKey("IdMedicine")]
        public virtual Medicine Medicine { get; set; }
        public int? Count { get; set; }
        public int? LotNumber { get; set; }
        public int? IdInputInfo { get; set; }
        [ForeignKey("IdInputInfo")]
        public virtual InputInfo InputInfo { get; set; }
    }
}
