using Swashbuckle.AspNetCore.Annotations;

namespace BookLibraryAPI.DTOs.Books;

[SwaggerSchema("Данные книги")]
public class BookDto
{
    [SwaggerSchema("ID книги")]
    public int Id { get; set; }

    [SwaggerSchema("Название книги")]
    public string Title { get; set; }

    [SwaggerSchema("Описание книги")]
    public string Description { get; set; }

    [SwaggerSchema("URL обложки")]
    public string CoverUrl { get; set; }

    [SwaggerSchema("ID автора")]
    public int AuthorId { get; set; }

    [SwaggerSchema("Имя автора")]
    public string AuthorName { get; set; }

    [SwaggerSchema("Средний рейтинг")]
    public double AverageRating { get; set; }

    [SwaggerSchema("Дата публикации", Format = "date")]
    public DateTime PublishDate { get; set; }
}