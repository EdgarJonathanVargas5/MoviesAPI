using MoviesAPI.Repository;
using MoviesAPI.Repository.IRepository;
using MoviesAPI.Service;
using MoviesAPI.Service.IService;

namespace MoviesAPI.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IGenreService, GenreService>();

        return services;
    }
}
