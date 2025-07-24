using Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Presentation.Api.IntegrationTests
{
    /// <summary>
    /// Spins up the API but swaps in an InMemory DB for AppDbContext.
    /// </summary>
    public class CustomWebAppFactory
        : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // 1. Remove the real AppDbContext registration
                var descriptor = services
                    .SingleOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                //// 2. Register AppDbContext using InMemory
                //services.AddDbContext<AppDbContext>(options =>
                //{
                //    options.UseInMemoryDatabase("TestDb");
                //});

                //// 3. Ensure the DB is created
                //var sp = services.BuildServiceProvider();
                //using var scope = sp.CreateScope();
                //var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();
            });
        }
    }
}