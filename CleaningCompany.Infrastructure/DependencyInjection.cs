using CleaningCompany.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using CleaningCompany.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Infrastructure.Identity;
using CleaningCompany.Infrastructure.Implementations;
using CleaningCompany.Infrastructure.Configuration;

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
            services.Configure<EmailSettings>(options =>
                configuration.GetSection(nameof(EmailSettings)).Bind(options));

            services
                .AddDefaultIdentity<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<User, ApplicationContext>();

            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IMaterialRepository, MaterialRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IOrderRequestRepository, OrderRequestRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IEmailService, EmailService>();


            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddAuthorization();

            return services;

        }
    }
}
