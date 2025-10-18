using System.Net;
using System.Text.Json;

namespace ApiCustomers.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // continua o pipeline
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado detectado");

                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonSerializer.Serialize(new
            {
                statusCode = context.Response.StatusCode,
                message = "Ocorreu um erro interno no servidor. Por favor, tente novamente mais tarde.",
                detailed = ex.Message // opcional (use apenas em ambiente de dev)
            });

            await context.Response.WriteAsync(result);
        }
    }
}
