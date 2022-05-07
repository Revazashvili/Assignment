using Application.Core.Interfaces;
using Application.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Infrastructure
{
    public static  class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
            services.AddScoped<IAppDbContext, AppDbContext>();
            return services;
        }
    }
}
