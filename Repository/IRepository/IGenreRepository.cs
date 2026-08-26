using MoviesAPI.Models;

namespace MoviesAPI.Repository.IRepository;

public interface IGenreRepository
{
    ICollection<Genre> GetGenres();
    Genre? GetGenre(int id);
    bool GenreExists(int id);
    bool GenreExists(string name);
    bool CreateGenre(Genre genre);
    bool UpdateGenre(Genre genre);
    bool DeleteGenre(Genre genre);
    bool Save();
}