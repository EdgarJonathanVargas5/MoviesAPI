using MoviesAPI.Models.Dtos;

namespace MoviesAPI.Service.IService;

public interface IGenreService
{
    Task<ICollection<GenreDto>> GetAllGenresAsync();
    Task<GenreDto?> GetGenreByIdAsync(int id);
}
