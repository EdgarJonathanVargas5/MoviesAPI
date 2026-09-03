namespace MoviesAPI.Models.Dtos;

public class CreateMovieDto
{
    [Required(ErrorMessage = "El título es obligatorio.")]
    [StringLength(150, MinimumLength = 1,
        ErrorMessage = "El título debe tener entre 1 y 150 caracteres.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripción es obligatoria.")]
    [StringLength(2000,
        ErrorMessage = "La descripción no puede superar los 2000 caracteres.")]
    public string Description { get; set; } = string.Empty;

    [Range(1888, 2100,
        ErrorMessage = "El año debe estar entre 1888 y 2100.")]
    public int ReleaseYear { get; set; }

    [Range(1, int.MaxValue,
        ErrorMessage = "Debes indicar un género válido.")]
    public int GenreId { get; set; }

    [Range(1, 600,
        ErrorMessage = "La duración debe estar entre 1 y 600 minutos.")]
    public int DurationMinutes { get; set; }
}