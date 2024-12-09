using BusinessLogicLayer.Services.Contracts;
using BusinessLogicLayer.Services.Dtos;
using DataAccessLayer.DataContext;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _TransactionRepository;

        public TransactionService(ITransactionRepository repository)
        {
            _TransactionRepository = repository;
        }

        public async Task<TransactionDto> GetTransactionById(int id)
        {
            try
            {
                return (await _TransactionRepository.GetTransactionById(id)).toDto();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactionsByClientId(int clientid)
        {
            try
            {
                return (await _TransactionRepository.GetTransactionsByClientId(clientid)).Select(Trx => Trx.toDto());
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactions()
        {
            try
            {
                return (await _TransactionRepository.GetTransactions()).Select(trx => trx.toDto());
            }
            catch
            {
                throw;
            }
        }

        public async Task InsertTransaction(CreateTransactionDto cTransaction)
        {
            Transaction transaction = new Transaction()
            {
                ClientId = cTransaction.ClientId,
                CompanyId = cTransaction.CompanyId,
                Date = cTransaction.Date,
                TypeId = cTransaction.TypeId,
                Shares = cTransaction.Shares,
                Rate = cTransaction.Rate,
                SaleProfit = cTransaction.SaleProfit,
                SaleCostBasis = cTransaction.SaleCostBasis
            };
            
            try
            {
                await _TransactionRepository.InsertTransaction(transaction);
                await _TransactionRepository.Save();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteTransaction(int id)
        {
            try
            {
                await _TransactionRepository.DeleteTransaction(id);
                await _TransactionRepository.Save();
            }
            catch
            {
                throw;
            }
        }

    }
}
