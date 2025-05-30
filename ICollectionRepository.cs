using BookLib.Core.Entities;

namespace BookLib.Core.Interfaces;

public interface IReviewRepository
{
    Task<Review?> GetByIdAsync(int id);
    Task<IEnumerable<Review>> GetByBookIdAsync(int bookId);
    Task<IEnumerable<Review>> GetByUserIdAsync(int userId);
    Task<Review> AddAsync(Review review);
    Task<Review?> UpdateAsync(Review review);
    Task<bool> DeleteAsync(int id);

    Task<bool> UserHasReviewedBookAsync(int userId, int bookId);
    Task<Review?> GetUserReviewForBookAsync(int userId, int bookId);
    Task<double> GetAverageRatingForUserAsync(int userId);

    Task<bool> UserOwnsReviewAsync(int reviewId, int userId);
}