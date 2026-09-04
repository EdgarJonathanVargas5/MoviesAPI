namespace MoviesAPI.Models.Dtos;

public class UserLoginResponseDto
{
  public UserResponseDto? User { get; set; }
  public string? Token { get; set; }
  public string? Message { get; set; }
}