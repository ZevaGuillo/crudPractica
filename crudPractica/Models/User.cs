using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crudPractica.Models
{
    [Index(nameof(UserName), IsUnique =true)]
    public class User : IAuditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set;  }
        public string Role { get; set; }

        // Auditoría
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
