using Microsoft.AspNetCore.Authorization;

namespace Sellius.API.DI.Authentication
{
    public class ConfigRequeriment : IAuthorizationRequirement
    {
        public string Propriedade { get; }

        public ConfigRequeriment(string propriedade) { Propriedade = propriedade; }
    }
}
