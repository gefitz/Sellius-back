using Microsoft.AspNetCore.Authorization;
using Sellius.API.Models.Usuario;
using System.Text.Json;

namespace Sellius.API.DI.Authentication
{
    public class ConfigHandler : AuthorizationHandler<ConfigRequeriment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ConfigRequeriment requirement)
        {
            var configClaim = context.User.FindFirst("configUsuario")?.Value;

            if (string.IsNullOrEmpty(configClaim))
                return Task.CompletedTask;

            var config = JsonSerializer.Deserialize<TpUsuarioConfiguracao>(configClaim);

            var prop = typeof(TpUsuarioConfiguracao)
                .GetProperty(requirement.Propriedade);

            if (prop == null)
                return Task.CompletedTask;

            var valor = prop.GetValue(config);

            if (valor is bool permitido && permitido)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
