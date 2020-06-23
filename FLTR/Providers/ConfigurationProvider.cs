using Microsoft.Extensions.Configuration;

namespace FLTR.Providers
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private IConfiguration _configuration;
        
        public ConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public string RootFolderPath => _configuration.GetValue<string>("rootFolderPath");
    }
}