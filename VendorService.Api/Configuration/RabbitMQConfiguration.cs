//using Confluent.Kafka;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace VendorService.Api.Configuration
//{
//    public static class RabbitMQConfiguration
//    {
//        public static void AddRabbitMqConfiguration(this IServiceCollection services)
//        {
//            var factory = new ConnectionFactory() { HostName = "localhost" };
//            using (var connection = factory.CreateConnection())
//            using (var channel = connection.CreateModel())
//            {
//                channel.QueueDeclare(queue: "hello",
//                                     durable: false,
//                                     exclusive: false,
//                                     autoDelete: false,
//                                     arguments: null);

//                string message = "Hello World!";
//                var body = Encoding.UTF8.GetBytes(message);

//                channel.BasicPublish(exchange: "",
//                                     routingKey: "hello",
//                                     basicProperties: null,
//                                     body: body);
//                Console.WriteLine(" [x] Sent {0}", message);
//            }

//            Console.WriteLine(" Press [enter] to exit.");
//            Console.ReadLine();
//        }
//    }

//}
