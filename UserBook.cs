namespace BookLib.Core.Entities;

public class UserBook
{
    public int Id { get; set; }
    public ReadingStatus Status { get; set; }
    public DateTime? AddedDate { get; set; } = DateTime.UtcNow;
    public DateTime? StartedReading { get; set; }
    public DateTime? FinishedReading { get; set; }
    public int? UserRating { get; set; } 


    public int UserId { get; set; }
    public int BookId { get; set; }

    // Навигационные свойства
    public User User { get; set; }
    public Book Book { get; set; }
}

public enum ReadingStatus
{
    WantToRead = 1,
    CurrentlyReading = 2,
    Read = 3,
    Abandoned = 4
}