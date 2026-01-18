using OrderApi.Infrastructure.Redis;

namespace OrderApi.Features.SendOrder;

public static class SendOrderEndpoint
{
    public static void MapSendOrder(this IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (
            SendOrderCommand command,
            IRedisCommandPublisher publisher,
            CancellationToken ct) =>
        {
            await publisher.PublishAsync(command, ct);

            return Results.Accepted(null, new
            {
                command.OrderId,
                Status = "Queued"
            });
        })
        .WithName("SendOrder")
        .WithTags("Orders");
    }
}

