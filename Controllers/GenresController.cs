using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models.Dtos;
using MoviesAPI.Service.IService;

namespace MoviesAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class GenresController : ControllerBase
{
    private readonly IGenreService _genreService;
    public GenresController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllGenres()
    {
        var genres = await _genreService.GetAllGenresAsync();
        return Ok(genres);
    }

    [HttpGet("{id:int}", Name = "GetGenreById")]
    [AllowAnonymous]
    public async Task<IActionResult> GetGenreById(int id)
    {
        var genre = await _genreService.GetGenreByIdAsync(id);
        if (genre == null)
            return NotFound($"No se encontro el genero con el id {id}");

        return Ok(genre);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGenre([FromBody] CreateGenreDto createGenreDto)
    {
        if (createGenreDto == null || !ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var genreDto = await _genreService.CreateGenreAsync(createGenreDto);
            return CreatedAtRoute("GetGenreById", new { id = genreDto.Id }, genreDto);
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError("CustomError", ex.Message);
            return BadRequest(ModelState);
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError("CustomError", ex.Message);
            return BadRequest(ModelState);
        }
        catch (Exception)
        {
            ModelState.AddModelError("CustomError", "Algo salió mal al guardar el registro.");
            return StatusCode(500, ModelState);
        }
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateGenre(int id, [FromBody] CreateGenreDto updateGenreDto)
    {
        if (updateGenreDto == null || !ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var result = await _genreService.UpdateGenreAsync(id, updateGenreDto);
            if (!result)
            {
                return NotFound($"El genero con el id {id} no existe");
            }
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError("CustomError", ex.Message);
            return BadRequest(ModelState);
        }
        catch (Exception)
        {
            ModelState.AddModelError("CustomError", "Algo salió mal al actualizar el registro.");
            return StatusCode(500, ModelState);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        try
        {
            var result = await _genreService.DeleteGenreAsync(id);
            if (!result)
            {
                return NotFound($"El genero con el id {id} no existe");
            }
            return NoContent();
        }
        catch (Exception)
        {
            ModelState.AddModelError("CustomError", "Algo salió mal al eliminar el registro.");
            return StatusCode(500, ModelState);
        }
    }
}

