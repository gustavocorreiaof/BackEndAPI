using Core.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.Text.Json;

namespace Core.API.Middlewere
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMongoCollection<ExceptionLog> _exceptionLogsCollection;

        public ExceptionHandlingMiddleware(RequestDelegate next, IMongoDatabase database)
        {
            _next = next;
            _exceptionLogsCollection = database.GetCollection<ExceptionLog>("exception-logs");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await LogExceptionToMongoDBAsync(ex);

                int statusCode = 400;

                var errorResponse = new
                {
                    StatusCode = statusCode,
                    ex.Message
                };

                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";

                string result = JsonSerializer.Serialize(errorResponse);

                await context.Response.WriteAsync(result);

                if (!context.Response.HasStarted)
                    throw;
            }
        }

        private async Task LogExceptionToMongoDBAsync(Exception ex)
        {
            var exceptionLog = new ExceptionLog
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                Timestamp = DateTime.UtcNow,
                ExceptionType = ex.GetType().ToString(),
                InnerExceptionMessage = ex.InnerException?.Message,
                InnerExceptionStackTrace = ex.InnerException?.StackTrace
            };

            try
            {
                await _exceptionLogsCollection.InsertOneAsync(exceptionLog);
            }
            catch (Exception mongoEx)
            {
                Console.WriteLine($"Falha ao gravar log no MongoDB: {mongoEx.Message}");
            }
        }
    }
}
