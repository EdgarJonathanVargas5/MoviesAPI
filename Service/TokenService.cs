using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MoviesAPI.Models;
using MoviesAPI.Service.IService;

namespace MoviesAPI.Service;

public class TokenService : ITokenService
{
    private readonly string _secretKey;

    public TokenService(IConfiguration configuration)
    {
        _secretKey = configuration["ApiSettings:SecretKey"]
            ?? throw new InvalidOperationException("ApiSettings:SecretKey no está configurada");
    }

    public string CreateToken(User user)
    {
        var key = Encoding.UTF8.GetBytes(_secretKey);
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new[]
        {
            new Claim("id", user.Id.ToString()),
            new Claim("username", user.Username),
            new Claim(
                ClaimTypes.Role,
                user.Role ?? string.Empty)
        };

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(descriptor);

        return tokenHandler.WriteToken(token);
    }
}