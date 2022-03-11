using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Server.Data.Entity
{
    [Table("Input")]
    public class Input
    {
        [Key]
        public string Id { get; set; }
     
        public DateTime DateInput { get; set; }
      
    }
}
