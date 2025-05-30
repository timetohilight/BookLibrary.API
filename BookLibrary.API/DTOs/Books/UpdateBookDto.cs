using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace BookLibraryAPI.DTOs.Books;

[SwaggerSchema("Данные для обновления книги")]
public class UpdateBookDto
{
    [StringLength(100, ErrorMessage = "Название не должно превышать 100 символов")]
    [SwaggerSchema("Название книги")]
    public string Title { get; set; }

    [SwaggerSchema("Описание книги")]
    public string Description { get; set; }

    [Url(ErrorMessage = "Некорректный URL обложки")]
    [SwaggerSchema("URL обложки")]
    public string CoverUrl { get; set; }
}