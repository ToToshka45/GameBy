using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RatingService.Application.Services;
using RatingService.Application.Services.Abstractions;
using RatingService.Application.Services.Caching;
using RatingService.Application.Services.LifecycleServices;

namespace RatingService.Application;

public static class DiExtensions
{
    public static void AddApplicationConfiguration(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IEventLifecycleService, EventLifecycleService>();
        builder.Services.AddScoped<IUserLifecycleService, UserLifecycleService>();
        builder.Services.AddScoped<IRatingsProcessingService, RatingsProcessingService>();
        builder.Services.AddSingleton<InMemoryCachingService>();
    }
}
