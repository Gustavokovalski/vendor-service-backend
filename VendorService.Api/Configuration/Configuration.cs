using Microsoft.Extensions.Configuration;

namespace VendorService.Api.Configuration
{
    public static class Configuration
    {
        private static IConfiguration _configuration;

        public static void SetConfigurations(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string ConnectionString => _configuration.GetConnectionString("DefaultConnection");
        public static string Secret => _configuration["Secret"];

    }


}
