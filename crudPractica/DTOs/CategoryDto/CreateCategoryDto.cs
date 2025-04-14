using System.ComponentModel.DataAnnotations;

namespace crudPractica.DTOs.CategoryDto
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public string Name { get; set; } = default!;
    }
}
