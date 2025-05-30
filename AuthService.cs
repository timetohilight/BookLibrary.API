using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BookLib.Core.Entities;
using BookLib.Core.Interfaces;
using BookLib.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookLib.Core.Services;
using BookLib.Core.Entities;
using BookLib.Core.Interfaces;
using BookLibraryAPI.DTOs.Accounts;
using AutoMapper;

namespace BookLibraryAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService; // Предполагается существование сервиса токенов

        public AuthService(
            IUserRepository userRepository,
            IMapper mapper,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            // Ваша существующая логика регистрации
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            // Ваша существующая логика входа
        }

        public async Task<UserDto> GetCurrentUserAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return _mapper.Map<UserDto>(user);
  
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _config;

    public AuthService(IUserRepository userRepository, IConfiguration config)
    {
        _userRepository = userRepository;
        _config = config;
    }


    public async Task<AuthResult> Register(RegisterModel model)
    {

        if (await _userRepository.GetByEmailAsync(model.Email) != null)
            return new AuthResult(false, "Email уже занят");

        if (await _userRepository.GetByUsernameAsync(model.Username) != null)
            return new AuthResult(false, "Имя пользователя уже занято");

        CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = new User
        {
            Username = model.Username,
            Email = model.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user);

        var token = GenerateJwtToken(user);
        return new AuthResult(true, "Регистрация успешна", token);
    }

    public async Task<AuthResult> Login(LoginModel model)
    {
        var user = await _userRepository.GetByEmailAsync(model.Email);
        if (user == null)
            return new AuthResult(false, "Пользователь не найден");

        if (!VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            return new AuthResult(false, "Неверный пароль");

        var token = GenerateJwtToken(user);
        return new AuthResult(true, "Вход выполнен успешно", token);
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds,
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    // Проверка 
    private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
        using var hmac = new HMACSHA512(storedSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(storedHash);
    }
}