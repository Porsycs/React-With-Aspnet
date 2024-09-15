using RabbitMQ.Client;
using System.Text;

public class ClientService
{
    private readonly RabbitMQConfigurationService _rabbitMQConfig;
    private readonly IModel _channel;

    public ClientService(RabbitMQConfigurationService rabbitMQConfig)
    {
        _rabbitMQConfig = rabbitMQConfig;
        _channel = _rabbitMQConfig.CreateChannel();
        _channel.QueueDeclare(queue: "client_queue",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
    }

    public void SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: "",
                              routingKey: "client_queue",
                              basicProperties: null,
                              body: body);
    }
}
