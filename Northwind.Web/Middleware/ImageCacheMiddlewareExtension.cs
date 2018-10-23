using Microsoft.AspNetCore.Builder;

namespace Northwind.Web.Middleware
{
    public static class ImageCacheMiddlewareExtension
    {
        public static IApplicationBuilder UseImageCache(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ImageCacheMiddleware>();
        }
    }
}