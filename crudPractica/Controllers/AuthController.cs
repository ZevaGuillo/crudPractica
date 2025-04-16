using crudPractica.DTOs.UserDto;
using crudPractica.Services.auth;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace crudPractica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserDto dto)
        {
            await _authService.RegisterAsync(dto);
            return Ok(new { message = "Usuario registrado correctamente" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            return result == null ? Unauthorized("Credenciales incorrectas") : Ok(result);
        }

    }
}
