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
            string ConfigContents = cache["ConfigContents"] as string;
            string FileRuleContents = cache["FileRuleContents"] as string;

            if (ConfigContents == null)
            {
                CacheItemPolicy ConfigContentPolicy = new CacheItemPolicy();
                List<string> filePaths = new List<string>();
                filePaths.Add("Configuration/Config.json");
                ConfigContentPolicy.ChangeMonitors.Add(new HostFileChangeMonitor(filePaths));
                ConfigContents =File.ReadAllText("Configuration/Config.json");
                var ConfigRuleContents = JsonConvert.DeserializeObject<List<ConfigEntity>>(ConfigContents);
                cache.Add("ConfigRules", ConfigRuleContents, ConfigContentPolicy);

                CacheItemPolicy FileRulePolicy = new CacheItemPolicy();
                filePaths.Clear();
                filePaths.Add("Configuration/FileRules.json");
                FileRuleContents = File.ReadAllText("Configuration/FileRules.json");
                var ImageFileRules = JsonConvert.DeserializeObject<List<ConfigEntity>>(FileRuleContents);
                cache.Add("FileRules", ImageFileRules, FileRulePolicy);
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
