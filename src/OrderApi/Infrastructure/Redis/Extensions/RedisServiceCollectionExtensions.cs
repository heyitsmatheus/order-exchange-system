using StackExchange.Redis;

namespace OrderApi.Infrastructure.Redis.Extensions;

public static class RedisServiceCollectionExtensions
{
    public static IServiceCollection AddRedis(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>(_ =>
            ConnectionMultiplexer.Connect(
                configuration["Redis:ConnectionString"]!));

        services.AddScoped<IRedisCommandPublisher, RedisCommandPublisher>();

        return services;
    }
}
