using Core.Domain.Interfaces;
using Core.Services.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();

var host = builder.Build();
host.Run();
