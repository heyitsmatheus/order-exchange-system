using OrderFixGatewayWorker.Infrastructure.Fix.Acceptor.Extensions;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddFixAcceptor(builder.Configuration);

var host = builder.Build();
host.Run();