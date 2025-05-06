using Core.Services.BusinesseRules;
using Core.Services.Services;

namespace BackEndChellengeAPI.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void RegisterRabbitMqTransferEvent(this IServiceProvider services)
        {
            var rabbitService = new RabbitMqService();
            var channel = rabbitService.GetChannel();
            var publisher = new MessagePublisherBR(channel);

            TransferBR.TransferCompleted += publisher.Publish;
        }
    }
}
