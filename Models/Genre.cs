using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models;

public class Genre
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;
}