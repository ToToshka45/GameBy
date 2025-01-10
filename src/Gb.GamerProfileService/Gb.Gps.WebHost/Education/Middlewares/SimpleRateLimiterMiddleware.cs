﻿using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace Education.Middlewares
{
    public class SimpleRateLimiterMiddleware
    {
        private readonly RequestDelegate _next;

        public SimpleRateLimiterMiddleware( RequestDelegate next )
        {
            _next = next;
        }

        public async Task InvokeAsync( HttpContext context, IMemoryCache memoryCache )
        {
            var now = DateTime.UtcNow;
            var minInterval = TimeSpan.FromSeconds( 5 );
            var key = $"rateLimiting_{context.Request.Headers[ "IP" ].ToString()}";
            var lastRequestDate = memoryCache.Get<DateTime?>( key );
            if ( lastRequestDate != null && now - lastRequestDate < minInterval )
            {
                context.Response.StatusCode = (int) HttpStatusCode.TooManyRequests;
                context.Response.Headers[ "Retry-After" ] =
                    ( lastRequestDate - now + minInterval ).Value.TotalSeconds.ToString( "#" );
            }
            else
            {
                memoryCache.Set<DateTime>( key, now );
                await _next( context );
            }
        }
    }

    public static class RateLimiterExtensions
    {
        public static IApplicationBuilder UseSimpleRateLimiter( this IApplicationBuilder builder )
        {
            return builder.UseMiddleware<SimpleRateLimiterMiddleware>();
        }
    }
}
