using CleaningCompany.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using CleaningCompany.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Infrastructure.Identity;

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

            services
                .AddDefaultIdentity<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<User, ApplicationContext>();

            services.AddTransient<IIdentityService, IdentityService>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            /*services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Administrator"));
            });*/

            return services;

        }
    }
}
