using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BusinessLayer.RabbitMQ
{ 
public class RabbitMQConsumer : BackgroundService
{
    private readonly ConnectionFactory _factory;
    private IConnection _connection;
    private IModel _channel;

    public RabbitMQConsumer()
    {
        _factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: "UserQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
        _channel.QueueDeclare(queue: "ContactQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

        return base.StartAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"🔹 Received Message: {message}");
        };

        _channel.BasicConsume(queue: "UserQueue", autoAck: true, consumer: consumer);
        _channel.BasicConsume(queue: "ContactQueue", autoAck: true, consumer: consumer);

        return Task.Delay(Timeout.Infinite, stoppingToken);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _channel?.Close();
        _connection?.Close();
        return base.StopAsync(cancellationToken);
    }
}
}
