# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

All commands should be run from the solution root (`SistemaEstoque.API/`).

```bash
# Build
dotnet build

# Run API (listens on https://*:5000)
dotnet run --project SistemaEstoque.API/Sellius.API.csproj

# Run all tests
dotnet test Sellius.Test/Sellius.Test.csproj

# Run a single test class
dotnet test Sellius.Test/Sellius.Test.csproj --filter "FullyQualifiedName~ProdutoInsercao"

# Run tests by trait
dotnet test Sellius.Test/Sellius.Test.csproj --filter "Trait=Insercao"

# EF Core migrations (run from solution root)
dotnet ef migrations add <MigrationName> --project SistemaEstoque.API --startup-project SistemaEstoque.API
dotnet ef database update --project SistemaEstoque.API --startup-project SistemaEstoque.API
```

## Architecture

The project is a .NET 8 ASP.NET Core Web API undergoing a SOLID refactor. The solution contains two projects:

- `SistemaEstoque.API/` — main API project (`Sellius.API.csproj`)
- `Sellius.Test/` — xUnit test project (references the API project)

### Layer structure (inside `SistemaEstoque.API/`)

```
Domain/Entity/       — EF entity models (no business logic)
Application/
  DTOs/              — Request/response DTOs; also houses Response<T>
  Enums/             — Shared enums (TipoUsuario, TipoLicenca, OrigemTabelaPreco)
  Services/          — Business logic services
Infra/
  Context/           — AppDbContext + IEntityTypeConfiguration classes
  Repository/        — Concrete repository implementations
    Abstract/        — BaseRepository<T> (wraps DbSet and SaveChangesAsync)
    Interfaces/      — IDbMethods<T> and IPaginacao<model,filtro>
Controllers/         — API controllers
DI/                  — DI wiring and authorization handler
Utils/               — TokenService (JWT generation + claim helpers)
```

### Key patterns

**Auto-registration via reflection** — `Program.cs` calls two DI helpers:
- `RepositoryInjecton.RepositoryInjecao`: scans the assembly for any non-abstract class whose name ends with `Repository`. If a matching interface exists (same name), it registers by interface; otherwise registers the concrete type directly.
- `ServicesInjectoncs.ServicesInjecao`: scans for classes ending with `Service` and registers them as scoped by concrete type.

Naming matters: any new repository/service must follow the `*Repository` / `*Service` suffix convention to be auto-registered.

**Repository base** — `BaseRepository<T>` provides `DbConext` (a `DbSet<T>`) and `SaveChangesAsync()`. Concrete repos inherit from it and implement `IDbMethods<T>` (Create, Update, Delete, Filtrar, BuscaDireto) and/or `IPaginacao<model,filtro>` for paginated table queries.

**Response wrapper** — All service methods and controllers return `Response<T>`. Use `Response<T>.Ok(data)` / `Response<T>.Ok()` for success and `Response<T>.Failed("message")` for errors. Controllers check `response.success` and return `Ok(response)` or `BadRequest(response)`.

**JWT via cookie** — Authentication reads the JWT from the `auth_token` cookie (set at login, 1-day expiry). JWT claims include `id` (userId), `empresa` (companyId), `user` (name), `config` (JSON-serialised `UserConfiguration`), and `ClaimTypes.Role`. Controllers extract company/user ids with `TokenService.RecuperaIdEmpresa(User)` and `TokenService.RecuperaIdUsuario(User)`.

**Permission policies** — `ConfigHandler` reads the `config` claim and reflects into `UserConfiguration` to enforce policies: `podeCriar`, `podeEditar`, `podeExcluir`, `podeGerenciarUsuarios`, `podeInativar`, `podeAprovar`, `podeExportar`. Decorate actions with `[Authorize(Policy = "podeCriar")]` etc.

**EF configuration** — Entity configurations live under `Infra/Context/ConfigurationContext/` as `IEntityTypeConfiguration<T>` classes. `AppDbContext.OnModelCreating` applies all of them via `ApplyConfigurationsFromAssembly`. The context uses snake_case naming convention (`UseSnakeCaseNamingConvention`).

**CORS** — Configured for `http://localhost:4200` and `https://localhost:4200` (Angular frontend).

### Test structure

Tests use xUnit + Moq. Each domain area has an abstract `*Config` base class that wires up mocks and constructs the service under test. Test classes inherit from this config and add `[Fact]` methods with `[Trait("Category", "SubCategory")]` attributes.

Example: `Sellius.Test/Produtos/Configuracao/ProdutoConfig.cs` creates `Mock<IProdutoRepository>` and `ProdutoService`; `Sellius.Test/Produtos/Services/ProdutoInsercao.cs` inherits it and tests insertion.

## Database

PostgreSQL via Neon.tech (connection string in `appsettings.json`). EF Core uses `Npgsql` provider with snake_case column naming. A MySQL provider (`Pomelo`) is also referenced but not actively configured.
