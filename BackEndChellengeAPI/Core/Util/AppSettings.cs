using Microsoft.Extensions.Configuration;

namespace Core.Util
{
    public static class AppSettings
    {
        public static IConfiguration Configuration { get; private set; }

        public static string MongoUrl => Configuration["MongoSettings:ConnectionString"];
        public static string MongoDatabase => Configuration["MongoSettings:DatabaseName"];
        public static string SendNotificationURL => Configuration["SendNotificationURL"];
        public static string AuthorizeTransferURL => Configuration["AuthorizeTransferURL"];

        public static void Init(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
