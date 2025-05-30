namespace BookLib.Core.Entities;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }


    public ICollection<Book> Books { get; set; } = new List<Book>();
}