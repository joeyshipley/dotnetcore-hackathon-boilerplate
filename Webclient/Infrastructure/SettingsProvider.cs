using BOS.Webclient.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace BOS.Webclient.Infrastructure
{
    public class SettingsProvider : ISettingsProvider
    {
        private readonly IConfiguration _configuration;

        public SettingsProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string DatabaseConnectionString()
        {
            return _configuration.GetConnectionString("DefaultConnection");
        }
    }
}