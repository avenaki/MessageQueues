using Microsoft.Extensions.Hosting;
using Shared.KafkaServices.Classes;

namespace MainProcessingService;

internal class HostedService : IHostedService
{
    private readonly KafkaConsumer _kafkaConsumer;

    public HostedService(KafkaConsumer kafkaConsumer)
    {
        _kafkaConsumer = kafkaConsumer;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(() => _kafkaConsumer.Listen());
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

}
