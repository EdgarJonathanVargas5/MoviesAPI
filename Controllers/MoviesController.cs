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
            var moviesDto = _mapper.Map<List<MovieDto>>(movies);
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
        
    }
}
