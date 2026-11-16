using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace RestaurantApp.Infrastructure
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("Customer"))
            {
                await roleManager.CreateAsync(new IdentityRole("Customer"));
            }

            string adminEmail = "admin@restaurace.cz";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            var context = serviceProvider.GetRequiredService<RestaurantApp.Infrastructure.Data.RestaurantDbContext>();
            var categories = context.Set<RestaurantApp.Domain.Entities.MenuCategory>();

            if (!categories.Any())
            {
                categories.AddRange(
                    new RestaurantApp.Domain.Entities.MenuCategory { Name = "Předkrmy" },
                    new RestaurantApp.Domain.Entities.MenuCategory { Name = "Hlavní jídla" },
                    new RestaurantApp.Domain.Entities.MenuCategory { Name = "Dezerty" },
                    new RestaurantApp.Domain.Entities.MenuCategory { Name = "Nápoje" }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}