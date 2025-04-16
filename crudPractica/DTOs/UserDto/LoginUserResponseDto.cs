namespace crudPractica.DTOs.UserDto
{
    public class LoginUserResponseDto
    {
        public UserDataDto User { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
