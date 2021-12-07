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
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Linq;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using IdentityServer4.AccessTokenValidation;
using System.Threading.Tasks;

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
                .AddApiAuthorization<User, ApplicationContext>(options =>
                {
                    options.ApiResources.Add(GetApis().First());
                });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IMaterialRepository, MaterialRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IOrderRequestRepository, OrderRequestRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IEmailService, EmailService>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddAuthorization(options =>
                options.AddPolicy("CanPurge", policy => policy.RequireRole("Admin")));

            return services;

        }

        private static IEnumerable<ApiResource> GetApis()
        {

            return new List<ApiResource>
            {
                new ApiResource("CleaningCompany.API", "My Asp.net core WebApi,the best Webapi!"){
                    UserClaims =
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.Role,
                    }
                },
            };
        }
    }
}
