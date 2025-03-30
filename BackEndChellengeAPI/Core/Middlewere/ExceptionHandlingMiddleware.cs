using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.Text.Json;

namespace Core.Middlewere
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMongoCollection<ExceptionLog> _exceptionLogsCollection;

        // Injeção de dependência do MongoDB
        public ExceptionHandlingMiddleware(RequestDelegate next, IMongoDatabase database)
        {
            _next = next;
            _exceptionLogsCollection = database.GetCollection<ExceptionLog>("exception-logs");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Passa a requisição adiante
            }
            catch (Exception ex)
            {
                // Log de erro no MongoDB
                await LogExceptionToMongoDBAsync(ex);

                int statusCode = 400;

                var errorResponse = new
                {
                    StatusCode = statusCode,
                    Message = ex.Message
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
                // Inserir o log de exceção no MongoDB
                await _exceptionLogsCollection.InsertOneAsync(exceptionLog);
            }
            catch (Exception mongoEx)
            {
                // Se houver falha ao salvar no MongoDB, pode registrar em outro lugar, se necessário
                Console.WriteLine($"Falha ao gravar log no MongoDB: {mongoEx.Message}");
            }
        }
    }

    // Modelo de log de exceção
    public class ExceptionLog
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime Timestamp { get; set; }
        public string ExceptionType { get; set; }
        public string InnerExceptionMessage { get; set; }
        public string InnerExceptionStackTrace { get; set; }
    }
}
