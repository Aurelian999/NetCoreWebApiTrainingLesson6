using Microsoft.Extensions.DependencyInjection;
using StudentsAPI.V2.Services.Interfaces;
using System.Linq;

namespace StudentsAPI.V2.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStudentsService(this IServiceCollection services)
        {
            if (services.Any(sd => sd.ServiceType == typeof(IStudentsService)))
                return services;

            services.AddSingleton<V1.Services.IStudentsService, V1.Services.StudentsService>();

            services.AddSingleton<IStudentsGenerator, StudentsGenerator>();
            services.AddSingleton<IStudentsService, StudentsService>();

            return services;
        }
    }
}