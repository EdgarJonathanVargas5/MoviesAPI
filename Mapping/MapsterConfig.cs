using Mapster;
using MoviesAPI.Models;
using MoviesAPI.Models.Dtos;

namespace MoviesAPI.Mapping;

public static class MapsterConfig
{
  public static void RegisterMappings()
  {
    TypeAdapterConfig<Movie, MovieDto>.NewConfig().TwoWays();
    TypeAdapterConfig<Movie, CreateMovieDto>.NewConfig().TwoWays();

    TypeAdapterConfig<Genre, GenreDto>.NewConfig().TwoWays();

    TypeAdapterConfig<User, UserDto>.NewConfig().TwoWays();
    TypeAdapterConfig<User, UserLoginDto>.NewConfig().TwoWays();
    TypeAdapterConfig<User, UserLoginResponseDto>.NewConfig().TwoWays();
    TypeAdapterConfig<User, UserRegisterDto>.NewConfig().TwoWays();
  }
}