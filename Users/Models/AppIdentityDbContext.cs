using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace Users.Models
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {

        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options) { }

        public class ApplicationDbContextFactory
        : IDesignTimeDbContextFactory<AppIdentityDbContext>
        {
            public AppIdentityDbContext CreateDbContext(string[] args) =>
                Program.BuildWebHost(args).Services
                    .GetRequiredService<AppIdentityDbContext>();
        }
    }
}
