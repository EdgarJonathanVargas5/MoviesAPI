namespace MoviesAPI.Models.Dtos;

public class UserResponseDto
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public string? Name { get; set; }
    public string? Role { get; set; }
}