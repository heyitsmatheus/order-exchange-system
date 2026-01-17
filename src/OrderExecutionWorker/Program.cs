using OrderExecutionWorker;
using OrderExecutionWorker.Features.SendOrder.Extensions;
using OrderExecutionWorker.Infrastructure.Fix.Initiator.Extensions;
using OrderExecutionWorker.Infrastructure.Redis.Extensions;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddHostedService<Worker>();

builder.Services.AddFixInitiator(builder.Configuration);
builder.Services.AddRedis(builder.Configuration);

builder.Services.AddSendOrder();

var host = builder.Build();
host.Run();
