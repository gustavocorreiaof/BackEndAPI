using Core.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace BackEndChellenge.API.IntegrationTests.Factories
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly Action<IServiceCollection> _configureServices;

        public CustomWebApplicationFactory(Action<IServiceCollection> configureServices)
        {
            _configureServices = configureServices;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(IUserRepository));
                if (descriptor != null)
                    services.Remove(descriptor);

                _configureServices(services);
            });
        }
    }

}
