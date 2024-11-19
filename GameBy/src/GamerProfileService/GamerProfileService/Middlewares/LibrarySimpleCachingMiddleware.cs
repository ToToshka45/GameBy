using Microsoft.Extensions.Caching.Memory;

namespace GamerProfileService.Middlewares;

public class LibrarySimpleCachingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LibrarySimpleCachingMiddleware> _logger;

    public LibrarySimpleCachingMiddleware(RequestDelegate next, ILogger<LibrarySimpleCachingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, IMemoryCache memoryCache)
    {
        if (context.Request.Path == "/api/v1/GameLibrary/library" )
        {
            context.Request.EnableBuffering();
            var key = $"caching_{context.Request.Path.ToString()}";
            var cache = memoryCache.Get<byte[]>(key);
            if (cache != null)
                await GetDataFromCache(context, memoryCache, key);
            else
                await SaveDataToCacheEndExecuteNext(context, memoryCache, _next);
        }
    }

    private async Task GetDataFromCache(HttpContext context, IMemoryCache memoryCache, string key)
    {
        var cache = memoryCache.Get<byte[]>(key);
        var responseStream = context.Response.Body;
        _logger.LogInformation("taking data from cache");
        context.Response.Body = responseStream;
        context.Response.Headers.Append("Content-Type", "text/plain; charset=utf-8");
        await context.Response.Body.WriteAsync(cache);
    }

    private async Task SaveDataToCacheEndExecuteNext(HttpContext context, IMemoryCache memoryCache, RequestDelegate requestDelegate)
    {
        var responseStream = context.Response.Body;
        var key = $"caching_{context.Request.Path.ToString()}";
        _logger.LogInformation("taking data from action method");
        await using var ms = new MemoryStream();
        context.Response.Body = ms;
        await requestDelegate(context);
        memoryCache.Set(key, ms.ToArray(),
            TimeSpan.FromSeconds(5));
        context.Response.Body = responseStream;
        await context.Response.Body.WriteAsync(ms.ToArray());
    }
}

public static class CachingExtensions
{
    public static IApplicationBuilder UseLibrarySimpleCaching(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LibrarySimpleCachingMiddleware>();
    }
}
