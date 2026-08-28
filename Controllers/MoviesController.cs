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
        public IActionResult CreateMovie(CreateMovieDto createMovieDto)
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
    }
}
