using OrderExecutionWorker.Features.SendOrder.Models;
using QuickFix.Fields;
using QuickFix.FIX44;

namespace OrderExecutionWorker.Features.SendOrder.Fix;

public sealed class Fix44OrderMapper : IFixOrderMapper
{
    public Message Map(Order order)
    {
        var message = new NewOrderSingle(

            new ClOrdID(order.OrderId),
            new Symbol(order.Symbol),
            new Side(MapSide(order.Side)),
            new TransactTime(DateTime.UtcNow),
            new OrdType(MapOrderType(order.Type))
        );

        message.SetField(new OrderQty(order.Quantity));
        message.SetField(new Price(order.Price));
        message.SetField(new Account(order.Account));

        return message;
    }

    private static char MapSide(OrderSide side) =>
        side switch
        {
            OrderSide.Buy => Side.BUY,
            OrderSide.Sell => Side.SELL,
            _ => throw new ArgumentOutOfRangeException(nameof(side))
        };

    private static char MapOrderType(OrderType type) =>
        type switch
        {
            OrderType.Market => OrdType.MARKET,
            OrderType.Limit => OrdType.LIMIT,
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };
}
