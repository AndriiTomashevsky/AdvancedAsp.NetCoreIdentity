using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Users.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SportsStore.Models
{
    public static class SeedData
    {
        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            AppIdentityDbContext context = app.ApplicationServices
                .GetRequiredService<AppIdentityDbContext>();
            UserManager<AppUser> userManager = app.ApplicationServices
                .GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> roleManager = app.ApplicationServices
                .GetRequiredService<RoleManager<IdentityRole>>();

            context.Database.Migrate();

            if (!context.Users.AnyAsync().Result)
            {
                AppUser joe = new AppUser { UserName = "Joe", Email = "joe@example.com", };
                AppUser alice = new AppUser { UserName = "Alice", Email = "alice@example.com", };
                AppUser bob = new AppUser { UserName = "Bob", Email = "bob@example.com", };
                AppUser admin = new AppUser { UserName = "Admin", Email = "admin@example.com", };

                IdentityResult resultAdmin1 = userManager.CreateAsync(admin, "secret").Result;
                IdentityResult resultUser1 = userManager.CreateAsync(joe, "secret").Result;
                IdentityResult resultUser2 = userManager.CreateAsync(alice, "secret").Result;
                IdentityResult resultUser3 = userManager.CreateAsync(bob, "secret").Result;

                IdentityResult resultRole1 = await roleManager.CreateAsync(new IdentityRole("Admins"));
                IdentityResult resultRole2 = await roleManager.CreateAsync(new IdentityRole("Users"));
                IdentityResult resultRole3 = await roleManager.CreateAsync(new IdentityRole("Employees"));


                if (resultRole1.Succeeded && resultRole2.Succeeded && resultRole3.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admins");
                    await userManager.AddToRolesAsync(alice, new[] { "Users", "Employees" });
                    await userManager.AddToRoleAsync(bob, "Employees");
                    await userManager.AddToRoleAsync(joe, "Users");
                }

                bool isInRole = await userManager.IsInRoleAsync(alice, "Employees");
            }
            await context.SaveChangesAsync();
        }
    }
}
