using React_project.Server.Interfaces;

namespace React_project.Server.Repositories
{
    public class LifeCicleRepositories
    {
        public static void LifeCicleRepositoriesConfiguration(IServiceCollection services)
        {
            services.AddScoped<IClientRepository, ClientRepository>();
        }

    }
}
