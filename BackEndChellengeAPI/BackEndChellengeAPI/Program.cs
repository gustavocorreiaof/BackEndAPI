using Core.BusinesseRules;
using Core.Interfaces;
using Core.Middlewere;
using Core.Repository;
using Core.Repository.Settings;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMongoDatabase>(serviceProvider =>
{
    var client = new MongoClient("mongodb://localhost:27017/");
    return client.GetDatabase("BackEndAPI");
});

builder.Services.AddControllers();
builder.Services.Configure<BaseRepository>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddScoped<UserRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 
builder.Services.AddScoped<IUserBR, UserBR>();

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