using BookLib.Core.Entities;

namespace BookLib.Core.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByEmailAsync(string email);
    Task<User> AddAsync(User user);
    Task<User?> UpdateAsync(User user);
    Task<bool> DeleteAsync(int id);

    Task<bool> CheckPasswordAsync(int userId, string password);
    Task UpdatePasswordAsync(int userId, byte[] passwordHash, byte[] passwordSalt);

    Task<IEnumerable<Book>> GetUserBooksByStatusAsync(int userId, ReadingStatus status);
    Task<int> GetBooksCountByStatusAsync(int userId, ReadingStatus status);
    Task<bool> BookExistsInUserLibraryAsync(int userId, int bookId);

    Task<int> GetTotalBooksReadAsync(int userId);
    Task<int> GetTotalPagesReadAsync(int userId);
    Task<double> GetAverageUserRatingAsync(int userId);
}