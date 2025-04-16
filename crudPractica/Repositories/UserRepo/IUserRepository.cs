using crudPractica.DTOs.UserDto;
using crudPractica.Models;

namespace crudPractica.Repositories.UserRepo
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetUser();
        Task<User?> GetUserById(int id);
        Task<User?> GetUserByUserName(string userName);

        Task<User> CreateUserAsync(User user);

    }
}
