using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosDb.Model.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCosmosDb(this IServiceCollection services, IConfiguration configuration)
        {
            var cosmosDbSettings = new CosmosDbSettings();
            var cosmosDbSettingsSection = configuration.GetSection("CosmosDb");
            cosmosDbSettingsSection.Bind(cosmosDbSettings);

            services.Configure<CosmosDbSettings>(cosmosDbSettingsSection);

            return services;
        }
    }
}
