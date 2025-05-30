namespace BookLib.Core.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? CoverImageUrl { get; set; }
    public string? Isbn { get; set; }
    public int? PageCount { get; set; }
    public DateTime? PublishedDate { get; set; }
    public string? Publisher { get; set; }

    public ICollection<Author> Authors { get; set; } = new List<Author>();
    public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Collection> Collections { get; set; } = new List<Collection>();
    public ICollection<UserBook> UserBooks { get; set; } = new List<UserBook>();

    public double? AverageRating => Reviews?.Any() == true ? Reviews.Average(r => r.Rating) : null;
}