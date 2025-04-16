using crudPractica.DTOs.UserDto;
using crudPractica.Models;
using crudPractica.Repositories.UserRepo;
using crudPractica.Services.CategoryServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace crudPractica.Services.auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserService userService;
        private readonly ILogger<CategoryService> _logger;
        private readonly IConfiguration _config;

        public AuthService(IUserService userService, ILogger<CategoryService> logger, IConfiguration config)
        {
            this.userService = userService;
            _logger = logger;
            _config = config;
        }

        public async Task<LoginUserResponseDto?> LoginAsync(LoginUserDto dto)
        {
            // validar existencia de username
            var user = await userService.GetUserByUsername(dto.UserName);
            if (user == null)
                throw new ArgumentException("Validar Credenciales");

            // validamos contraseña
            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.Password, dto.Password);
            if (result == PasswordVerificationResult.Failed) return null;

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role),
            };
            // configuracion para la encriptación
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpireMinutes"]!)),
                signingCredentials: creds
            );

            return new LoginUserResponseDto
            {
                Role = user.Role,
                User = new UserDataDto { Id= user.Id, Name = user.Name, UserName = user.UserName },
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        public async Task<bool> RegisterAsync(CreateUserDto dto)
        {
            // validar existencia de username
            var search = await userService.GetUserByUsername(dto.UserName);
            if (search != null)
                throw new ArgumentException("El username ya está registrado");

            var hasher = new PasswordHasher<User>();
            var user = new User
            {
                UserName = dto.UserName,
                Name = dto.Name,
                Role = "ADMIN"
            };
            user.Password = hasher.HashPassword(user, dto.Password);

            User NewUser = await userService.CreateUserAsync(user);
            return NewUser != null;
        }
    }
}
