using BookLib.Core.Entities;
using BookLib.Core.Filters;

namespace BookLib.Core.Interfaces;

public interface IBookRepository
{
    Task<Book?> GetByIdAsync(int id);
    Task<IEnumerable<Book>> GetAllAsync(BookFilter? filter = null);
    Task<Book> AddAsync(Book book);
    Task<Book?> UpdateAsync(Book book);
    Task<bool> DeleteAsync(int id);

    Task<IEnumerable<Book>> GetByAuthorAsync(int authorId);
    Task<IEnumerable<Book>> GetByGenreAsync(int genreId);
    Task<IEnumerable<Book>> SearchAsync(string query);
    Task<bool> ExistsAsync(int id);

    Task AddAuthorToBookAsync(int bookId, int authorId);
    Task RemoveAuthorFromBookAsync(int bookId, int authorId);
    Task AddGenreToBookAsync(int bookId, int genreId);
    Task RemoveGenreFromBookAsync(int bookId, int genreId);

    Task<double?> GetAverageRatingAsync(int bookId);
    Task<int> GetReviewCountAsync(int bookId);
}