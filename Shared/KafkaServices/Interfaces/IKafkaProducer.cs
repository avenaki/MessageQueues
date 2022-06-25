using Shared.Models;

namespace Shared.KafkaServices.Interfaces;

public interface IKafkaProducer
{
    public void ProduceMessage(Message message);
}
