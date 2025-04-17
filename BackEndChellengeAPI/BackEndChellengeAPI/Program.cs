using Core.BusinesseRules;
using Core.Interfaces;
using Core.Middlewere;
using Core.Repository;
using Core.Repository.Settings;
using Core.Util;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

AppSettings.Init(builder.Configuration);

builder.Services.AddSingleton<IMongoDatabase>(serviceProvider =>
{
    var client = new MongoClient(AppSettings.MongoUrl);
    return client.GetDatabase(AppSettings.MongoDatabase);
});

builder.Services.AddControllers();
builder.Services.Configure<BaseRepository>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddScoped<UserRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 
builder.Services.AddScoped<IUserBR, UserBR>();

NotificationBR notificacaoService = new NotificationBR();

TransferBR.TransferCompleted += notificacaoService.SendEmail;

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }