using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;

namespace LinQDemo.Service
{
    public static class StartupExtensions
    {
        public static void ServiceLayerStartup(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetExecutingAssembly())
                .Where(c => c.Name.EndsWith("Service") || c.Name.EndsWith("ServiceAsync"))
                .AsPublicImplementedInterfaces();
        }
    }
}