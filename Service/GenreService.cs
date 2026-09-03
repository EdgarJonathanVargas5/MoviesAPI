using MapsterMapper;
using MoviesAPI.Models.Dtos;
using MoviesAPI.Repository.IRepository;
using MoviesAPI.Service.IService;
using MoviesAPI.Models;

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
    
    public async Task<GenreDto> CreateGenreAsync(CreateGenreDto createGenreDto)
    {
        if (createGenreDto == null)
            throw new ArgumentNullException(nameof(createGenreDto));

        if (string.IsNullOrWhiteSpace(createGenreDto.Name))
            throw new ArgumentException("El nombre es obligatorio.");

        if (await _genreRepository.GenreExists(createGenreDto.Name))
            throw new InvalidOperationException("La genero ya existe.");

        var genre = _mapper.Map<Genre>(createGenreDto);

        var result = await _genreRepository.CreateGenre(genre);

        if (!result)
            throw new Exception("No se pudo guardar el genero.");

        var genreDto = _mapper.Map<GenreDto>(genre);
        return genreDto;
    }

    public async Task<bool> UpdateGenreAsync(int id, CreateGenreDto updateGenreDto)
    {
        if (updateGenreDto == null)
            throw new ArgumentNullException(nameof(updateGenreDto));

        if (!await _genreRepository.GenreExists(id))
            return false;

        if (await _genreRepository.GenreExists(updateGenreDto.Name))
            throw new InvalidOperationException("El genero ya existe.");

        var genre = _mapper.Map<Genre>(updateGenreDto);
        genre.Id = id;

        return await _genreRepository.UpdateGenre(genre);
    }

    public async Task<bool> DeleteGenreAsync(int id)
    {
        if (!await _genreRepository.GenreExists(id))
        {
            return false;
        }
        var genre = await _genreRepository.GetGenreById(id);
        if (genre == null)
        {
            return false;
        }
        return await _genreRepository.DeleteGenre(genre);
    }
}
