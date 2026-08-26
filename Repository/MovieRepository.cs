using MoviesAPI.Data;
using MoviesAPI.Models;
using MoviesAPI.Repository.IRepository;

namespace MoviesAPI.Repository;

public class MovieRepository : IMovieRepository
{
    private readonly ApplicationDbContext _db;
    public MovieRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public bool CreateMovie(Movie movie)
    {
        _db.Movies.Add(movie);
        return Save();
    }

    public bool DeleteMovie(Movie movie)
    {
        _db.Movies.Remove(movie);
        return Save();
    }

    public Movie? GetMovie(int id)
    {
        return _db.Movies.FirstOrDefault(m => m.Id == id);
    }

    public ICollection<Movie> GetMovies()
    {
        return _db.Movies.OrderBy(m => m.Title).ToList();
    }

    public bool MovieExists(int id)
    {
        return _db.Movies.Any(m => m.Id == id);
    }

    public bool MovieExists(string title)
    {
        return _db.Movies.Any(m => m.Title.ToLower().Trim() == title.ToLower().Trim());
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0;
    }

    public bool UpdateMovie(Movie movie)
    {
        _db.Movies.Update(movie);
        return Save();
    }
}