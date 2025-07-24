using Domain.Entities.Identity;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Identity;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. EF Core
            services.AddDbContext<AppDbContext>(opts =>
                opts.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sql => sql.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
            );

            // 2. ASP.NET Core Identity (if you haven’t done in Presentation layer)
            services.AddIdentityCore<ApplicationUser>(options => {
                // configure password, lockout, etc.
            })
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<AppDbContext>();
            //.AddDefaultTokenProviders();

            // 3. Repository registrations:
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();

            // 4. TokenService
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}