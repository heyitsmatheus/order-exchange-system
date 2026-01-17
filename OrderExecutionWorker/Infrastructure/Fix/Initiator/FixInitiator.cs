using QuickFix;
using QuickFix.Logger;
using QuickFix.Store;
using QuickFix.Transport;

namespace OrderExecutionWorker.Infrastructure.Fix.Initiator;

public sealed class FixInitiator(
   FixApplication application,
   SessionSettings settings,
   IMessageStoreFactory storeFactory,
   ILogFactory logFactory,
   ILogger<FixInitiator> logger) : IHostedService
{
    private readonly SocketInitiator _initiator = new(
            application,
            storeFactory,
            settings,
            logFactory);

    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting FIX Initiator");
        _initiator.Start();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Stopping FIX Initiator");
        _initiator.Stop();
        return Task.CompletedTask;
    }
}
