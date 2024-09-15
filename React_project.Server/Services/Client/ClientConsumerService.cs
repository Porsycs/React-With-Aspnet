using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using React_project.Server.Interfaces;
using React_project.Server.Models;
using System.Text;

public class ClientConsumerService : BackgroundService
{
    private readonly RabbitMQConfigurationService _rabbitMQConfig;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ClientConsumerService(RabbitMQConfigurationService rabbitMQConfig, IServiceScopeFactory serviceScopeFactory)
    {
        _rabbitMQConfig = rabbitMQConfig;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var channel = _rabbitMQConfig.CreateChannel();
        channel.QueueDeclare(queue: "client_queue",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            if (stoppingToken.IsCancellationRequested)
            {
                return;
            }

            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var clientMessage = JsonConvert.DeserializeObject<Client>(message);
            if (clientMessage is not null)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var clientRepository = scope.ServiceProvider.GetRequiredService<IClientRepository>();

                    try
                    {
                        switch (clientMessage.operation)
                        {
                            case "create":
                                await CreateClientInDatabase(clientMessage, clientRepository);
                                break;
                            case "update":
                                await UpdateClientInDatabase(clientMessage, clientRepository);
                                break;
                            case "delete":
                                await DeleteClientInDataBase(clientMessage, clientRepository);
                                break;
                            case "inactive":
                                await InactiveClientInDataBase(clientMessage, clientRepository);
                                break;
                            default:
                                throw new ArgumentException("Operation type is invalid");
                        }

                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }
                    catch (Exception e)
                    {
                        SentrySdk.CaptureException(e);
                        channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
                    }
                }
            }
        };

        channel.BasicConsume(queue: "client_queue", autoAck: false, consumer: consumer);

        return Task.CompletedTask;
    }

    private async Task CreateClientInDatabase(Client client, IClientRepository clientRepository)
    {
        await clientRepository.CreateClient(client);
    }

    private async Task UpdateClientInDatabase(Client client, IClientRepository clientRepository)
    {
        await clientRepository.UpdateClient(client);
    }

    private async Task DeleteClientInDataBase(Client client, IClientRepository clientRepository)
    {
        await clientRepository.DeleteClientById(client.Id);
    }

    private async Task InactiveClientInDataBase(Client client, IClientRepository clientRepository)
    {
        await clientRepository.InactiveClient(client.Id);
    }
}
