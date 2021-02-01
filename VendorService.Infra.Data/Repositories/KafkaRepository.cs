using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using VendorService.Domain.Repositories;

namespace VendorService.Infra.Data.Repositories
{
    public class KafkaRepository : IKafkaRepository
    {

        public string SendMessageByKafka(string message)
        {
            var config = new ProducerConfig { BootstrapServers = "kafka:9092" };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var sendResult = producer
                                        .ProduceAsync("fila_pedido", new Message<Null, string> { Value = message })
                                            .GetAwaiter()
                                                .GetResult();

                    Console.WriteLine($"Mensagem '{sendResult.Value}' de '{sendResult.TopicPartitionOffset}'");

                    return $"Mensagem '{sendResult.Value}' de '{sendResult.TopicPartitionOffset}'";
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
                return string.Empty;
            }
        }

    }
}


//private readonly IQueueService _queueService;
//public RabbitMqRepository(IQueueService queueService)
//{
//    _queueService = queueService;
//}
//public async Task EnviarMensagemFilaEmail(string mensagem)
//{
//    await _queueService.SendJsonAsync(
//        mensagem,
//        exchangeName: ConfigurationHelper.RabbitMqExchange,
//        routingKey: ConfigurationHelper.RabbitMqRoutingKey);
//}
