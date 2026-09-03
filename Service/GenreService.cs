using MapsterMapper;
using MoviesAPI.Models.Dtos;
using MoviesAPI.Repository.IRepository;
using MoviesAPI.Service.IService;

namespace MoviesAPI.Service;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;
    public GenreService(IGenreRepository genreRepository, IMapper mapper)
    {
        _genreRepository = genreRepository;
        _mapper = mapper;
    }
    public async Task<ICollection<GenreDto>> GetAllGenresAsync()
    {
        var genres = await _genreRepository.GetAllGenres();
        var genresDto = _mapper.Map<List<GenreDto>>(genres);
        return genresDto;
    }

    public async Task<GenreDto?> GetGenreByIdAsync(int id)
    {
        var genre = await _genreRepository.GetGenreById(id);
        if (genre == null)
        {
            return null;
        }
        var genreDto = _mapper.Map<GenreDto>(genre);
        return genreDto;
    }
}
