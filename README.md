# dotnet-webapi-sqlite-sample
Sample ASP.NET Core Web API using Clean Architecture principles, layered structure, Entity Framework Core, and SQLite for simple product CRUD operations.

## Prerequisites

- .NET 8 SDK
- Entity Framework Core CLI

```bash
dotnet tool install --global dotnet-ef
```

## Running the application

Restore packages:

```bash
dotnet restore
```

Create the database:

```bash
dotnet ef migrations add InitialCreate --project ProductAPI.Infrastructure --startup-project ProductAPI.API

dotnet ef database update --project ProductAPI.Infrastructure --startup-project ProductAPI.API
```

Run the API:

```bash
dotnet run --project ProductAPI.API
```