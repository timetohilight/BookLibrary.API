using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace BookLibraryAPI.DTOs.Books;

[SwaggerSchema("Данные для создания книги")]
public class CreateBookDto
{
    [Required(ErrorMessage = "Название обязательно")]
    [StringLength(100, ErrorMessage = "Название не должно превышать 100 символов")]
    [SwaggerSchema("Название книги")]
    public string Title { get; set; }

    [SwaggerSchema("Описание книги")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Автор обязателен")]
    [SwaggerSchema("ID автора")]
    public int AuthorId { get; set; }

    [Url(ErrorMessage = "Некорректный URL обложки")]
    [SwaggerSchema("URL обложки")]
    public string CoverUrl { get; set; }

    [SwaggerSchema("Дата публикации", Format = "date")]
    public DateTime PublishDate { get; set; }
}