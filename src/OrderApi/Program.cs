using OrderApi.Features.SendOrder;
using OrderApi.Infrastructure.Redis.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddRedis(builder.Configuration);

var app = builder.Build();

app.MapSendOrder();

app.Run();
