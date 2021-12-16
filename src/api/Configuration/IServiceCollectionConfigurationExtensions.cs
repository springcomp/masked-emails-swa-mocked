public static class IServiceCollectionConfigurationExtensions
{
    public static IConfiguration GetConfiguration(this IServiceCollection services)
    {
        return services.BuildServiceProvider().GetRequiredService<IConfiguration>();
    }
}
