using crudPractica.DTOs.CategoryDto;
using crudPractica.DTOs.UserDto;
using crudPractica.Models;

namespace crudPractica.Services.auth
{
    public interface IUserService
    {
        Task<ICollection<User>> GetUsers();
        Task<User?> GetUserById(int id);
        Task<User?> GetUserByUsername(string username);
        Task<User> CreateUserAsync(User user);
    }
}
