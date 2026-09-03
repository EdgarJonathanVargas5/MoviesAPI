using MoviesAPI.Models.Dtos;

namespace MoviesAPI.Service.IService;

public interface IMovieService
{
    Task<ICollection<MovieDto>> GetAllMoviesAsync();
    Task<MovieDto?> GetMovieByIdAsync(int id);
    Task<MovieDto> CreateMovieAsync(CreateMovieDto createMovieDto);
    Task<bool> UpdateMovieAsync(int id, CreateMovieDto updateMovieDto);
    Task<bool> DeleteMovieAsync(int id);
}
