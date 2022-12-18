using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Caching;
using Imagination.DataLayer;
using Imagination.DataLayer.UploadService;
using Imagination.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using OpenTelemetry.Context.Propagation;
using Serilog;

namespace Imagination
{
    internal static class Program
    {
        internal static readonly ActivitySource Telemetry = new("Server");

        private static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
           .WriteTo.Console()
           .WriteTo.File("logs/ImagtinationServer.txt", rollingInterval: RollingInterval.Day)
           .CreateBootstrapLogger();

            Log.Information("Main Logger Starting up");

            OpenTelemetry.Sdk.SetDefaultTextMapPropagator(new B3Propagator());

            ObjectCache cache = System.Runtime.Caching.MemoryCache.Default;
            string fileContents = cache["filecontents"] as string;

            if (fileContents == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                List<string> filePaths = new List<string>();
                filePaths.Add("Configuration/Config.json");
                policy.ChangeMonitors.Add(new
                HostFileChangeMonitor(filePaths));
                // Fetch the file contents.  
                fileContents =
                    File.ReadAllText("Configuration/Config.json");

                cache.Set("ConfigRules", fileContents, policy);
            }

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddDbContext<ImaginationContext>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddScoped<IUploadServiceRepository, UploadServiceRepository>();
            var app = builder.Build();
            app.MapControllers();
            app.Run();
            CreateHostBuilder(args).Build().Run();

        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
