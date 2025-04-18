using Core.Infrastructure.Events;
using Core.Infrastructure.Util;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Core.Services.BusinesseRules
{
    public class MessagePublisherBR
    {
        private readonly IModel _channel;

        public MessagePublisherBR(IModel channel)
        {
            _channel = channel;
        }

        public void Publish(object? sender, TransferEventArgs e)
        {
            _channel.QueueDeclare(queue: AppSettings.RabbitMQQueue,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var json = JsonSerializer.Serialize(e);
            var body = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(exchange: "",
                                  routingKey: AppSettings.RabbitMQQueue,
                                  basicProperties: null,
                                  body: body);
        }
    }
}
