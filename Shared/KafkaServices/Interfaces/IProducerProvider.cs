using Confluent.Kafka;
using Env.FileWatcher;

namespace Shared.KafkaServices.Interfaces;

public interface IProducerProvider : IDisposable
{
    IProducer<DataCaptureService_0_Key, DataCaptureService_0_Value> Producer { get; }
}
