using BookLib.Core.Interfaces;
using BookLib.Core.Entities;
using BookLib.Core.Enums;
using BookLib.Infrastructure.Data.Repositories;
using BookLibraryAPI.DTOs.Books;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BookLib.Core.Services
{
    public class UserBookService : IUserBookService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;

        public UserBookService(
            IUserRepository userRepository,
            IBookRepository bookRepository)
        {
            _userRepository = userRepository;
            _bookRepository = bookRepository;
        }

        public async Task<List<BookDto>> GetUserBooksAsync(int userId, ReadingStatus? status)
        {
            var userBooks = await _userRepository.GetUserBooksAsync(userId);

            if (status.HasValue)
            {
                userBooks = userBooks.Where(ub => ub.Status == status.Value).ToList();
            }

            return userBooks.Select(ub => new BookDto
            {
                Id = ub.BookId,
                Title = ub.Book.Title,
                Author = ub.Book.Author.Name,
                CoverUrl = ub.Book.CoverUrl,
                Status = ub.Status
            }).ToList();
        }

        public async Task UpdateBookStatusAsync(int userId, int bookId, ReadingStatus status)
        {
            await _userRepository.UpdateBookStatusAsync(userId, bookId, status);
        }

        public async Task RateBookAsync(int userId, int bookId, int rating)
        {
            await _userRepository.RateBookAsync(userId, bookId, rating);
        }
    }
}