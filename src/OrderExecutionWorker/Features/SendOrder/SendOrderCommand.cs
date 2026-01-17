using OrderExecutionWorker.Features.SendOrder.Models;

namespace OrderExecutionWorker.Features.SendOrder;

public sealed record SendOrderCommand(
    string Account,
    string OrderId,
    string Symbol,
    OrderSide Side,
    int Quantity,
    OrderType Type,
    decimal Price = 0
);