using OrderExecutionWorker.Features.SendOrder;

namespace OrderExecutionWorker.Application.Abstractions;

public interface ISendOrderHandler
{
    Task<bool> HandleAsync(SendOrderCommand command, CancellationToken ct);
}
