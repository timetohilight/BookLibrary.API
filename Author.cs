namespace BookLib.Core.Entities;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Biography { get; set; }


    public ICollection<Book> Books { get; set; } = new List<Book>();
}