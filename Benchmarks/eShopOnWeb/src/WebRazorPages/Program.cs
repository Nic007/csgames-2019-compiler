using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace Microsoft.eShopWeb.RazorPages
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args) // @issue@I02
                        .Build(); // @issue@I02

            using (var scope = host.Services.CreateScope()) // @issue@I02
            {
                var services = scope.ServiceProvider; // @issue@I02
                var loggerFactory = services.GetRequiredService<ILoggerFactory>(); // @issue@I02
                try
                {
                    var catalogContext = services.GetRequiredService<CatalogContext>(); // @issue@I02
                    CatalogContextSeed.SeedAsync(catalogContext, loggerFactory) // @issue@I02
            .Wait(); // @issue@I02

                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>(); // @issue@I02
                    AppIdentityDbContextSeed.SeedAsync(userManager).Wait(); // @issue@I02
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>(); // @issue@I02
                    logger.LogError(ex, "An error occurred seeding the DB."); // @issue@I02
                }
            }

            host.Run(); // @issue@I02
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) => // @issue@I02
            WebHost.CreateDefaultBuilder(args) // @issue@I02
                .UseUrls("http://0.0.0.0:5107") // @issue@I02
                .UseStartup<Startup>(); // @issue@I02
    }
}
