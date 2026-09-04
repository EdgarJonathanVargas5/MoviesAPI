using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;
using MoviesAPI.Models;
using MoviesAPI.Repository.IRepository;

namespace MoviesAPI.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ICollection<User>> GetAllUsers()
    {
        var users = await _db.Users.OrderBy(user => user.Id).ToListAsync();
        return users;
    }

    public async Task<User?> GetUserById(int id)
    {
        var user = await _db.Users.FirstOrDefaultAsync(user => user.Id == id);
        return user;
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        var normalizedUsername = username.Trim().ToLower();
        var user = await _db.Users.FirstOrDefaultAsync(user => user.Username.ToLower() == normalizedUsername);
        return user;
    }

    public async Task<bool> IsUniqueUser(string username)
    {
        var normalizedUsername = username.Trim().ToLower();
        return !await _db.Users.AnyAsync(user => user.Username.ToLower() == normalizedUsername);
    }

    public async Task<User> CreateUser(User user)
    {
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
        return user;
    }
}