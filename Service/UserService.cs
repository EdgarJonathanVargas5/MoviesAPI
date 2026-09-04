using MapsterMapper;
using MoviesAPI.Models;
using MoviesAPI.Models.Dtos;
using MoviesAPI.Repository.IRepository;
using MoviesAPI.Service.IService;

namespace MoviesAPI.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, ITokenService tokenService, IMapper mapper)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    public async Task<UserLoginResponseDto> Login(UserLoginDto loginDto)
    {
        if (string.IsNullOrWhiteSpace(loginDto.Username))
        {
            return LoginError("El username es requerido");
        }

        var user = await _userRepository.GetUserByUsername(loginDto.Username);

        if (user is null)
        {
            return LoginError("Las credenciales son incorrectas");
        }

        var passwordIsValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
            
        if (!passwordIsValid)
        {
            return LoginError("Las credenciales son incorrectas");
        }

        return new UserLoginResponseDto
        {
            Token = _tokenService.CreateToken(user),
            User = MapToResponse(user),
            Message = "Inicio de sesión correcto"
        };
    }

    public async Task<User> Register(CreateUserDto createUserDto)
    {
        var username = createUserDto.Username!.Trim();

        var isUnique = await _userRepository.IsUniqueUser(username);

        if (!isUnique)
        {
            throw new InvalidOperationException("El username ya está registrado");
              
        }

        var user = new User
        {
            Username = username,
            Name = createUserDto.Name,
            Role = createUserDto.Role,
            Password = BCrypt.Net.BCrypt.HashPassword(
                createUserDto.Password)
        };

        return await _userRepository.CreateUser(user);
    }

    public async Task<ICollection<UserDto>> GetAllUserAsync()
    {
        var users = await _userRepository.GetAllUsers();
        var usersDto = _mapper.Map<List<UserDto>>(users);
        return usersDto;
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetUserById(id);
        if(user == null)
        {
            return null;
        }
        var userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }

    private static UserLoginResponseDto LoginError(string message)
    {
        return new UserLoginResponseDto
        {
            Token = string.Empty,
            User = null,
            Message = message
        };
    }

    private static UserResponseDto MapToResponse(User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            Name = user.Name,
            Role = user.Role
        };
    }

    
}