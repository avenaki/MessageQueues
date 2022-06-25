using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry.Serdes;
using Env.FileWatcher;
using Shared.KafkaServices.Interfaces;

namespace Shared.KafkaServices.Classes;

public class KafkaProducerProvider : IProducerProvider
{
    private bool _disposed;

    public IProducer<DataCaptureService_0_Key, DataCaptureService_0_Value> Producer { get; }

    public KafkaProducerProvider(KafkaConfigurationProvider config, IKafkaSchemaProvider schemaProvider)
    {
        var avroSerializerConfig = new AvroSerializerConfig
        {
            BufferBytes = 1024
        };

        Producer = new ProducerBuilder<DataCaptureService_0_Key, DataCaptureService_0_Value>(config.ConsumerConfiguration)
            .SetKeySerializer(new AvroSerializer<DataCaptureService_0_Key>(schemaProvider.SchemaConfig, avroSerializerConfig).AsSyncOverAsync())
            .SetValueSerializer(new AvroSerializer<DataCaptureService_0_Value>(schemaProvider.SchemaConfig, avroSerializerConfig).AsSyncOverAsync())
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
            Producer.Dispose();
        }
        _disposed = true;
    }
}
