using Application.Core.Interface;
using Application.Infrastructure.Persistence;
using Application.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Infrastructure
{
    public static  class Startup
    {
        public static IServiceCollection AddInfrastructre(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    connectionString,
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();

            return services;
        }
    }
}
