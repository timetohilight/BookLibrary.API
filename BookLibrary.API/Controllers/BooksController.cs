using Microsoft.AspNetCore.Mvc;
using BookLibraryAPI.DTOs.Books;
using BookLibraryAPI.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookLibraryAPI.Services
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAllBooksAsync();
        Task<BookDto> GetBookByIdAsync(int id);
        Task<BookDto> AddBookAsync(CreateBookDto createBookDto); // Изменено: возвращает BookDto
        Task DeleteBookAsync(int id);
        Task<bool> UpdateBookAsync(int id, UpdateBookDto updateBookDto); // Изменено: возвращает статус операции
    }
}

namespace BookLibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [SwaggerOperation("Получение списка всех книг")]
        public async Task<ActionResult<List<BookDto>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Получение книги по ID")]
        public async Task<ActionResult<BookDto>> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        [SwaggerOperation("Добавление новой книги")]
        public async Task<ActionResult<BookDto>> AddBook(CreateBookDto createBookDto)
        {
            var book = await _bookService.AddBookAsync(createBookDto);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        [SwaggerOperation("Обновление информации о книге")]
        public async Task<IActionResult> UpdateBook(int id, UpdateBookDto updateBookDto)
        {
            var result = await _bookService.UpdateBookAsync(id, updateBookDto);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        
    }
}