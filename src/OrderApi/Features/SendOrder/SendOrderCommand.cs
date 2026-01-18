namespace OrderApi.Features.SendOrder;

public sealed record SendOrderCommand(
    string Account,
    string OrderId,
    string Symbol,
    string Side,
    int Quantity,
    string Type,
    decimal Price = 0
);
