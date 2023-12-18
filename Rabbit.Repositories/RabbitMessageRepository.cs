using Rabbit.Models.Entities;
using Rabbit.Repositories.Interfaces;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rabbit.Repositories
{
    public class RabbitMessageRepository : IRabbitMessageRepository
    {
        public void SendMessage(RabbitMessage message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "CantinaMessagesQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "",
                                     routingKey: "CantinaMessagesQueue",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine(" [X] Sent {0} ", json);
            }
        }
    }
}
