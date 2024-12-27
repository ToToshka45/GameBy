using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RatingService.Application.Services;
using RatingService.Application.Services.Abstractions;

namespace RatingService.Application;

public static class DiExtensions
{
    public static void AddApplicationConfiguration(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IEventLifecycleService, EventLifecycleService>();
    }
}
