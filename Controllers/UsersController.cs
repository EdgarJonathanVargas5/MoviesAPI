using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models.Dtos;
using MoviesAPI.Service.IService;

namespace MoviesAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUserAsync();
        return Ok(users);
    }

    [HttpGet("{id:int}", Name = "GetUserById")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if(user == null)
        {
            return NotFound($"El usuario con el id {id} no existe");
        }
        return Ok(user);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterUser([FromBody] CreateUserDto createUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var user = await _userService.Register(createUserDto);
           return CreatedAtRoute("GetUserById", new { id = user.Id },
                        new UserResponseDto
                        {
                            Id = user.Id,
                            Username = user.Username,
                            Name = user.Name,
                            Role = user.Role
                        });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new
            {
                Message = ex.Message
            });
        }
    }

    [HttpPost("login", Name = "LoginUser")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginUser([FromBody] UserLoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var response = await _userService.Login(loginDto);

        if (string.IsNullOrEmpty(response.Token))
        {
            return Unauthorized(response);
        }
        return Ok(response);
    }
}