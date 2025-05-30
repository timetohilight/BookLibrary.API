using BookLib.Core.Entities;
using BookLib.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookLib.Infrastructure.Data.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(BookLibContext context) : base(context) { }

        public async Task<IEnumerable<Review>> GetBookReviewsAsync(int bookId)
        {
            return await _context.Reviews
                .Where(r => r.BookId == bookId)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<double> GetBookAverageRatingAsync(int bookId)
        {
            return await _context.Reviews
                .Where(r => r.BookId == bookId)
                .AverageAsync(r => r.Rating);
        }
    }
}