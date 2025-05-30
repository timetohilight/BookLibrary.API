using Microsoft.AspNetCore.Mvc;
using BookLibraryAPI.DTOs.Reviews;
using BookLibraryAPI.Services;
using Swashbuckle.AspNetCore.Annotations;


using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookLibraryAPI.Services
{
    public interface IReviewService
    {
        Task<List<ReviewDto>> GetBookReviewsAsync(int bookId);
        Task<ReviewDto> AddReviewAsync(int userId, CreateReviewDto createReviewDto);
        Task DeleteReviewAsync(int userId, int reviewId);
    }
}

namespace BookLibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("book/{bookId}")]
        [SwaggerOperation("Получение всех рецензий для книги")]
        public async Task<ActionResult<List<ReviewDto>>> GetBookReviews(int bookId)
        {
            var reviews = await _reviewService.GetBookReviewsAsync(bookId);
            return Ok(reviews);
        }

        [HttpPost]
        [SwaggerOperation("Добавление новой рецензии")]
        public async Task<ActionResult<ReviewDto>> AddReview(CreateReviewDto createReviewDto)
        {
            var userId = int.Parse(User.FindFirst("id").Value);
            var review = await _reviewService.AddReviewAsync(userId, createReviewDto);
            return CreatedAtAction(nameof(GetBookReviews), new { bookId = review.BookId }, review);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Удаление рецензии")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var userId = int.Parse(User.FindFirst("id").Value);
            await _reviewService.DeleteReviewAsync(userId, id);
            return NoContent();
        }
    }
}