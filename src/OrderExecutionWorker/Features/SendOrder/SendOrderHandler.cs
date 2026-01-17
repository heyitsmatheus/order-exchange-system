using OrderExecutionWorker.Application.Abstractions;
using OrderExecutionWorker.Features.SendOrder.Fix;
using OrderExecutionWorker.Features.SendOrder.Models;

namespace OrderExecutionWorker.Features.SendOrder;

public sealed class SendOrderHandler(
    IFixOrderMapper mapper,
    IFixSessionManager sessionManager,
    ILogger<SendOrderHandler> logger) : ISendOrderHandler
{
    public async Task<bool> HandleAsync(SendOrderCommand command, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var order = new Order
        {
            Account = command.Account,
            OrderId = command.OrderId,
            Symbol = command.Symbol,
            Side = command.Side,
            Quantity = command.Quantity,
            Type = command.Type,
            Price = (int)(command.Price * 1000)
        };

        var fixMessage = mapper.Map(order);

        var isSuccess = sessionManager.TrySend(fixMessage);

        if (!isSuccess)
            logger.LogWarning("FIX session unavailable. OrderId={OrderId}", order.OrderId);

        await Task.CompletedTask;

        return isSuccess;
    }
}

