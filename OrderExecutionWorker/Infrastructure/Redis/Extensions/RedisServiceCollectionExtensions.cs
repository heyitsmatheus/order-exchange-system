using OrderExecutionWorker.Application.Abstractions;
using OrderExecutionWorker.Features.SendOrder;
using StackExchange.Redis;

namespace OrderExecutionWorker.Infrastructure.Redis.Extensions;

public static class RedisServiceCollectionExtensions
{
    public static IServiceCollection AddRedis(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>(_ =>
            ConnectionMultiplexer.Connect(
                configuration["Redis:ConnectionString"]!));

        services.AddSingleton<ICommandQueue<SendOrderCommand>>(sp =>
        {
            var mux = sp.GetRequiredService<IConnectionMultiplexer>();
            var queueKey = configuration["Redis:SendOrderQueueKey"]!;
            return new RedisCommandQueue<SendOrderCommand>(mux, queueKey);
        });

        return services;
    }
}
