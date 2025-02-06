using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Application
{
    public class RabbitService
    {
        private  IConnection _connection;
        private  IChannel _channel;

        public  RabbitService()
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
            catch (Exception ex) {
                return res;
            }
            return true;
        }

        public void SendMessage(string queueName, string message)
        {
            _channel.QueueDeclareAsync(queue: queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublishAsync(exchange: "",
                                 routingKey: queueName,
                                 //basicProperties: null,
                                 body: body);
        }
    }
}
