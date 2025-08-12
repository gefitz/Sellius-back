using System.Reflection;

namespace Sellius.API.DI
{
    public class ServicesInjectoncs
    {
        public static void ServicesInjecao(Assembly assembly, IServiceCollection service)
        {
            var classes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service")).ToList();

            foreach (var t in classes)
            {
               service.AddScoped(t);
            }
        }
    }
}
