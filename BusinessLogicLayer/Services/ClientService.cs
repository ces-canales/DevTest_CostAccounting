using BusinessLogicLayer.Services.Contracts;
using BusinessLogicLayer.Services.Dtos;
using DataAccessLayer.DataContext;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository repository)
        {
            _clientRepository = repository;
        }

        public async Task<ClientDto> GetClientById(int id)
        {
            try
            {
                return (await _clientRepository.GetClientById(id)).toDto();
            }catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<ClientDto>> GetClients()
        {
            try
            {
                return (await _clientRepository.GetClients()).Select(client => client.toDto());
            }
            catch
            {
                throw;
            }
        }

        public async Task InsertClient(CreateClientDto createclient)
        {
            Client client = new Client() { Name= createclient.Name, Email=createclient.Email };
            try
            {
                await _clientRepository.InsertClient(client);
                await _clientRepository.Save();
            }catch
            {
                throw;
            }
        }

        public async Task DeleteClient(int id)
        {
            try
            {
                await _clientRepository.DeleteClient(id);
                await _clientRepository.Save();
            }catch
            {
                throw;
            }
        }

        public void UpdateClient(int id, UpdateClientDto updateclient)
        {
            Client client = new Client() { Id=id, Name=updateclient.Name, Email=updateclient.Email };
            try
            {
                _clientRepository.UpdateClient(client);
                _clientRepository.Save();
            } catch
            {
                throw;
            }
        }

    }
}
