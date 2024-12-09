using BusinessLogicLayer.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogicLayer.Services.Contracts
{
    public interface IClientService
    {
        public Task<ClientDto> GetClientById(int id);

        public Task<IEnumerable<ClientDto>> GetClients();
        public Task InsertClient(CreateClientDto client);
        public Task DeleteClient(int id);
        public void UpdateClient(int id, UpdateClientDto client);

    }
}
