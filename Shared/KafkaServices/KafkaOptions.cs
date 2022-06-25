namespace Shared.KafkaServices;

public class KafkaOptions
{
    public const string ConfigurationSection = "Kafka";

    public string Bootstrap_Server { get; set; }
    
    public string Schema_Server { get; set; }

    public string Schema_Api_Key { get; set; }

    public string Schema_Api_Secret { get; set; }
  
    public string Client_Id { get; set; }

    public string Topic { get; set; }
}
