using Store.Service.Services.HandelResponse;
using System.Net;
using System.Text.Json;

namespace Store.Web.Middleware
{
    public class ExeptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExeptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExeptionMiddleware(RequestDelegate Next, ILogger<ExeptionMiddleware> logger, IHostEnvironment env) 
        {
            next = Next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex) 
            {
                logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var ResponseEnv = env.IsDevelopment() ? new CustomExeption(500, ex.Message, ex.StackTrace.ToString())
                    : new CustomExeption((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var jsonResponse = JsonSerializer.Serialize(ResponseEnv, options);

                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
