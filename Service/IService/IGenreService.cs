using MoviesAPI.Models.Dtos;

namespace MoviesAPI.Service.IService;

public interface IGenreService
{
    Task<ICollection<GenreDto>> GetGenresAsync();
    Task<GenreDto?> GetGenreAsync(int id);
}
