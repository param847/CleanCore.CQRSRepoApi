using Domain.Entities.Identity;
using Domain.Entities.Logging;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts
{
    /// <summary>
    /// EF Core DbContext including ASP.NET Identity.
    /// </summary>
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        // Add DbSet for our other aggregates:

        /// <summary>
        /// Table for storing global exception logs.
        /// </summary>
        public DbSet<ExceptionLog> ExceptionLogs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}