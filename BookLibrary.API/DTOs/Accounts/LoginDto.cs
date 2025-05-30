using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace BookLibraryAPI.DTOs.Accounts;

[SwaggerSchema("Данные для входа")]
public class LoginDto
{
    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный формат email")]
    [SwaggerSchema("Email пользователя", Format = "email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Пароль обязателен")]
    [DataType(DataType.Password)]
    [SwaggerSchema("Пароль", Format = "password")]
    public string Password { get; set; }
}