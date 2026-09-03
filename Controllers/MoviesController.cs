using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;
using MoviesAPI.Models.Dtos;
using MoviesAPI.Repository.IRepository;
using MoviesAPI.Service.IService;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            return Ok(movies);
        }

        [HttpGet("{id:int}", Name = "GetMovie")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if(movie == null)
            {
                return NotFound($"No se encontro la pelicula con el id {id}");
            }
            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromForm] CreateMovieDto createMovieDto)
        {
            if (createMovieDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var movieDto = await _movieService.CreateMovieAsync(createMovieDto);
                return CreatedAtRoute("GetMovie", new { id = movieDto.Id }, movieDto);
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
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] CreateMovieDto updateMovieDto)
        {
            if (updateMovieDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _movieService.UpdateMovieAsync(id, updateMovieDto);
                if (!result)
                {
                    return NotFound($"La pelicula con el id {id} no existe");
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
        public async Task<IActionResult> DeleteMovie(int id)
        {
            try
            {
                var result = await _movieService.DeleteMovieAsync(id);
                if (!result)
                {
                    return NotFound($"La pelicula con el id {id} no existe");
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
}
