using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using Sellius.API.Domain.Entity.EntityUsers;

namespace Sellius.API.DI.Authentication
{
    public class ConfigHandler : AuthorizationHandler<ConfigRequeriment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ConfigRequeriment requirement)
        {
            var configClaim = context.User.FindFirst("config")?.Value;

            if (string.IsNullOrEmpty(configClaim))
                return Task.CompletedTask;

            var config = JsonSerializer.Deserialize<UserConfiguration>(configClaim);

            var prop = typeof(UserConfiguration)
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
