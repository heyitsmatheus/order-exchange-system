using OrderExecutionWorker.Application.Abstractions;
using OrderExecutionWorker.Features.SendOrder.Fix;

namespace OrderExecutionWorker.Features.SendOrder.Extensions
{
    public static class SendOrderCollectionExtensions
    {
        public static IServiceCollection AddSendOrder(
        this IServiceCollection services)
        {
            services.AddSingleton<IFixOrderMapper, Fix44OrderMapper>();
            services.AddTransient<ISendOrderHandler, SendOrderHandler>();

            return services;
        }
    }
}