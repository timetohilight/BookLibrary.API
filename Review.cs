namespace BookLib.Core.Entities;

public class Review
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public int UserId { get; set; }
    public int BookId { get; set; }

    public User User { get; set; }
    public Book Book { get; set; }
}