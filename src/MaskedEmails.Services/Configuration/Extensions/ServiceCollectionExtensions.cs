using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MaskedEmails.Services.Configuration.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            return services;
        }
    }
}