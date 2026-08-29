using Microsoft.EntityFrameworkCore;
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
    public async Task<bool> CreateMovie(Movie movie)
    {
        await _db.Movies.AddAsync(movie);
        return await SaveAsync();
    }

    public async Task<bool> DeleteMovie(Movie movie)
    {
        _db.Movies.Remove(movie);
        return await SaveAsync();
    }

    public async Task<Movie?> GetMovie(int id)
    {
        return await _db.Movies.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<ICollection<Movie>> GetMovies()
    {
        return await _db.Movies.OrderBy(m => m.Title).ToListAsync();
    }

    public async Task<bool> MovieExists(int id)
    {
        return await _db.Movies.AnyAsync(m => m.Id == id);
    }

    public async Task<bool> MovieExists(string title)
    {
        return await _db.Movies.AnyAsync(m => m.Title.ToLower().Trim() == title.ToLower().Trim());
    }

    public async Task<bool> MovieExists(string title, int id)
    {
        return await _db.Movies.AnyAsync(m =>
                m.Title.ToLower().Trim() == title.ToLower().Trim()
                && m.Id != id);
    }

    public async Task<bool> SaveAsync()
    {
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateMovie(Movie movie)
    {
        _db.Movies.Update(movie);
        return await SaveAsync();
    }
}