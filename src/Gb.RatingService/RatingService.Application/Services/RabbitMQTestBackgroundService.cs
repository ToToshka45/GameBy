using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using RatingService.Common.CommonServices;
using System.Text;
using System.Text.Json;

namespace RatingService.Application.Services;

public class RabbitMQTestBackgroundService : BackgroundService
{
    private IConfiguration _configuration;
    private readonly bool _isTestingRequired;
    private readonly RabbitMQSettings _settings;
    private readonly ConnectionFactory _factory;
    private readonly ILogger<RabbitMQTestBackgroundService> _logger;

    public RabbitMQTestBackgroundService(IConfiguration configuration, IOptions<RabbitMQSettings> rabbitmqSettings, ILogger<RabbitMQTestBackgroundService> logger)
    {
        _configuration = configuration;
        _isTestingRequired = _configuration.GetValue<bool>("TestSettings:IsRabbitMQTestRequired");
        _settings = rabbitmqSettings.Value;
        _factory = ConnectionFactoryProvider.GetConnectionFactory(_settings);
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!_isTestingRequired) return;

        try
        {
            var users = FakeDataProvider.ProvideUserCreatedEventTestData();
            using var conn = await _factory.CreateConnectionAsync(stoppingToken);
            using var channel = await conn.CreateChannelAsync(cancellationToken: stoppingToken);

            foreach (var user in users)
            {
                await Task.Delay(600);

                await channel.BasicPublishAsync("user_exchange", "users_created",
                    Encoding.UTF8.GetBytes(JsonSerializer.Serialize(user)), stoppingToken);
            }
        }
        catch (BrokerUnreachableException ex)
        {
            _logger.LogInformation(ex, "Not able to connect to RabbitMQ");
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex, "An error occured while making testing publishes");
        }

    }
}
