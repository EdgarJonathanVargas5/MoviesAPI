using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Repository.IRepository;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        [HttpGet]
        public IActionResult GetMovies()
        {
            var movies = _movieRepository.GetMovies();
            return Ok(movies);
        }
        [HttpGet("{id:int}", Name = "GetMovie")]
        public IActionResult GetMovie(int id)
        {
            var movie = _movieRepository.GetMovie(id);
            if(movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }
    }
}
