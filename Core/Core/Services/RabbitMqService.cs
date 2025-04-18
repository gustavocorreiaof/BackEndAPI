using Core.Domain.Interfaces;
using Core.Infrastructure.Util;
using RabbitMQ.Client;

namespace Core.Services.Services
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqService()
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = AppSettings.RabbitMQUserName,
                Password = AppSettings.RabbitMQPassword
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: AppSettings.RabbitMQQueue,
                      durable: false,
                      exclusive: false,
                      autoDelete: false,
                      arguments: null);
        }

        public IModel GetChannel()
        {
            return _channel;
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}
