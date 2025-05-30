using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace BookLibraryAPI.DTOs.Accounts;

[SwaggerSchema("Данные для регистрации")]
public class RegisterDto
{
    [Required(ErrorMessage = "Имя обязательно")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Имя должно быть от 2 до 50 символов")]
    [SwaggerSchema("Имя пользователя")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный формат email")]
    [SwaggerSchema("Email пользователя", Format = "email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Пароль обязателен")]
    [DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен быть от 6 до 100 символов")]
    [SwaggerSchema("Пароль", Format = "password")]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [SwaggerSchema("Подтверждение пароля", Format = "password")]
    public string ConfirmPassword { get; set; }
}