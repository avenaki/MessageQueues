using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry.Serdes;
using Env.FileWatcher;
using Shared.KafkaServices.Interfaces;

namespace Shared.KafkaServices.Classes;

public class KafkaConsumerProvider : IConsumerProvider
{
    private bool _disposed;

    public IConsumer<DataCaptureService_0_Key, DataCaptureService_0_Value> Consumer { get; }

    public KafkaConsumerProvider(KafkaConfigurationProvider config, IKafkaSchemaProvider schemaProvider)
    {
        Consumer = new ConsumerBuilder<DataCaptureService_0_Key, DataCaptureService_0_Value>(config.ConsumerConfiguration)
            .SetKeyDeserializer(new AvroDeserializer<DataCaptureService_0_Key>(schemaProvider.SchemaConfig).AsSyncOverAsync())
            .SetValueDeserializer(new AvroDeserializer<DataCaptureService_0_Value>(schemaProvider.SchemaConfig).AsSyncOverAsync())
            .Build();
    }

    public void Dispose()
    {
        Dispose(true);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            Consumer.Dispose();
        }
        _disposed = true;
    }
}
