using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace crudPractica.Models
{
    public class TaskItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool IsCompleted { get; set; } 
        
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
