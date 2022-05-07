using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            // MapperConfig.ConfigureMapper();
            return services;
        }
    }
}
