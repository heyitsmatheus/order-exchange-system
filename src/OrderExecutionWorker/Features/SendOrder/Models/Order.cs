namespace OrderExecutionWorker.Features.SendOrder.Models;

public sealed class Order
{
    public string Account { get; init; } = default!;
    public string OrderId { get; init; } = default!;
    public string Symbol { get; init; } = default!;
    public OrderSide Side { get; init; }
    public int Quantity { get; init; }
    public OrderType Type { get; init; }
    public int Price { get; init; }
}

public enum OrderSide
{
    Buy,
    Sell
}

public enum OrderType
{
    Market,
    Limit
}