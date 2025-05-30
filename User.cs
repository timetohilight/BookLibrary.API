namespace BookLib.Core.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastActive { get; set; }
    public string? Bio { get; set; }
    public string? AvatarUrl { get; set; }

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<UserBook> UserBooks { get; set; } = new List<UserBook>();
    public ICollection<Collection> Collections { get; set; } = new List<Collection>();
}