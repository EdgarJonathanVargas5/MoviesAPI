using System;

namespace MoviesAPI.Models.Dtos;

public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public int GenreId { get; set; }
    public int DurationMinutes { get; set; }
}
