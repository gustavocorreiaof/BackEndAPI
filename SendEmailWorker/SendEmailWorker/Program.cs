using Core.Domain.Interfaces;
using Core.Infrastructure.Util;
using Core.Services.Services;

var builder = Host.CreateApplicationBuilder(args);

AppSettings.Init(builder.Configuration);
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();

var host = builder.Build();
host.Run();
