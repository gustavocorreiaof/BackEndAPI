using Microsoft.Extensions.Configuration;

namespace Core.Common.Repository.Base
{
    public class BaseRepository
    {
        public string _connectionString;

        public BaseRepository()
        {
            var configuration = new ConfigurationManager();
            configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
