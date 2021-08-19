using Contoso.Gaming.Engine.API.Services;
using Contoso.Gaming.Engine.API.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Contoso.Gaming.Engine.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // consider creating in memory graph store as singleton
                    services.AddSingleton<IGraphService, GraphService>();
                    // transient == scope?
                    services.AddScoped<IPlayersLocatorService, PlayersLocatorService>();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
