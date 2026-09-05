 # MoviesAPI

API REST desarrollada con ASP.NET Core para administrar películas, géneros y usuarios.

## Tecnologías

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- MySQL
- JWT Bearer
- BCrypt
- Mapster
- Swagger
- Docker Compose

## Funcionalidades

- Crear, consultar, actualizar y eliminar películas.
- Crear, consultar, actualizar y eliminar géneros.
- Registrar usuarios e iniciar sesión.
- Autenticación mediante JWT.
- Autorización para usuarios con rol `Admin`.
- Persistencia de datos en MySQL.
- Documentación mediante Swagger.

## Configuración

En `appsettings.json` se deben configurar la clave JWT y la conexión a MySQL:

```json
{
	"ApiSettings": {
		"SecretKey": "TU_SECRET_KEY"
	},
	"ConnectionStrings": {
		"DefaultConnection": "server=localhost;port=3306;database=movies_db;user=TU_USER;password=TU_PASSWORD"
	}
}
```

## Ejecución

```bash
dotnet restore
dotnet ef database update
dotnet run
```

La API estará disponible en:

```text
http://localhost:5106
https://localhost:7245
```

Swagger:

```text
https://localhost:7245/swagger
```

## Docker

Para iniciar MySQL y phpMyAdmin:

```bash
docker compose up -d
```

phpMyAdmin estará disponible en:

```text
http://localhost:8080
```

## Estructura

```text
Controllers/   Endpoints de la API
Data/          Contexto de Entity Framework
Models/        Entidades y DTOs
Repository/    Acceso a datos
Service/       Lógica de negocio
Extensions/    Configuraciones
Migrations/    Migraciones de la base de datos
Mapping/       Configuración de Mapster
```