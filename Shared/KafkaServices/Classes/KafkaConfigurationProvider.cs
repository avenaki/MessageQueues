using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace Shared.KafkaServices.Classes;

public class KafkaConfigurationProvider
{
    public ConsumerConfig ConsumerConfiguration { get; }

    public KafkaConfigurationProvider(IOptions<KafkaOptions> kafkaOptions)
    {
        var kafka = kafkaOptions.Value;
        ConsumerConfiguration = new ConsumerConfig
        {
            BootstrapServers = kafka.Bootstrap_Server,
            GroupId = kafka.Client_Id,
            SaslMechanism = SaslMechanism.Plain,
            SecurityProtocol = SecurityProtocol.Plaintext,
            SslEndpointIdentificationAlgorithm = SslEndpointIdentificationAlgorithm.Https,
        };

    }
}
