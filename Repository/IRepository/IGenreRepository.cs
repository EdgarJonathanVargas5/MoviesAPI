using MoviesAPI.Models;

namespace MoviesAPI.Repository.IRepository;

public interface IGenreRepository
{
    Task<ICollection<Genre>> GetGenres();
    Task<Genre?> GetGenreById(int id);
    Task<bool> GenreExists(int id);
    Task<bool> GenreExists(string name);
    Task<bool> CreateGenre(Genre genre);
    Task<bool> UpdateGenre(Genre genre);
    Task<bool> DeleteGenre(Genre genre);
    Task<bool> SaveAsync();
}