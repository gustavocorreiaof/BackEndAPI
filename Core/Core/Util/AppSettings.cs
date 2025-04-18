using Microsoft.Extensions.Configuration;

namespace Core.Infrastructure.Util
{
    public static class AppSettings
    {
        public static IConfiguration Configuration { get; private set; }

        public static string MongoUrl => Configuration["MongoSettings:ConnectionString"]!;
        public static string MongoDatabase => Configuration["MongoSettings:DatabaseName"]!;
        public static string SendNotificationURL => Configuration["SendNotificationURL"]!;
        public static string AuthorizeTransferURL => Configuration["AuthorizeTransferURL"]!;
        public static string SmtpServer => Configuration["EmailSettings:SmtpServer"]!;
        public static int SmtpPort => int.Parse(Configuration["EmailSettings:SmtpPort"]!);
        public static string SenderEmail => Configuration["EmailSettings:SenderEmail"]!;
        public static string SenderName => Configuration["EmailSettings:SenderName"]!;
        public static string SenderPassword => Configuration["EmailSettings:SenderPassword"]!;
        public static string RabbitMQUserName => Configuration["RabbitMQ:Username"]!;
        public static string RabbitMQPassword => Configuration["RabbitMQ:Password"]!;
        public static string RabbitMQQueue => Configuration["RabbitMQ:Queue"]!;

        public static void Init(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
