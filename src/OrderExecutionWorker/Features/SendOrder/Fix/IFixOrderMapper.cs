using OrderExecutionWorker.Features.SendOrder.Models;
using QuickFix.FIX44;

namespace OrderExecutionWorker.Features.SendOrder.Fix;

public interface IFixOrderMapper
{
    Message Map(Order order);
}
