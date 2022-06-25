using Microsoft.Extensions.Options;
using Shared.Models;
using Serilog;
using Shared.KafkaServices.Interfaces;
using Shared.Services.Interfaces.Interfaces;

namespace Shared.KafkaServices.Classes;

public class KafkaConsumer
{
    private readonly ILogger _logger;
    private readonly IOptions<KafkaOptions> _kafkaOptions;
    private readonly IConsumerProvider _consumerProvider;
    private readonly IMainProcessingService _mainProcessingService;

    public KafkaConsumer(IOptions<KafkaOptions> kafkaOptions,
                         IConsumerProvider consumerProvider,
                         ILogger logger,
                         IMainProcessingService mainProcessingService)
    {
        _kafkaOptions = kafkaOptions;
        _consumerProvider = consumerProvider;
        _logger = logger;
        _mainProcessingService = mainProcessingService;
    }

    public async Task Listen()
    {
        using var consumer = _consumerProvider.Consumer;
        consumer.Subscribe(_kafkaOptions.Value.Topic);

        while (true)
        {
            try
            {
                var consumeResult = consumer.Consume();
                var result = new Message(consumeResult.Message.Key, consumeResult.Message.Value);

                _mainProcessingService.AddMessage(result);
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
            }
        }
 
    }
}
