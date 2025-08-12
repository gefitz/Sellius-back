using System.Reflection;

namespace Sellius.API.DI
{
    public class RepositoryInjecton
    {
        public static void RepositoryInjecao(Assembly assembly, IServiceCollection service)
        {
            var classes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository")).ToList();

            foreach (var t in classes)
            {
                var interfaces = t.GetInterfaces();
                var expectedInterfaceName = $"I{t.Name}";
                var interfaceAlvo = interfaces.FirstOrDefault(i => i.Name == expectedInterfaceName);
                if (interfaceAlvo != null && !service.Any(s => s.ServiceType == interfaceAlvo))
                {
                    service.AddScoped(interfaceAlvo, t);
                }
            }
        }
    }
}
