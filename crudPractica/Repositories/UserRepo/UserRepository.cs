using crudPractica.Data;
using crudPractica.DTOs.UserDto;
using crudPractica.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace crudPractica.Repositories.UserRepo
{
    public class UserRepository : IUserRepository
    {

        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<ICollection<User>> GetUser()
        {
            return await _context.Users.OrderBy(c => c.UserName).ToListAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<User?> GetUserByUserName(string userName)
        {
            return await _context.Users
                    .FirstOrDefaultAsync(c => c.UserName== userName);
        }
    }
}
