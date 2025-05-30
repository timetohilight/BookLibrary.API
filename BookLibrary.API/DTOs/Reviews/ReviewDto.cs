using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace BookLibraryAPI.DTOs.Reviews;

[SwaggerSchema("Данные для создания рецензии")]
public class CreateReviewDto
{
    [Required(ErrorMessage = "Текст рецензии обязателен")]
    [StringLength(2000, ErrorMessage = "Рецензия не должна превышать 2000 символов")]
    [SwaggerSchema("Текст рецензии")]
    public string Text { get; set; }

    [Required(ErrorMessage = "Оценка обязательна")]
    [Range(1, 5, ErrorMessage = "Оценка должна быть от 1 до 5")]
    [SwaggerSchema("Оценка (1-5)")]
    public int Rating { get; set; }

    [Required(ErrorMessage = "ID книги обязателен")]
    [SwaggerSchema("ID книги")]
    public int BookId { get; set; }
}