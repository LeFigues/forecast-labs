using Newtonsoft.Json;

namespace ufl_id.Exceptions
{
    public class CustomRateLimitMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomRateLimitMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Response.StatusCode == 429) // Too Many Requests
            {
                context.Response.ContentType = "application/json";
                var response = new
                {
                    Message = "Too many requests. Please try again later."
                };
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
            else
            {
                await _next(context);
            }
        }
    }

}
