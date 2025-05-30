namespace BookLibraryAPI.DTOs.Accounts;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    // Добавьте другие необходимые свойства
}