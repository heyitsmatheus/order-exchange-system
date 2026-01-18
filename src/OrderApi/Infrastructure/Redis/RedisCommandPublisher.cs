using OrderApi.Infrastructure.Serialization;
using StackExchange.Redis;
using System.Text.Json;

namespace OrderApi.Infrastructure.Redis;

public sealed class RedisCommandPublisher(
    IConnectionMultiplexer redis,
    IConfiguration config) : IRedisCommandPublisher
{
    private readonly IDatabase _db = redis.GetDatabase();
    private readonly string _key = config["Redis:SendOrderQueueKey"]!;

    public async Task PublishAsync<T>(T command, CancellationToken ct)
    {
        var json = JsonSerializer.Serialize(command, JsonDefaults.Options);

        await _db.ListRightPushAsync(_key, json);
    }
}