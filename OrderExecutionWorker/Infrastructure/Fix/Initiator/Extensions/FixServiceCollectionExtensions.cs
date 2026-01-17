using OrderExecutionWorker.Application.Abstractions;
using QuickFix;
using QuickFix.Logger;
using QuickFix.Store;

namespace OrderExecutionWorker.Infrastructure.Fix.Initiator.Extensions;

public static class FixServiceCollectionExtensions
{
    public static IServiceCollection AddFixInitiator(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddSingleton<IMessageStoreFactory, FileStoreFactory>();
        services.AddSingleton<ILogFactory, FileLogFactory>();

        services.AddSingleton<IFixSessionManager, FixSessionManager>();
        services.AddSingleton<FixApplication>();

        services.AddSingleton(_ =>
        {
            string configPath = configuration["Fix:ConfigPath"]!;
            return new SessionSettings(configPath);
        });

        services.AddHostedService<FixInitiator>();

        return services;
    }
}
