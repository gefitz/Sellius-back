using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Sellius.API.Context;
using Sellius.API.DI;
using Sellius.API.Models;
using Sellius.API.Models.Empresa;
using Sellius.API.Repository;
using Sellius.API.Repository.CidadeEstado;
using Sellius.API.Repository.Cliente;
using Sellius.API.Repository.Cliente.Interfaces;
using Sellius.API.Repository.Empresa;
using Sellius.API.Repository.Empresa.Interface;
using Sellius.API.Repository.Fornecedor;
using Sellius.API.Repository.Fornecedor.Interfaces;
using Sellius.API.Repository.Interfaces;
using Sellius.API.Repository.Login;
using Sellius.API.Repository.Login.Interfaces;
using Sellius.API.Repository.Pedidos;
using Sellius.API.Repository.Pedidos.Interfaces;
using Sellius.API.Repository.Produto;
using Sellius.API.Repository.Produto.Interface;
using Sellius.API.Repository.Usuarios;
using Sellius.API.Repository.Usuarios.Interfaces;
using Sellius.API.Services;
using Sellius.API.Services.Clientes;
using Sellius.API.Services.Produtos;
using Sellius.API.Utils;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connection = builder.Configuration.GetConnectionString("ConnectionString");

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connection).UseSnakeCaseNamingConvention());

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy",
        build =>
        {
            build.WithOrigins("http://localhost:4200", "https://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
        });
});
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
builder.Services
    .AddAuthentication("Bearer")

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
        OnMessageReceived = context =>
        {
            // Pega o token do cookie chamado "auth_token"

            if (context.Request.Cookies.ContainsKey("auth_token"))
            {
                context.Token = context.Request.Cookies["auth_token"];
            }

            // Opcional: também tenta pegar do header Authorization (caso queira suportar os dois)
            // var bearer = context.Request.Headers.Authorization.FirstOrDefault();
            // if (!string.IsNullOrEmpty(bearer) && bearer.StartsWith("Bearer "))
            //     context.Token = bearer["Bearer ".Length..].Trim();

            return Task.CompletedTask;
        },

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


builder.Services.ConfigureApplicationCookie(options =>
{
    // Quando a autenticação falhar, em vez de redirecionar para /Account/Login,
    // retorna 401 ou 403 sem redirecionar
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };

    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("podeCriar", p => p.RequireClaim("flPodeCriar", "True"));

    options.AddPolicy("podeEditar", p =>  p.RequireClaim("flPodeEditar", "True"));

    options.AddPolicy("podeExcluir", p => p.RequireClaim("flPodeExcluir", "True"));

    options.AddPolicy("podeGerenciarUsuarios", p => p.RequireClaim("flPodeGerenciarUsuarios", "True"));
    
    options.AddPolicy("podeInativar", p => p.RequireClaim("flPodeInativar", "True"));
    
    options.AddPolicy("podeAprovar", p => p.RequireClaim("flPodeAprovar", "True"));

    options.AddPolicy("podeExportar", p => p.RequireClaim("flPodeExportar", "True"));

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
