using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Northwind.Web.Middleware
{
    public class ImageCacheMiddleware
    {
        private readonly RequestDelegate _next;

        public ImageCacheMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
        }
    }
}