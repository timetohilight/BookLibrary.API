using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace BookLibraryAPI.DTOs.Collections;

[SwaggerSchema("Данные для создания коллекции")]
public class CreateCollectionDto
{
    [Required(ErrorMessage = "Название обязательно")]
    [StringLength(50, ErrorMessage = "Название не должно превышать 50 символов")]
    [SwaggerSchema("Название коллекции")]
    public string Name { get; set; }

    [SwaggerSchema("Описание коллекции")]
    public string Description { get; set; }
}