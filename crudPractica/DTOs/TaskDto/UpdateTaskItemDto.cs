using System.ComponentModel.DataAnnotations;

namespace crudPractica.DTOs.TaskDto
{
    public class UpdateTaskItemDto
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(100, ErrorMessage = "El título no puede tener más de 100 caracteres.")]
        public string Title { get; set; } = default!;

        [StringLength(500, ErrorMessage = "La descripción no puede tener más de 500 caracteres.")]
        public string? Description { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required(ErrorMessage = "Debe asignar una categoría.")]
        public int CategoryId { get; set; }
    }
}
