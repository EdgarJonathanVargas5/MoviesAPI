using System;
using MoviesAPI.Models;
using MoviesAPI.Models.Dtos;

namespace MoviesAPI.Repository.IRepository;

public interface IUserRepository
{
    Task<ICollection<User>> GetAllUsers();
    Task<User?> GetUserById(int id);
    Task<User?> GetUserByUsername(string username);
    Task<bool> IsUniqueUser(string username);
    Task<User> CreateUser(User user);
}