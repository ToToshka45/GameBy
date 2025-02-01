using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using RatingService.Common.CommonServices;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace RatingService.Application.Services.RabbitPublishers.TestingPublisher;

public class RabbitMQTestSeedService : IAsyncDisposable
{
    private readonly bool _isTestingRequired;
    private readonly RabbitMQSettings _settings;
    private readonly RabbitMQConfigurations _configs;
    private readonly ConnectionFactory _factory;
    private readonly ILogger<RabbitMQTestSeedService> _logger;

    private IConnection? _conn;
    private IChannel? _channel;

    public RabbitMQTestSeedService(IOptions<RabbitMQSettings> rabbitmqSettings, IOptions<RabbitMQConfigurations> configs,
        ILogger<RabbitMQTestSeedService> logger)
    {
        _settings = rabbitmqSettings.Value;
        _configs = configs.Value;
        _factory = ConnectionFactoryProvider.GetConnectionFactory(_settings);
        _logger = logger;
    }

    public async Task ExecuteAsync([Range(1, 100)] int usersCount = 10, CancellationToken stoppingToken = default)
    {
        try
        {
            _conn = await _factory.CreateConnectionAsync(stoppingToken);
            _channel = await _conn.CreateChannelAsync(cancellationToken: stoppingToken);
            await _channel.QueueDeclareAsync(RabbitMQSettings.UserCreatedQueueName, exclusive: false, autoDelete: false, cancellationToken: stoppingToken);

            // prepare the seeding data
            var users = FakeDataProvider.ProvideUserCreatedEventTestData(usersCount);

            string keyPrefix = "user";
            var userCreatedConfig = _configs.FirstOrDefault(e => e.ExchangeName.StartsWith(keyPrefix));
            if (userCreatedConfig is null)
            {
                _logger.LogInformation($"A Rabbit Config with an exchange name starting with '${keyPrefix}' is not found.");
                return;
            }
            string exchangeName = userCreatedConfig.ExchangeName;
            var routingKey = userCreatedConfig.Queues.FirstOrDefault(e => e.Name.Contains("created"))?.RoutingKey;
            if (string.IsNullOrEmpty(routingKey))
            {
                _logger.LogInformation("A routing key for the queue is not defined.");
                return;
            }

            foreach (var user in users)
            {
                try
                {
                    await Task.Delay(500);

                    await _channel.BasicPublishAsync(exchangeName, routingKey,
                        Encoding.UTF8.GetBytes(JsonSerializer.Serialize(user)), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"An error occured while preseeding test data to RabbitMQ: userId = {user.ExternalUserId}, username = ${user.UserName}");
                }
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
        finally
        {
            if (_channel != null)
                await _channel.CloseAsync(stoppingToken);

            if (_conn != null)
                await _conn.CloseAsync(stoppingToken);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_channel != null) await _channel.DisposeAsync();
        if (_conn != null) await _conn.DisposeAsync();
    }
}
