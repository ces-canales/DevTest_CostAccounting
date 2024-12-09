using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Contracts
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetClients();
        Task<Client> GetClientById(int id);

        Task InsertClient(Client client);

        Task DeleteClient(int id);
        
        void UpdateClient(Client client);
        Task Save();
    }
}
