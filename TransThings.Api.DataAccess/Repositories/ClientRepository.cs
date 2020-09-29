using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.DataAccess.Repositories
{
    public class ClientRepository
    {
        private readonly TransThingsDbContext context;

        public ClientRepository(TransThingsDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Client>> GetAllClientsAsync()
        {
            var clients = await context.Clients.ToListAsync();
            return clients;
        }

        public async Task<Client> GetClientByPeselNumberAsync(string peselNumber)
        {
            var client = await context.Clients.FirstOrDefaultAsync(x => x.ClientPeselNumber.Equals(peselNumber));
            return client;
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            var client = await context.Clients.FirstOrDefaultAsync(x => x.Id.Equals(id));
            return client;
        }

        public async Task AddClientAsync(Client client)
        {
            await context.Clients.AddAsync(client);
            await context.SaveChangesAsync();
        }
        public async Task UpdateClient(Client client)
        {
            context.Clients.Update(client);
            await context.SaveChangesAsync();
        }

        public async Task RemoveClient(Client client)
        {
            context.Clients.Remove(client);
            await context.SaveChangesAsync();
        }
    }
}
