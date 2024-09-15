
using React_project.Server.Models;

namespace React_project.Server.Interfaces
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllClients();
        Task<Client?> GetClientById(Guid id);
        Task<List<Client>?> GetClientByName(string name);
        Task<List<Client>?> GetClientByEmail(string email);
        Task<List<Client>?> GetClientByDocument(string email);
        Task InactiveClient(Guid id);
        Task CreateClient(Client client);
        Task UpdateClient(Client client);
        Task DeleteClientById(Guid id);
    }
}
