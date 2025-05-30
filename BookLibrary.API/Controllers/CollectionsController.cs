using Microsoft.AspNetCore.Mvc;
using BookLibraryAPI.DTOs.Collections;
using BookLibraryAPI.Services;
using Swashbuckle.AspNetCore.Annotations;


using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookLibraryAPI.Services
{
    public interface ICollectionService
    {
        Task<List<CollectionDto>> GetUserCollectionsAsync(int userId);
        Task<CollectionDto> CreateCollectionAsync(int userId, CreateCollectionDto createCollectionDto);
        Task AddBookToCollectionAsync(int collectionId, int bookId);
        Task RemoveBookFromCollectionAsync(int collectionId, int bookId);
    }
}

namespace BookLibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollectionsController : ControllerBase
    {
        private readonly ICollectionService _collectionService;

        public CollectionsController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpGet]
        [SwaggerOperation("Получение всех коллекций пользователя")]
        public async Task<ActionResult<List<CollectionDto>>> GetUserCollections()
        {
            var userId = int.Parse(User.FindFirst("id").Value);
            var collections = await _collectionService.GetUserCollectionsAsync(userId);
            return Ok(collections);
        }

        [HttpPost]
        [SwaggerOperation("Создание новой коллекции")]
        public async Task<ActionResult<CollectionDto>> CreateCollection(CreateCollectionDto createCollectionDto)
        {
            var userId = int.Parse(User.FindFirst("id").Value);
            var collection = await _collectionService.CreateCollectionAsync(userId, createCollectionDto);
            return CreatedAtAction(nameof(GetUserCollections), collection);
        }

        [HttpPost("{collectionId}/books/{bookId}")]
        [SwaggerOperation("Добавление книги в коллекцию")]
        public async Task<IActionResult> AddBookToCollection(int collectionId, int bookId)
        {
            await _collectionService.AddBookToCollectionAsync(collectionId, bookId);
            return NoContent();
        }

        [HttpDelete("{collectionId}/books/{bookId}")]
        [SwaggerOperation("Удаление книги из коллекции")]
        public async Task<IActionResult> RemoveBookFromCollection(int collectionId, int bookId)
        {
            await _collectionService.RemoveBookFromCollectionAsync(collectionId, bookId);
            return NoContent();
        }
    }
}