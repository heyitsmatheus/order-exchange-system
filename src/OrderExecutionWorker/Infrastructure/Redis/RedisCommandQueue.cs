using OrderExecutionWorker.Application.Abstractions;
using OrderExecutionWorker.Infrastructure.Serialization;
using StackExchange.Redis;
using System.Text.Json;

namespace OrderExecutionWorker.Infrastructure.Redis;

public sealed class RedisCommandQueue<TCommand>(
    IConnectionMultiplexer connection,
    string queueKey)
    : ICommandQueue<TCommand>
{
    private readonly IDatabase _db = connection.GetDatabase();

    public async Task<TCommand?> DequeueAsync(CancellationToken ct)
    {
        var result = await _db.ExecuteAsync(
            "BLPOP",
            queueKey,
            "1");

        if (result is null || result.Length <= 0)
            return default;

        var payload = (string)result[1]!;
        return JsonSerializer.Deserialize<TCommand>(payload, JsonDefaults.Options)!;
    }
}