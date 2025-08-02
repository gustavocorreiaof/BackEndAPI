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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000", policy =>
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();


app.UseCors("AllowLocalhost3000");

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