using RabbitMQ.Client;

public class RabbitMQConfigurationService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQConfigurationService(IConfiguration configuration)
    {
        var factory = new ConnectionFactory
        {
            HostName = configuration["RabbitMQ:HostName"],
            UserName = configuration["RabbitMQ:UserName"],
            Password = configuration["RabbitMQ:Password"]
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public IModel CreateChannel()
    {
        return _channel;
    }

    public IConnection GetConnection()
    {
        return _connection;
    }

    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}
