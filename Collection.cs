namespace BookLib.Core.Entities;

public class Collection
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsPublic { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public int UserId { get; set; }
    public User User { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}