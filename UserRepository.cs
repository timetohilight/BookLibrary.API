using BookLib.Core.Entities;
using BookLib.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookLib.Infrastructure.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BookLibContext context) : base(context) { }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<UserBook>> GetUserBooksAsync(int userId)
        {
            return await _context.UserBooks
                .Where(ub => ub.UserId == userId)
                .Include(ub => ub.Book)
                .ThenInclude(b => b.Author)
                .ToListAsync();
        }
    }
}