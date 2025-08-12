using Sellius.API.Models;
using Sellius.API.Repository.Interfaces;
using Sellius.API.Repository;
using Sellius.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Sellius.API.Context;
using Microsoft.OpenApi.Models;
using Sellius.API.Repository.Produto;
using Sellius.API.Repository.Produto.Interface;
using Sellius.API.Repository.Fornecedor.Interfaces;
using Sellius.API.Repository.Fornecedor;
using Sellius.API.Repository.Cliente;
using Sellius.API.Repository.Cliente.Interfaces;
using Sellius.API.Utils;
using Sellius.API.Repository.Login;
using Sellius.API.Repository.Login.Interfaces;
using Sellius.API.Repository.Pedidos;
using Sellius.API.Repository.Pedidos.Interfaces;
using Sellius.API.Repository.Empresa;
using Sellius.API.Repository.Usuarios;
using Sellius.API.Repository.Usuarios.Interfaces;
using Sellius.API.Repository.Empresa.Interface;
using Sellius.API.Repository.CidadeEstado;
using Newtonsoft.Json;
using Sellius.API.Services.Clientes;
using Sellius.API.Services.Produtos;
using System.Reflection;
using Sellius.API.DI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connection = builder.Configuration.GetConnectionString("ConnectionString");

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseMySql(connection, ServerVersion.AutoDetect(connection)));

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy",
        build =>
        {
            build.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
        });
});
builder.Services.AddAutoMapper(typeof(UsuarioModel));
var assembly = Assembly.GetExecutingAssembly();
#region Repository
RepositoryInjecton.RepositoryInjecao(assembly, builder.Services);

builder.Services.AddScoped<IDbMethods<LicencaModel>, LicencaRepository>();
builder.Services.AddScoped<LogRepository>();
builder.Services.AddScoped<IDbMethods<EstadoModel>, EstadoRespository>();
builder.Services.AddScoped<IDbMethods<CidadeModel>, CidadeRepository>();
#endregion

#region Services
ServicesInjectoncs.ServicesInjecao(assembly, builder.Services);
#endregion

builder.Services.AddControllers();

#region JWTBearer
builder.Services.AddAuthentication("Bearer")
.AddJwtBearer("Bearer", opt =>
{

    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["jwt:issuer"],
        ValidAudience = builder.Configuration["jwt:audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:secretkey"]))
    };
    opt.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            context.NoResult();
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            string response =
                JsonConvert.SerializeObject("The access token provided is not valid.");
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-Expired", "true");
                response =
                    JsonConvert.SerializeObject("Token is experied.");
            }

            context.Response.WriteAsync(response);
            return Task.CompletedTask;
        },
        OnChallenge = c => {
            c.HandleResponse();
            return Task.CompletedTask;
        }
    };
}
);
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {


        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header,
        },
        new List<string>()
        }

    });
});
#endregion


var app = builder.Build();

app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
