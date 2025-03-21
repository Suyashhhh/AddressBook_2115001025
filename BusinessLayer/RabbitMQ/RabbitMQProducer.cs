using System.Text;
using RabbitMQ.Client;

namespace BusinessLayer.RabbitMQ
{
    public class RabbitMQProducer
    {
        private readonly ConnectionFactory _factory;

        public RabbitMQProducer()
        {
            _factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
        }

        public void PublishMessage(string queueName, string message)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
        }
    }
}