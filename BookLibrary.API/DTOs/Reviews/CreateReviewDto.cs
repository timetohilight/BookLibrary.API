using Swashbuckle.AspNetCore.Annotations;

namespace BookLibraryAPI.DTOs.Reviews;

[SwaggerSchema("Данные рецензии")]
public class ReviewDto
{
    [SwaggerSchema("ID рецензии")]
    public int Id { get; set; }

    [SwaggerSchema("Текст рецензии")]
    public string Text { get; set; }

    [SwaggerSchema("Оценка (1-5)")]
    public int Rating { get; set; }

    [SwaggerSchema("ID пользователя")]
    public int UserId { get; set; }

    [SwaggerSchema("Имя пользователя")]
    public string Username { get; set; }

    [SwaggerSchema("ID книги")]
    public int BookId { get; set; }

    [SwaggerSchema("Дата создания", Format = "date-time")]
    public DateTime CreatedAt { get; set; }
}