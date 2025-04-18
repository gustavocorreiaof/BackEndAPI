using RabbitMQ.Client;

namespace Core.Domain.Interfaces
{
    public interface IRabbitMqService
    {
        IModel GetChannel();
    }
}
