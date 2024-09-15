using Microsoft.EntityFrameworkCore;
using React_project.Server.Context;
using React_project.Server.Interfaces;
using React_project.Server.Models;
using System.Web.Helpers;

namespace React_project.Server.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private AppDbContext _dbContext;
        public ClientRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Client>> GetAllClients()
        {
            try
            {
                return await _dbContext.Clients.AsNoTracking().ToListAsync();
            }
            catch(Exception e)
            {
                SentrySdk.CaptureException(e);
                return new List<Client>();
            }
        }

        public async Task<List<Client>?> GetClientByEmail(string email)
        {
            try
            {
                if(!string.IsNullOrEmpty(email))
                    return await _dbContext.Clients.AsNoTracking().Where(w => w.Email.Contains(email) && w.active).ToListAsync();

                return new List<Client>();
            }
            catch (Exception e)
            {
                SentrySdk.CaptureException(e);
                return new List<Client>();
            }
        }

        public async  Task<Client?> GetClientById(Guid id)
        {
            try
            {
                return await _dbContext.Clients.AsNoTracking().FirstOrDefaultAsync(w => w.Id == id && w.active);
            }
            catch (Exception e)
            {
                SentrySdk.CaptureException(e);
                return new Client();
            }
        }

        public async Task<List<Client>?> GetClientByName(string name)
        {
            try
            {
                if (!string.IsNullOrEmpty(name))
                    return await _dbContext.Clients.AsNoTracking().Where(w => w.Name.Contains(name) && w.active).ToListAsync();

                return new List<Client>();
            }
            catch (Exception e)
            {
                SentrySdk.CaptureException(e);
                return new List<Client>();
            }
        }
        public async Task<List<Client>?> GetClientByDocument(string document)
        {
            try
            {
                if (!string.IsNullOrEmpty(document))
                    return await _dbContext.Clients.AsNoTracking().Where(w => w.document.Contains(document) && w.active).ToListAsync();

                return new List<Client>();
            }
            catch (Exception e)
            {
                SentrySdk.CaptureException(e);
                return new List<Client>();
            }
        }

        public async Task CreateClient(Client client)
        {
            try
            {
                _dbContext.Add(client);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                SentrySdk.CaptureException(e);
            }
        }

        public async Task UpdateClient(Client client)
        {
            try
            {
                client.AlterationDate = DateTime.Now;
                _dbContext.Update(client);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                SentrySdk.CaptureException(e);
            }
        }

        public async Task DeleteClientById(Guid id)
        {
            try
            {
                var client = GetClientById(id).Result;

                if (client is not null)
                {
                    _dbContext.Attach(client);
                    _dbContext.Remove(client);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                SentrySdk.CaptureException(e);
            }
        }

        public async Task InactiveClient(Guid id)
        {
            try
            {
                var client = GetClientById(id).Result;
                if (client is not null)
                {
                    _dbContext.Attach(client);
                    client.active = false;
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                SentrySdk.CaptureException(e);
            }
        }
    }
}
