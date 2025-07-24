using Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Presentation.Api.IntegrationTests
{
    public class WebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Replace real DbContext with InMemory for tests
                var descriptor = services.Single(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                services.Remove(descriptor);

                //services.AddDbContext<AppDbContext>(opts =>
                //{
                //    opts.UseInMemoryDatabase("TestDb");
                //});

                //// Ensure DB is created
                //var sp = services.BuildServiceProvider();
                //using var scope = sp.CreateScope();
                //var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                //db.Database.EnsureCreated();
            });
        }
    }
}