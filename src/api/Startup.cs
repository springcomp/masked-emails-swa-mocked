using CosmosDb.Model;
using CosmosDb.Model.Configuration;
using CosmosDb.Model.Interop;
using CosmosDb.Model.Shim;
using CosmosDb.Utils;
using CosmosDb.Utils.Interop;
using MaskedEmails.Inbox.Http;
using MaskedEmails.Services;
using MaskedEmails.Services.Configuration.Extensions;
using MaskedEmails.Services.Interop;
using MaskedEmails.Services.Storage;
using Utils;
using Utils.Interop;

[assembly: FunctionsStartup(typeof(Startup))]

public class Startup : FunctionsStartup
{
	public override void Configure(IFunctionsHostBuilder builder)
	{
		ConfigureServices(builder.Services);
	}

	private void ConfigureServices(IServiceCollection services)
    {
        // retrieve IConfiguration instance if necessary
        // to configure the Function App

        var configuration = services.GetConfiguration();

        ConfigureServices(services, configuration);
        ConfigureDependencies(services, configuration);
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSettings(configuration);
        services.AddInboxApi(configuration);
        services.AddCosmosDb(configuration);
    }

    public static void ConfigureDependencies(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ICosmosDbClientFactory, CosmosDbClientFactory>();
        services.AddTransient<ICosmosDbContext, CosmosDbContextShim>();
        services.AddTransient<ICosmosOperations>(provider =>
        {
            var clientFactory = provider.GetRequiredService<ICosmosDbClientFactory>();
            var client = clientFactory.CreateClient();
            var logger = provider.GetRequiredService<ILogger<CosmosRequestChargeOperations>>();

            return new CosmosRequestChargeOperations(client, logger);
        });

        services.AddTransient<IProfilesService, CosmosDbProfilesService>();

        services.AddTransient<IUnixTime, UnixTime>();
        services.AddTransient<IUniqueIdGenerator, UniqueIdGenerator>();

        services.AddTransient<ISequence, StorageTableSequence>(
            provider => new StorageTableSequence(configuration));

        services.AddSingleton<IMaskedEmailCommandService, MaskedEmailCommandQueueService>(
            provider => new MaskedEmailCommandQueueService(configuration));
    }
}