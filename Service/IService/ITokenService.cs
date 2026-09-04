using MoviesAPI.Models;

namespace MoviesAPI.Service.IService;

public interface ITokenService
{
    string CreateToken(User user);
}