using Microsoft.Extensions.Configuration;

namespace Core.Repository.Settings
{
    public class BaseRepository
    {
        internal string _connectionString;

        public BaseRepository()
        {
            var configuration = new ConfigurationManager();
            configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
