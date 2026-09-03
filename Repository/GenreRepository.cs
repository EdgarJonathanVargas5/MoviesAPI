using Microsoft.EntityFrameworkCore;
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
    public async Task<bool> CreateGenre(Genre genre)
    {
        await _db.Genres.AddAsync(genre);
        return await SaveAsync();
    }

    public async Task<bool> DeleteGenre(Genre genre)
    {
        _db.Genres.Remove(genre);
        return await SaveAsync();
    }

    public async Task<bool> GenreExists(int id)
    {
        return await _db.Genres.AnyAsync(g => g.Id == id);
    }

    public async Task<bool> GenreExists(string name)
    {
        return await _db.Genres.AnyAsync(g => g.Name.ToLower().Trim() == name.ToLower().Trim());
    }

    public async Task<Genre?> GetGenreById(int id)
    {
        return await _db.Genres.FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<ICollection<Genre>> GetAllGenres()
    {
        return await _db.Genres.OrderBy(g => g.Name).ToListAsync();
    }

    public async Task<bool> SaveAsync()
    {
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateGenre(Genre genre)
    {
        _db.Genres.Update(genre);
        return await SaveAsync();
    }
}