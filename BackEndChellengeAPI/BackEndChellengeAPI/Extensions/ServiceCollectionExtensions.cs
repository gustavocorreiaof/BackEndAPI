using Core.Domain.Interfaces;
using Core.Infrastructure.Repository.Base;
using Core.Infrastructure.Repository.Interfaces;
using Core.Infrastructure.Repository;
using Core.Infrastructure.Util;
using Core.Services.BusinesseRules;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Text;

namespace BackEndChellengeAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureAppSettings(this IServiceCollection services, IConfiguration config)
        {
            AppSettings.Init(config);
            return services;
        }

        public static IServiceCollection ConfigureMongoDb(this IServiceCollection services)
        {
            services.AddSingleton<IMongoDatabase>(sp =>
            {
                var client = new MongoClient(AppSettings.MongoUrl);
                return client.GetDatabase(AppSettings.MongoDatabase);
            });

            return services;
        }

        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration config)
        {
            bool isTesting = config.GetValue<bool>("IsTesting");

            services.AddDbContext<AppDbContext>(options =>
            {
                if (isTesting)
                    options.UseInMemoryDatabase("BackEndApi");
                else
                    options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }

        public static IServiceCollection ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var jwtSection = config.GetSection("JwtSettings");
            var secretKey = jwtSection["SecretKey"];
            var issuer = jwtSection["Issuer"];
            var audience = jwtSection["Audience"];

            var keyBytes = Encoding.UTF8.GetBytes(secretKey!);

            services.AddAuthentication(options =>
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

            services.AddAuthorization();
            return services;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ITransactionBR, TransactionBR>();
            services.AddScoped<IUserBR, UserBR>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            return services;
        }
    }
}
