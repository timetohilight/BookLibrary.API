using BookLib.Core.Models;
using BookLib.Core.Filters;
using BookLib.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookLib.Core.Enums;

namespace BookLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IUserBookService _userBookService;

        public BooksController(
            IBookService bookService,
            IUserBookService userBookService)
        {
            _bookService = bookService;
            _userBookService = userBookService;
        }

        // GET: api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookModel>>> GetAllBooks(
            [FromQuery] BookFilter filter)
        {
            var books = await _bookService.GetAllBooksAsync(filter);
            return Ok(books);
        }

        // GET: api/books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookModel>> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // POST: api/books
        [HttpPost]
        public async Task<ActionResult<BookModel>> CreateBook(BookCreateModel model)
        {
            var createdBook = await _bookService.AddBookAsync(model);
            return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
        }

        // PUT: api/books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, BookUpdateModel model)
        {
            var updatedBook = await _bookService.UpdateBookAsync(id, model);
            if (updatedBook == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // GET: api/books/search?query=...
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BookModel>>> SearchBooks([FromQuery] string query)
        {
            var books = await _bookService.SearchBooksAsync(query);
            return Ok(books);
        }

        // GET: api/books/author/5
        [HttpGet("author/{authorId}")]
        public async Task<ActionResult<IEnumerable<BookModel>>> GetBooksByAuthor(int authorId)
        {
            var books = await _bookService.GetBooksByAuthorAsync(authorId);
            return Ok(books);
        }

        // GET: api/books/genre/5
        [HttpGet("genre/{genreId}")]
        public async Task<ActionResult<IEnumerable<BookModel>>> GetBooksByGenre(int genreId)
        {
            var books = await _bookService.GetBooksByGenreAsync(genreId);
            return Ok(books);
        }

        // GET: api/books/5/rating
        [HttpGet("{bookId}/rating")]
        public async Task<ActionResult<double?>> GetBookRating(int bookId)
        {
            var rating = await _bookService.GetBookAverageRatingAsync(bookId);
            return Ok(rating);
        }

        // USER-SPECIFIC ENDPOINTS //

        // GET: api/books/user/5?status=...
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetUserBooks(
            int userId,
            [FromQuery] ReadingStatus? status)
        {
            var books = await _userBookService.GetUserBooksAsync(userId, status);
            return Ok(books);
        }

        // PUT: api/books/user/5/book/10/status
        [HttpPut("user/{userId}/book/{bookId}/status")]
        public async Task<IActionResult> UpdateReadingStatus(
            int userId,
            int bookId,
            [FromBody] ReadingStatus status)
        {
            await _userBookService.UpdateBookStatusAsync(userId, bookId, status);
            return NoContent();
        }

        // PUT: api/books/user/5/book/10/rate
        [HttpPut("user/{userId}/book/{bookId}/rate")]
        public async Task<IActionResult> RateBook(
            int userId,
            int bookId,
            [FromBody] int rating)
        {
            await _userBookService.RateBookAsync(userId, bookId, rating);
            return NoContent();
        }
    }
}