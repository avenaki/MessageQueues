using Confluent.SchemaRegistry;
using Microsoft.Extensions.Options;
using Shared.KafkaServices.Interfaces;

namespace Shared.KafkaServices.Classes;

public class KafkaSchemaProvider : IKafkaSchemaProvider
{
    public CachedSchemaRegistryClient SchemaConfig { get; }
    private bool _disposed;

    public KafkaSchemaProvider(IOptions<KafkaOptions> kafkaOptions)
    {
        var schemaRegistryConfig = new SchemaRegistryConfig
        {
            Url = kafkaOptions.Value.Schema_Server,
            BasicAuthCredentialsSource = AuthCredentialsSource.UserInfo,
            BasicAuthUserInfo = $"{kafkaOptions.Value.Schema_Api_Key}:{kafkaOptions.Value.Schema_Api_Secret}",
        };
        SchemaConfig = new CachedSchemaRegistryClient(schemaRegistryConfig);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            SchemaConfig.Dispose();
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
    }
}
