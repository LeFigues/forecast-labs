using Newtonsoft.Json;
using System.Net;

namespace ufl_id.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Procesar la solicitud
                await _next(context);
            }
            catch (Exception ex)
            {
                // Capturar cualquier excepción y manejarla
                _logger.LogError(ex, "An unexpected error occurred.");

                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = _env.IsDevelopment()
                ? new { StatusCode = context.Response.StatusCode, Message = exception.Message, StackTrace = exception.StackTrace }
                : new { StatusCode = context.Response.StatusCode, Message = "Internal Server Error", StackTrace = (string?)null };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }

    }

}
