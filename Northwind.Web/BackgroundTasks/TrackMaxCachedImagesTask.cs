using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Northwind.Web.BackgroundTasks
{
    internal class TrackMaxCachedImagesTask : IHostedService, IDisposable
{
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    private Timer _timer;

    public TrackMaxCachedImagesTask(
        ILogger<TrackMaxCachedImagesTask> logger,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Max cached-images-on-disk check is starting.");

        _timer = new Timer(DoWork, null, TimeSpan.Zero, 
            TimeSpan.FromMinutes(1));

        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {   
        int maxFilesNumber = Convert.ToInt32(
            !String.IsNullOrEmpty(_configuration["Cache:Image:FileStorage:Directory"]) ?
                _configuration["Cache:Image:FileStorage:Directory"] : "2");
        
        _logger.LogInformation("Timed Background Service is working.");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Timed Background Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
}

