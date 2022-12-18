using System.Diagnostics;
using Imagination.DataLayer;
using Imagination.DataLayer.UploadService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Context.Propagation;
using Serilog;

namespace Imagination
{
    internal static class Program
    {
        internal static readonly ActivitySource Telemetry = new ("Server");

        private static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
           .WriteTo.Console()
           .WriteTo.File("logs/ImagtinationServer.txt", rollingInterval: RollingInterval.Day)
           .CreateBootstrapLogger();

            Log.Information("Main Logger Starting up");

            OpenTelemetry.Sdk.SetDefaultTextMapPropagator(new B3Propagator());

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
