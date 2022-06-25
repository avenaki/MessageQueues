using DataCaptureService.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DataCaptureService
{
    internal class HostedService : IHostedService
    {
        IDataCaptureService _dataCaptureService;
        ILogger _logger;

        public HostedService(IDataCaptureService dataCaptureService,
                             ILogger logger)
        {
            _dataCaptureService = dataCaptureService;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() => _dataCaptureService.MonitorFileFolder());
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
