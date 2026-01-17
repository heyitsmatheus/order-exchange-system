using QuickFix;
using QuickFix.Logger;
using QuickFix.Store;

namespace OrderFixGatewayWorker.Infrastructure.Fix.Acceptor;

public sealed class FixAcceptor(
   FixApplication application,
   SessionSettings settings,
   IMessageStoreFactory storeFactory,
   ILogFactory logFactory,
   ILogger<FixAcceptor> logger) : IHostedService
{
    private readonly ThreadedSocketAcceptor _acceptor = new(
            application,
            storeFactory,
            settings,
            logFactory);

    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting FIX Initiator");
        _acceptor.Start();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Stopping FIX Initiator");
        _acceptor.Stop();
        return Task.CompletedTask;
    }
}
