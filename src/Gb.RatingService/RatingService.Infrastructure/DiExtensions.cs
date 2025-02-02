using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RatingService.Application;
using RatingService.Application.Services.RabbitConsumers;
using RatingService.Application.Services.RabbitPublishers.TestingPublisher;
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
        builder.AddRepositories();
        builder.AddHostedServices();
    }

    public static async Task MigrateRabbitMQ(this IHostApplicationBuilder builder)
    {
        try
        {
            CancellationTokenSource cts = new();

            using var provider = builder.Services.BuildServiceProvider();
            var settings = provider.GetRequiredService<IOptions<RabbitMQSettings>>().Value;
            var configs = provider.GetRequiredService<IOptions<RabbitMQConfigurations>>().Value;

            var factory = ConnectionFactoryProvider.GetConnectionFactory(settings);
            using var conn = await factory.CreateConnectionAsync(cts.Token);
            using var channel = await conn.CreateChannelAsync(cancellationToken: cts.Token);

            foreach (var config in configs)
            {
                await channel.ExchangeDeclareAsync(config.ExchangeName, config.ExchangeType, false, false, cancellationToken: cts.Token);
                foreach (var queue in config.Queues)
                {
                    await channel.QueueDeclareAsync(queue.Name, false, false, false, cancellationToken: cts.Token);
                    await channel.QueueBindAsync(queue.Name, config.ExchangeName, queue.RoutingKey, cancellationToken: cts.Token);
                }
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    private static void AddRepositories(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        builder.Services.AddScoped<IEventLifecycleRepository, EventLifecycleRepository>();
        builder.Services.AddScoped<IRatingsRepository, RatingsRepository>();
    }

    private static void AddHostedServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHostedService<UserCreatedRabbitEventConsumer>();
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

    public static async Task SeedRabbitTestMessages(this IApplicationBuilder builder, int usersCount)
    {
        using var scope = builder.ApplicationServices.CreateAsyncScope();
        await using var testService = scope.ServiceProvider.GetRequiredService<RabbitMQTestSeedService>();
        await testService.ExecuteAsync(usersCount);
    }
}
