using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RatingService.Application;
using RatingService.Common.Models.Settings;
using RatingService.Domain.Abstraction;
using RatingService.Domain.Abstractions;
using RatingService.Infrastructure.Abstractions;
using RatingService.Infrastructure.DataAccess;
using RatingService.Infrastructure.Repositories;

namespace RatingService.Infrastructure;

public static class DiExtensions
{
    public static void AddConfigurations(this IHostApplicationBuilder builder)
    {
        builder.AddApplicationConfiguration();
    }

    public static void AddRepositories(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        builder.Services.AddScoped<IEventLifecycleRepository, EventLifecycleRepository>();
        builder.Services.AddScoped<IRatingsRepository, RatingsRepository>();
    }

    public static void AddDbConfiguration(this IHostApplicationBuilder builder, IConfiguration config)
    {
        var connStrings = config.GetSection("ConnectionStrings").Get<ConnectionStringsSettings>();
        if (connStrings is null) { throw new Exception("Could not get the ConnectionsString section from appsettings.json."); }

        builder.Services.AddDbContextFactory<RatingServiceDbContext>(options =>
        {
            options.UseNpgsql(connStrings.Npgsql);
            options.LogTo(Console.WriteLine, LogLevel.Trace);
        });
    }

    public static async Task Migrate(this IApplicationBuilder builder)
    {
        await using var scope = builder.ApplicationServices.CreateAsyncScope();
        using var db = scope.ServiceProvider.GetRequiredService<RatingServiceDbContext>();
        await db.Database.MigrateAsync();
    }
}
