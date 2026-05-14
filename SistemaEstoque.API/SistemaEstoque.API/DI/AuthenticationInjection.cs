using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sellius.API.DI.Authentication;
using System.Text;

namespace Sellius.API.DI;

public static class AuthenticationInjection
{
    public static IServiceCollection AddAuthSetup(
        this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddJwtBearerAuthentication(configuration)
            .AddSwaggerSecurity()
            .ConfigureCookieRedirects()
            .AddPermissionPolicies();

    private static IServiceCollection AddJwtBearerAuthentication(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["jwt:issuer"],
                    ValidAudience = configuration["jwt:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["jwt:secretkey"]!))
                };
                opt.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("auth_token"))
                            context.Token = context.Request.Cookies["auth_token"];
                        return Task.CompletedTask;
                    }
                };
            });
        return services;
    }

    private static IServiceCollection AddSwaggerSecurity(this IServiceCollection services)
    {
        services.AddSwaggerGen(x =>
        {
            x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            x.AddSecurityRequirement(new OpenApiSecurityRequirement
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
        return services;
    }

    private static IServiceCollection ConfigureCookieRedirects(this IServiceCollection services)
    {
        services.ConfigureApplicationCookie(options =>
        {
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
        return services;
    }

    private static IServiceCollection AddPermissionPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("podeCriar",
                p => p.Requirements.Add(new ConfigRequeriment("flPodeCriar")));
            options.AddPolicy("podeEditar",
                p => p.Requirements.Add(new ConfigRequeriment("flPodeEditar")));
            options.AddPolicy("podeExcluir",
                p => p.Requirements.Add(new ConfigRequeriment("flPodeExcluir")));
            options.AddPolicy("podeGerenciarUsuarios",
                p => p.Requirements.Add(new ConfigRequeriment("flPodeGerenciarUsuarios")));
            options.AddPolicy("podeInativar",
                p => p.Requirements.Add(new ConfigRequeriment("flPodeInativar")));
            options.AddPolicy("podeAprovar",
                p => p.Requirements.Add(new ConfigRequeriment("flPodeAprovar")));
            options.AddPolicy("podeExportar",
                p => p.Requirements.Add(new ConfigRequeriment("flPodeExportar")));
        });
        return services;
    }
}
