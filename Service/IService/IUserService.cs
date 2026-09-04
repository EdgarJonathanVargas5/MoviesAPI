using MoviesAPI.Models;
using MoviesAPI.Models.Dtos;

namespace MoviesAPI.Service.IService;

public interface IUserService
{
    Task<ICollection<UserDto>> GetAllUserAsync();
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserLoginResponseDto> Login(UserLoginDto loginDto);
    Task<User> Register(CreateUserDto createUserDto);
}