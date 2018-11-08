using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Northwind.Web.BackgroundTasks
{
    internal class TrackMaxCachedImagesService : IHostedService, IDisposable
{
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    private Timer _timer;

    public TrackMaxCachedImagesService(
        ILogger<TrackMaxCachedImagesService> logger,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("TrackMaxCachedImagesService is initializing...");

        _timer = new Timer(DoWork, null, TimeSpan.Zero, 
            TimeSpan.FromSeconds(30));

        _logger.LogInformation("...Done.");

        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
        int maxFilesNumber;

        _logger.LogInformation("Max cached files check started.");
        try {
            maxFilesNumber = Convert.ToInt32(_configuration["Cache:Image:FileStorage:MaxFilesNumber"]);
        } 
        catch {
            maxFilesNumber = 2;
        }
        
        var directory = _configuration["Cache:Image:FileStorage:Directory"];

        if(Directory.Exists(directory)) {
            var filesToDelete = new DirectoryInfo(directory)
                    .GetFiles()
                    .OrderByDescending(x => x.LastWriteTime)
                    .Skip(maxFilesNumber);
            
            foreach (var fi in filesToDelete) 
            {
                fi.Delete();
            }
        }
        _logger.LogInformation("Max cached files check finished.");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("TrackMaxCachedImagesService is stopping...");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
}

