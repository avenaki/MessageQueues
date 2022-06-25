using Confluent.Kafka;
using Env.FileWatcher;

namespace Shared.KafkaServices.Interfaces;

public interface IConsumerProvider : IDisposable
{
    IConsumer<DataCaptureService_0_Key, DataCaptureService_0_Value> Consumer { get; }
}
