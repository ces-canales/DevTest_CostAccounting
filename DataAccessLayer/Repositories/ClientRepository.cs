using DataAccessLayer.DataContext;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly CADbContext _dbContext;
        public ClientRepository(CADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Client>> GetClients()
        {
            return await _dbContext.Clients.ToListAsync();
        }

        public async Task<Client> GetClientById(int id)
        {
            return await _dbContext.Clients.FindAsync(id);
        }

        public async Task InsertClient(Client client)
        {
            await _dbContext.Clients.AddAsync(client);
        }

        public async Task DeleteClient(int id)
        {
            Client client = await _dbContext.Clients.FindAsync(id);
            _dbContext.Clients.Remove(client);
        }

        public void UpdateClient(Client client)
        {
            _dbContext.Entry(client).State = EntityState.Modified;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
