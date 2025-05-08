using BackEndChellengeAPI.Extensions;
using BackEndChellengeAPI.Middlewere;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureAppSettings(builder.Configuration);
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureMongoDb();
builder.Services.ConfigureJwtAuthentication(builder.Configuration);
builder.Services.ConfigureServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// RabbitMQ Event Wiring
//app.Services.RegisterRabbitMqTransferEvent();

app.Run();

public partial class Program { }