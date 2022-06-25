using Confluent.SchemaRegistry;

namespace Shared.KafkaServices.Interfaces;

public interface IKafkaSchemaProvider : IDisposable
{
    CachedSchemaRegistryClient SchemaConfig { get; }
}
