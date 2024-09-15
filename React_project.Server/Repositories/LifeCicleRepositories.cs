using React_project.Server.Interfaces;

namespace React_project.Server.Repositories
{
    public class LifeCicleRepositories
    {
        public static void LifeCicleRepositoriesConfiguration(IServiceCollection services)
        {
            services.AddSingleton<RabbitMQConfigurationService>();
            services.AddSingleton<ClientService>();
            services.AddHostedService<ClientConsumerService>();
            services.AddScoped<IClientRepository, ClientRepository>();
        }

    }
}
