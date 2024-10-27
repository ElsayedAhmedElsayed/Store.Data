using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Store.Service.Services.CacheService;
using System.Text;


namespace Store.Web.Helper
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timetoLiveInSeconds;

        public CacheAttribute(int timetoLiveInSeconds)
        {
            _timetoLiveInSeconds = timetoLiveInSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var _cacheServices = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            var cacheResponse =  await _cacheServices.GetCacheResponseAsync(cacheKey);

            if(!string.IsNullOrEmpty(cacheResponse))
            {
                var ContentResult = new ContentResult
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = ContentResult;
                return;
            }
            
            var executedContext = await next();

            if (executedContext.Result is OkObjectResult response) 
            {
                await _cacheServices.SetCacheResponseAsync(cacheKey, response.Value, TimeSpan.FromSeconds(_timetoLiveInSeconds));
            }
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request) 
        {
            StringBuilder cacheKey = new StringBuilder();

            cacheKey.Append($"{request.Path}");

            foreach (var (key, value) in request.Query.OrderBy(x => x.Key)) 
            {
                cacheKey.Append($"{key} = {value}");
            }
            return cacheKey.ToString();
        }
    }
}
