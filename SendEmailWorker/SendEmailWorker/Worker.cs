using Core.Domain.Interfaces;
using Core.Infrastructure.Util;
using Core.Services.BusinesseRules;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private IConnection _connection;
    private IModel _channel;

    public Worker(ILogger<Worker> logger, IRabbitMqService rabbitMqService)
    {
        _logger = logger;
        _channel = rabbitMqService.GetChannel();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            new SendEmailBR().SendMailExecute(message);

        };

        _channel.BasicConsume(queue: AppSettings.RabbitMQQueue,
                              autoAck: true,
                              consumer: consumer);

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
        base.Dispose();
    }
}
