using crudPractica.DTOs.UserDto;

namespace crudPractica.Services.auth
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(CreateUserDto dto);
        Task<LoginUserResponseDto?> LoginAsync(LoginUserDto dto);
    }
}
