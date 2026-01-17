using OrderExecutionWorker.Application.Abstractions;
using OrderExecutionWorker.Features.SendOrder;

namespace OrderExecutionWorker;
public sealed class Worker(
    IFixSessionManager fixSessionManager,
    ISendOrderHandler handler,
    ICommandQueue<SendOrderCommand> queue,
    ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //TODO: FixSessionManager não deve ser conhecido pelo Worker diretamente
        await fixSessionManager.WaitUntilConnectedAsync(stoppingToken);

        logger.LogInformation("FIX ready. Waiting SendOrderCommand from Redis.");

        while (!stoppingToken.IsCancellationRequested)
        {
            var command = await queue.DequeueAsync(stoppingToken);
            
            if (command is null)
                continue;

            try
            {
                await handler.HandleAsync(command, stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to handle SendOrderCommand");
            }
        }
    }
}
