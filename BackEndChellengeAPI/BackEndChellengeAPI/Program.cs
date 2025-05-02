using BackEndChellengeAPI.Middlewere;
using Core.Domain.Interfaces;
using Core.Infrastructure.Repository;
using Core.Infrastructure.Repository.Base;
using Core.Infrastructure.Repository.Interfaces;
using Core.Infrastructure.Util;
using Core.Services.BusinesseRules;
using Core.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

AppSettings.Init(builder.Configuration);

builder.Services.AddSingleton<IMongoDatabase>(serviceProvider =>
{
    var client = new MongoClient(AppSettings.MongoUrl);
    return client.GetDatabase(AppSettings.MongoDatabase);
});

builder.Services.AddControllers();
builder.Services.Configure<BaseRepository>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserBR, UserBR>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var jwtConfig = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtConfig["SecretKey"];
var issuer = jwtConfig["Issuer"];
var audience = jwtConfig["Audience"];
var keyBytes = Encoding.UTF8.GetBytes(secretKey!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),

        ClockSkew = TimeSpan.FromSeconds(30)
    };
});

builder.Services.AddAuthorization();

var rabbitMqService = new RabbitMqService();
var channel = rabbitMqService.GetChannel();
var publisher = new MessagePublisherBR(channel);

TransferBR.TransferCompleted += publisher.Publish;

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }