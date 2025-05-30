using BookLib.Core.Entities;
using BookLib.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookLib.Infrastructure.Data.Repositories
{
    public class CollectionRepository : GenericRepository<Collection>, ICollectionRepository
    {
        public CollectionRepository(BookLibContext context) : base(context) { }

        public async Task<IEnumerable<Collection>> GetUserCollectionsAsync(int userId)
        {
            return await _context.Collections
                .Where(c => c.UserId == userId)
                .Include(c => c.Books)
                .ThenInclude(b => b.Author)
                .ToListAsync();
        }

        public async Task AddBookToCollectionAsync(int collectionId, int bookId)
        {
            var collection = await _context.Collections
                .Include(c => c.Books)
                .FirstOrDefaultAsync(c => c.Id == collectionId);

            var book = await _context.Books.FindAsync(bookId);

            if (collection != null && book != null)
            {
                collection.Books.Add(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}