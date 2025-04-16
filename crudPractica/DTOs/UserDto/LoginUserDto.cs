using System.ComponentModel.DataAnnotations;

namespace crudPractica.DTOs.UserDto
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; }
    }
}
