using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Northwind.Web.Middleware
{
    public class ImageCacheMiddleware
    {
        private readonly string DefaultCacheFileNameTemplate = "image_{categoryId}.{extension}";
        private readonly string DefaultCacheDirectory = "D:/ImageCache";
        private string cacheFileNameTemplate;
        private string cacheDirectory;
        private Regex matchRegex = new Regex(@"^/images/[0-9]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        private Dictionary<string, string> cachedImages { get; set; }

        public ImageCacheMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            cachedImages = new Dictionary<string, string>();
            _next = next;
            _configuration = configuration;

            Initialize();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method.ToUpper().Equals("GET") &&
                matchRegex.Match(context.Request.Path).Success)
            {
                await ProcessImageRequest(context);
            }
            else
            {
                await _next(context);
            }
        }

        private async Task ProcessImageRequest(HttpContext context)
        {
            if(!IsPresentInCache(context))
            {
                var originalBody = context.Response.Body;
                using (var memStream = new MemoryStream())
                {
                    context.Response.Body = memStream;
                    await _next(context);
                    context.Response.Body.Seek(0, SeekOrigin.Begin);

                    if (context.Response.StatusCode >= 200 && context.Response.StatusCode < 300)
                    {
                        SaveImageToCache(context);
                    }
                }
                context.Response.Body = originalBody;
            }
            
            var cachedImagePath = cachedImages.GetValueOrDefault(context.Request.Path);
            await context.Response.SendFileAsync(cachedImagePath);
        }

        private bool IsPresentInCache(HttpContext context)
        {
            if (!cachedImages.ContainsKey(context.Request.Path))
            {
                return false;
            }
            var filePath = cachedImages.GetValueOrDefault(context.Request.Path);
            if (string.IsNullOrEmpty(filePath) && !File.Exists(filePath))
            {
                return false;
            }
            return true;
        }

        private void SaveImageToCache(HttpContext context)
        {
            var filePath = SaveFileToDisk(context);

            // TODO: make sure Path only contains path without query params. 
            cachedImages.Add(context.Request.Path, filePath);
        }

        private string SaveFileToDisk(HttpContext context)
        {
            var requestPathParts = context.Request.Path.Value.Split("/");
            var categoryId = requestPathParts[requestPathParts.Length - 1];
            var contentTypeParts = context.Response.ContentType.Split("/"); // content type would be "image/*" 
            var fileExtension = contentTypeParts[contentTypeParts.Length - 1]; // take part after slash
            var contentLength = Convert.ToInt32(context.Response.ContentLength.Value);
            var fileBytes = new BinaryReader(context.Response.Body).ReadBytes(contentLength);
            var fileName =
                cacheFileNameTemplate
                    .Replace("{categoryId}", categoryId)
                    .Replace("{extension}", fileExtension);
            var filePath = $"{cacheDirectory}/{fileName}";
            if (!Directory.Exists(cacheDirectory))
            {
                Directory.CreateDirectory(cacheDirectory);
            }
            File.WriteAllBytes(filePath, fileBytes);

            return filePath;
        }

        private void Initialize()
        {
            cacheFileNameTemplate = 
                string.IsNullOrEmpty(_configuration["Cache:Image:FileStorage:FileName"]) ? 
                DefaultCacheFileNameTemplate: 
                _configuration["Cache:Image:FileStorage:FileName"];
            cacheDirectory = 
                string.IsNullOrEmpty(_configuration["Cache:Image:FileStorage:Directory"]) ?
                    DefaultCacheDirectory: 
                    _configuration["Cache:Image:FileStorage:Directory"];
        }
    }
}