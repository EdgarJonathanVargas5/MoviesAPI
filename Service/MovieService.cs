using MapsterMapper;
using MoviesAPI.Models;
using MoviesAPI.Models.Dtos;
using MoviesAPI.Repository.IRepository;
using MoviesAPI.Service.IService;

namespace MoviesAPI.Service;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;
    public MovieService(IMovieRepository movieRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }

    public async Task<MovieDto?> GetMovieByIdAsync(int id)
    {
        var movie = await _movieRepository.GetMovieById(id);
        if(movie == null)
        {
            return null;
        }
        var movieDto = _mapper.Map<MovieDto>(movie);
        return movieDto;
    }

    public async Task<ICollection<MovieDto>> GetAllMoviesAsync()
    {
        var movies = await _movieRepository.GetAllMovies();
        var moviesDto = _mapper.Map<List<MovieDto>>(movies);
        return moviesDto;
    }

    public async Task<MovieDto> CreateMovieAsync(CreateMovieDto createMovieDto)
    {
        if (createMovieDto == null)
            throw new ArgumentNullException(nameof(createMovieDto));

        if (string.IsNullOrWhiteSpace(createMovieDto.Title))
            throw new ArgumentException("El título es obligatorio.");

        if (string.IsNullOrWhiteSpace(createMovieDto.Description))
            throw new ArgumentException("La descripción es obligatoria.");

        if (await _movieRepository.MovieExists(createMovieDto.Title))
            throw new InvalidOperationException("La pelicula ya existe.");

        var movie = _mapper.Map<Movie>(createMovieDto);

        var result = await _movieRepository.CreateMovie(movie);

        if (!result)
            throw new Exception("No se pudo guardar la película.");

        var movieDto = _mapper.Map<MovieDto>(movie);
        return movieDto;
    }

    public async Task<bool> UpdateMovieAsync(int id, CreateMovieDto updateMovieDto)
    {
        if (updateMovieDto == null)
            throw new ArgumentNullException(nameof(updateMovieDto));

        if (!await _movieRepository.MovieExists(id))
            return false;

        if (await _movieRepository.MovieExists(updateMovieDto.Title, id))
            throw new InvalidOperationException("La pelicula ya existe.");

        var movie = _mapper.Map<Movie>(updateMovieDto);
        movie.Id = id;

        return await _movieRepository.UpdateMovie(movie);
    }

    public async Task<bool> DeleteMovieAsync(int id)
    {
        if (!await _movieRepository.MovieExists(id))
        {
            return false;
        }
        var movie = await _movieRepository.GetMovieById(id);
        if (movie == null)
        {
            return false;
        }
        return await _movieRepository.DeleteMovie(movie);
    }
}
