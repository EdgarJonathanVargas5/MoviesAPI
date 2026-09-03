using MoviesAPI.Models.Dtos;

namespace MoviesAPI.Service.IService;

public interface IGenreService
{
    Task<ICollection<GenreDto>> GetAllGenresAsync();
    Task<GenreDto?> GetGenreByIdAsync(int id);
    Task<GenreDto> CreateGenreAsync(CreateGenreDto createGenreDto);
    Task<bool> UpdateGenreAsync(int id, CreateGenreDto updateGenreDto);
    Task<bool> DeleteGenreAsync(int id);
}
