using crudPractica.Models;
using crudPractica.Repositories.CategoryRepo;
using crudPractica.Repositories.UserRepo;
using crudPractica.Services.CategoryServices;
using Microsoft.AspNetCore.Http.HttpResults;

namespace crudPractica.Services.auth
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<CategoryService> _logger;

        public UserService(IUserRepository userRepository, ILogger<CategoryService> logger)
        {
            this._repository = userRepository;
            _logger = logger;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            User UserCreated = await _repository.CreateUserAsync(user);
            _logger.LogInformation("Created user with ID {Id}", UserCreated.Id);

            return UserCreated;
        }

        public Task<ICollection<User>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetUserById(int id)
        {
            var searchUser = await _repository.GetUserById(id);
            return searchUser == null ? null : searchUser;
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            var searchUser = await _repository.GetUserByUserName(username);
            return searchUser;
        }
    }
}
