namespace OrderApi.Infrastructure.Redis;

public interface IRedisCommandPublisher
{
    Task PublishAsync<T>(T command, CancellationToken ct);
}
