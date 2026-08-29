using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;
using MoviesAPI.Models.Dtos;
using MoviesAPI.Repository.IRepository;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MoviesController(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetMovies()
        {
            var movies = _movieRepository.GetMovies();
            var moviesDto = _mapper.Map<List<MovieDto>>(movies).OrderBy(m => m.Id);
            return Ok(moviesDto);
        }

        [HttpGet("{id:int}", Name = "GetMovie")]
        public IActionResult GetMovie(int id)
        {
            var movie = _movieRepository.GetMovie(id);
            if(movie == null)
            {
                return NotFound();
            }
            var movieDto = _mapper.Map<MovieDto>(movie);
            return Ok(movieDto);
        }

        [HttpPost]
        public IActionResult CreateMovie([FromBody]CreateMovieDto createMovieDto)
        {
            if(createMovieDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_movieRepository.MovieExists(createMovieDto.Title))
            {
                ModelState.AddModelError("CustomError", "La pelicula ya existe");
                return BadRequest(ModelState);
            }
            var movie = _mapper.Map<Movie>(createMovieDto);
            if(!_movieRepository.CreateMovie(movie))
            {
                ModelState.AddModelError("CustomError", $"Algo salió mal al guardar el registro {movie.Title}");
                return StatusCode(500, ModelState);
            }
            var movieDto = _mapper.Map<MovieDto>(movie);

            return CreatedAtRoute(
                "GetMovie",
                new { id = movie.Id },
                movieDto);
        }

        [HttpPatch("{id:int}")]
        public IActionResult UpdateMovie(int id, [FromBody] CreateMovieDto updateMovieDto)
        {
            if(!_movieRepository.MovieExists(id))
            {
                return NotFound();
            }
            if(updateMovieDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_movieRepository.MovieExists(updateMovieDto.Title, id))
            {
                ModelState.AddModelError("CustomError", "La pelicula ya existe");
                return BadRequest(ModelState);
            }
            var movie = _mapper.Map<Movie>(updateMovieDto);
            movie.Id = id;
            if(!_movieRepository.UpdateMovie(movie))
            {
                ModelState.AddModelError("CustomError", $"Algo salió mal al actualizar el registro {movie.Title}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteMovie(int id)
        {
            if (!_movieRepository.MovieExists(id))
            {
                return NotFound($"La pelicula con el id {id} no existe");
            }
            var movie = _movieRepository.GetMovie(id);
            if (movie == null)
            {
                return NotFound($"La pelicula con el id {id} no existe");
            }

            if (!_movieRepository.DeleteMovie(movie))
            {
                ModelState.AddModelError("CustomError", $"Algo salió mal al eliminar el registro {movie.Title}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
