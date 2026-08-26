using MoviesAPI.Data;
using MoviesAPI.Models;
using MoviesAPI.Repository.IRepository;

namespace MoviesAPI.Repository;

public class GenreRepository : IGenreRepository
{
    private readonly ApplicationDbContext _db;
    public GenreRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public bool CreateGenre(Genre genre)
    {
        _db.Genres.Add(genre);
        return Save();
    }

    public bool DeleteGenre(Genre genre)
    {
        _db.Genres.Remove(genre);
        return Save();
    }

    public bool GenreExists(int id)
    {
        return _db.Genres.Any(g => g.Id == id);
    }

    public bool GenreExists(string name)
    {
        return _db.Genres.Any(g => g.Name.ToLower().Trim() == name.ToLower().Trim());
    }

    public Genre? GetGenre(int id)
    {
        return _db.Genres.FirstOrDefault(g => g.Id == id);
    }

    public ICollection<Genre> GetGenres()
    {
        return _db.Genres.OrderBy(g => g.Name).ToList();
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0;
    }

    public bool UpdateGenre(Genre genre)
    {
        _db.Genres.Update(genre);
        return Save();
    }
}