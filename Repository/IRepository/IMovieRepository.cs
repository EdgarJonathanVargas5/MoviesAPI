using MoviesAPI.Models;

namespace MoviesAPI.Repository.IRepository;

public interface IMovieRepository
{
    Task<ICollection<Movie>> GetAllMovies();
    Task<Movie?> GetMovieById(int id);
    Task<bool> MovieExists(int id);
    Task<bool> MovieExists(string name);
    Task<bool> MovieExists(string title, int id);
    Task<bool> CreateMovie(Movie movie);
    Task<bool> UpdateMovie(Movie movie);
    Task<bool> DeleteMovie(Movie movie);
    Task<bool> SaveAsync();
}
