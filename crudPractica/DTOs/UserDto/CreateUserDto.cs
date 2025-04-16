using System.ComponentModel.DataAnnotations;

namespace crudPractica.DTOs.UserDto
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
