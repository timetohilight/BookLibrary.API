using BookLib.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookLib.Infrastructure.Seed;

public static class DataSeeder
{
    public static async Task SeedAsync(BookLibContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (!await context.Users.AnyAsync())
        {
            var users = new List<User>
            {
                new() { Username = "admin", Email = "admin@booklib.com" }
                // Пароль будет установлен через AuthService
            };

            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }

        if (!await context.Genres.AnyAsync())
        {
            var genres = new List<Genre>
            {
                new() { Name = "Фэнтези" },
                new() { Name = "Научная фантастика" },
                new() { Name = "Детектив" }
            };

            await context.Genres.AddRangeAsync(genres);
            await context.SaveChangesAsync();
        }

        if (!await context.Authors.AnyAsync())
        {
            var authors = new List<Author>
            {
                new() { Name = "Дж.Р.Р. Толкин" },
                new() { Name = "Стивен Кинг" },
                new() { Name = "Агата Кристи" }
            };

            await context.Authors.AddRangeAsync(authors);
            await context.SaveChangesAsync();
        }
    }
}