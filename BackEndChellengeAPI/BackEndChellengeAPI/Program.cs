using Core.Middlewere;
using Core.Repository.Settings;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IMongoDatabase>(serviceProvider =>
{
    var client = new MongoClient("mongodb://localhost:27017/");  // Substitua pela sua connection string
    return client.GetDatabase("BackEndAPI");  // Substitua pelo nome do seu banco de dados
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.Configure<BaseRepository>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddScoped<UserRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
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
