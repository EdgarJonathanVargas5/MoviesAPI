using Mapster;
using MoviesAPI.Extensions;
using MoviesAPI.Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddMapster();
builder.Services.AddSwaggerDocumentation();

MapsterConfig.RegisterMappings();

builder.Services.AddControllers();

var app = builder.Build();

app.UseSwaggerDocumentation();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();