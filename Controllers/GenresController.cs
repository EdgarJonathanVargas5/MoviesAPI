using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models.Dtos;
using MoviesAPI.Service.IService;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genreService.GetAllGenresAsync();
            return Ok(genres);
        }

        [HttpGet("{id:int}", Name = "GetGenreById")]
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
    }
}
