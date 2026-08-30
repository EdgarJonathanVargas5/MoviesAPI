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
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _genreService.GetGenresAsync();
            return Ok(genres);
        }

        [HttpGet("{id:int}", Name = "GetGenre")]
        public async Task<IActionResult> GetGenre(int id)
        {
            var genre = await _genreService.GetGenreAsync(id);
            if (genre == null)
                return NotFound($"No se encontro el genero con el id {id}");

            return Ok(genre);
        }
    }
}
