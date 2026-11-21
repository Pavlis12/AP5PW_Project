using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantApp.Domain.Interfaces;
using RestaurantApp.Infrastructure.Repositories;
using RestaurantApp.Infrastructure.Services;
using RestaurantApp.Infrastructure.Data;

namespace RestaurantApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext< RestaurantDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<RestaurantDbContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}