using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class RabbitMqService
    {
        private IConnection _connection;
        private IChannel _channel;

        //rating_service_event_finished
        //await _channel.QueueDeclareAsync(CURRENT_QUEUE, exclusive: false, autoDelete: false, cancellationToken: token);
        public RabbitMqService()
        {

        }

        public async Task<bool> Init(string connString)
        {
            bool res = false;
            try
            {

                var factory = new RabbitMQ.Client.ConnectionFactory()
                {
                    HostName = "goose.rmq2.cloudamqp.com",
                    UserName = "fxboawbf",
                    Password = "w5zQ9VUibh-KJGXmlsTdz2xxJ5voyrlL",
                    Port = 5672,
                    VirtualHost = "fxboawbf"
                };
                _connection = await factory.CreateConnectionAsync();
                _channel = await _connection.CreateChannelAsync();
            }
            catch (Exception ex)
            {
                return res;
            }
            return true;
        }

        public async void SendMessage(string queueName, string message)
        {
            var res=await _channel.QueueDeclareAsync(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(message);
            await _channel.BasicPublishAsync(exchange: "",
                                 routingKey: queueName,
                                 //basicProperties: null,
                                 body: body);
        }
    }

}
