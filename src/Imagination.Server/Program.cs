using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
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
         .WriteTo.File("logs/ShCartLogs.txt", rollingInterval: RollingInterval.Day)
         .CreateBootstrapLogger();

            Log.Information("Main Logger Starting up");

            OpenTelemetry.Sdk.SetDefaultTextMapPropagator(new B3Propagator());
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
