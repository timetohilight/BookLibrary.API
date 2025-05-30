using Swashbuckle.AspNetCore.Annotations;

namespace BookLibraryAPI.DTOs.Collections;

[SwaggerSchema("Данные коллекции")]
public class CollectionDto
{
    [SwaggerSchema("ID коллекции")]
    public int Id { get; set; }

    [SwaggerSchema("Название коллекции")]
    public string Name { get; set; }

    [SwaggerSchema("Описание коллекции")]
    public string Description { get; set; }

    [SwaggerSchema("ID пользователя")]
    public int UserId { get; set; }

    [SwaggerSchema("Количество книг")]
    public int BookCount { get; set; }

    [SwaggerSchema("Дата создания", Format = "date-time")]
    public DateTime CreatedAt { get; set; }
}