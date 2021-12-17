using CosmosDb.Model;
using CosmosDb.Model.Configuration;
using CosmosDb.Model.Interop;
using CosmosDb.Utils;
using CosmosDb.Utils.Interop;

public static class Startup
{
    public static void ConfigureApplication(WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        var cosmosDbSettings = new CosmosDbSettings();
        var cosmosDbSettingsSection = configuration.GetSection("CosmosDb");
        cosmosDbSettingsSection.Bind(cosmosDbSettings);

        services.AddOptions();
        services.Configure<CosmosDbSettings>(cosmosDbSettingsSection);
    }

    public static void ConfigureDependencies(WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        // CosmosDb

        services.AddSingleton<ICosmosDbClientFactory, CosmosDbClientFactory>();
        services.AddTransient<ICosmosDbContext, CosmosDbContext>();
        services.AddTransient<ICosmosOperations>(provider =>
        {
            var clientFactory = provider.GetRequiredService<ICosmosDbClientFactory>();
            var client = clientFactory.CreateClient();
            var logger = provider.GetRequiredService<ILogger<CosmosRequestChargeOperations>>();

            return new CosmosRequestChargeOperations(client, logger);
        });
    }
}


