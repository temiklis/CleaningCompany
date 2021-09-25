using CleaningCompany.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleaningCompany.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("CleaningCompanyDB"),
                    b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));

            services.AddScoped<ApplicationContext>();

            return services;

        }
    }
}
