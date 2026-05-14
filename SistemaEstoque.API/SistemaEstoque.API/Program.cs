using Microsoft.EntityFrameworkCore;
using Sellius.API.DI;
using Sellius.API.Infra.Context;
using Sellius.API.Infra.Repository;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("ConnectionString");

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(connection).UseSnakeCaseNamingConvention());

builder.Services.AddCors(opt =>
    opt.AddPolicy("CorsPolicy", build =>
        build.WithOrigins("http://localhost:4200", "https://localhost:4200")
             .AllowAnyHeader()
             .AllowAnyMethod()
             .AllowCredentials()));

builder.Services.AddRepository();
builder.Services.AddServices();
builder.Services.AddMapper();
builder.Services.AddAuthSetup(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

var app = builder.Build();

app.UseApiPipeline();

app.Run();
