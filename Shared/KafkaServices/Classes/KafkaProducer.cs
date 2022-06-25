using Confluent.Kafka;
using Env.FileWatcher;
using Microsoft.Extensions.Options;
using Serilog;
using Shared.Models;
using Shared.KafkaServices.Interfaces;

namespace Shared.KafkaServices.Classes;

public class KafkaProducer : IKafkaProducer
{
    private static ILogger _logger;
    private readonly IProducerProvider _producerProvider;
    private readonly IOptions<KafkaOptions> _kafkaOptions;

    public KafkaProducer(IProducerProvider producerProvider,
                         IOptions<KafkaOptions> kafkaOptions,
                         ILogger logger)
    {
        _kafkaOptions = kafkaOptions;
        _logger = logger;
        _producerProvider = producerProvider;
    }

    public async void ProduceMessage(Message message)
    {
        try
        {
            var producer = _producerProvider.Producer;
            await producer.ProduceAsync(_kafkaOptions.Value.Topic, new Message<DataCaptureService_0_Key, DataCaptureService_0_Value> { Key = message.Key, Value = message.Value });
            producer.Flush();
        }

        catch (Exception e)
        {
            _logger.Error(e.ToString());
            _logger.Error($"Position:{message.Value.Position}, Content Size: {message.Value.Content.Length}");
        }

    }
}
