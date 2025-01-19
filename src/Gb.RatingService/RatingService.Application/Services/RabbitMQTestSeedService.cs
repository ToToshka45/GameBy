using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using RatingService.Common.CommonServices;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace RatingService.Application.Services;

public class RabbitMQTestSeedService
{
    private IConfiguration _configuration;
    private readonly bool _isTestingRequired;
    private readonly RabbitMQSettings _settings;
    private readonly RabbitMQConfigurations _configs;
    private readonly ConnectionFactory _factory;
    private readonly ILogger<RabbitMQTestSeedService> _logger;

    public RabbitMQTestSeedService(IConfiguration configuration, IOptions<RabbitMQSettings> rabbitmqSettings, IOptions<RabbitMQConfigurations> configs,
        ILogger<RabbitMQTestSeedService> logger)
    {
        _configuration = configuration;
        _isTestingRequired = _configuration.GetValue<bool>("TestSettings:IsRabbitMQTestRequired");
        _settings = rabbitmqSettings.Value; 
        _configs = configs.Value;
        _factory = ConnectionFactoryProvider.GetConnectionFactory(_settings);
        _logger = logger;
    }

    public async Task ExecuteAsync([Range(1, 100)] int usersCount = 10, CancellationToken stoppingToken = default)
    {
        if (!_isTestingRequired) return;

        try
        {
            var users = FakeDataProvider.ProvideUserCreatedEventTestData(usersCount);
            using var conn = await _factory.CreateConnectionAsync(stoppingToken);
            using var channel = await conn.CreateChannelAsync(cancellationToken: stoppingToken);

            string keyPrefix = "user";
            var userCreatedConfig = _configs.FirstOrDefault(e => e.ExchangeName.StartsWith(keyPrefix));
            if (userCreatedConfig is null) {
                _logger.LogInformation($"A Rabbit Config with an exchange name starting with '${keyPrefix}' is not found.");
                return;
            }
            string exchangeName = userCreatedConfig.ExchangeName;
            var routingKey = userCreatedConfig.Queues.FirstOrDefault(e => e.Name.Contains("created"))?.RoutingKey;
            if (String.IsNullOrEmpty(routingKey))
            {
                _logger.LogInformation("A routing key for the queue is not defined.");
                return;
            }

            foreach (var user in users)
            {
                try
                {
                    await Task.Delay(500);

                    await channel.BasicPublishAsync(exchangeName, routingKey,
                        Encoding.UTF8.GetBytes(JsonSerializer.Serialize(user)), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"An error occured while preseeding test data to RabbitMQ: userId = {user.ExternalUserId}, username = ${user.UserName}");
                }
            }

            await channel.CloseAsync(stoppingToken);
            await conn.CloseAsync(stoppingToken);
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
