namespace OrderExecutionWorker.Application.Abstractions;

public interface ICommandQueue<TCommand>
{
    Task<TCommand?> DequeueAsync(CancellationToken ct);
}