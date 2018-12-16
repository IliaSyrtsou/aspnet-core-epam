using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Northwind
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            ConfigureLogging(config);
            try
            {
                Log.Information("Starting web host");
                CreateWebHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>();


        private static void ConfigureLogging(IConfigurationRoot config) {
            var logTarget = config.GetValue<string>("Logging:LogTarget");
            logTarget = string.IsNullOrEmpty(logTarget) ? "Console": logTarget;

            var fileName = config.GetValue<string>("Logging:Options:Filename");
            fileName = string.IsNullOrEmpty(fileName) ? "log.txt": fileName;

            var enabled = config.GetValue<bool>("Logging:Enabled");
            var minimumLevel = config.GetValue<LogEventLevel>("Logging:LogLevel:Default");
            var ls = new LoggingLevelSwitch();
            ls.MinimumLevel = minimumLevel;
            var loggerConfig = new LoggerConfiguration()
                .Filter.ByExcluding(_ => !enabled)
                .MinimumLevel.ControlledBy(ls)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext();
            loggerConfig = 
                logTarget.ToLower().Equals("file") ? 
                loggerConfig.WriteTo.File(fileName, rollingInterval: RollingInterval.Day) :
                loggerConfig.WriteTo.Console();
            Log.Logger = loggerConfig.CreateLogger();
        }
    }
}
