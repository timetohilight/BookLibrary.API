using Microsoft.AspNetCore.Mvc;
using BookLibraryAPI.DTOs.Accounts;
using BookLibraryAPI.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BookLibraryAPI.Services
{
    public interface IAuthService
    {
        Task<UserDto> RegisterAsync(RegisterDto registerDto);
        Task<string> LoginAsync(LoginDto loginDto);
        Task<UserDto> GetCurrentUserAsync(int userId); // Добавлен новый метод
    }
}

namespace BookLibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [SwaggerOperation("Регистрация нового пользователя")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var userDto = await _authService.RegisterAsync(registerDto);
            return Ok(userDto);
        }

        [HttpPost("login")]
        [SwaggerOperation("Вход пользователя")]
        public async Task<ActionResult<string>> Login(LoginDto loginDto)
        {
            var token = await _authService.LoginAsync(loginDto);
            return Ok(new { Token = token });
        }

        [Authorize] // Требует аутентификации
        [HttpGet("me")]
        [SwaggerOperation("Получение данных текущего пользователя")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            // Безопасное получение ID пользователя
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized("Invalid user identifier");
            }

            var userDto = await _authService.GetCurrentUserAsync(userId);
            return Ok(userDto);
        }
    }
}