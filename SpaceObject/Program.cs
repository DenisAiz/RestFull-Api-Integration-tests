using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpaceObjectApi.Models;
using System.Threading.Tasks;

namespace SpaceObjectApi
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var db = scope.ServiceProvider.GetRequiredService<SpaceObjectContext>();
                db.Database.Migrate();
                SeedData.Initialize(services);
            }

            return host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
