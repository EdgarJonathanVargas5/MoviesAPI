using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models.Dtos; 

public class CreateGenreDto
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(50, MinimumLength = 1,
        ErrorMessage = "El nombre debe tener entre 1 y 50 caracteres.")]
    public string Name { get; set; } = string.Empty;
}
