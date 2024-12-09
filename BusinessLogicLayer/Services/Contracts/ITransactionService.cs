using BusinessLogicLayer.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogicLayer.Services.Contracts
{
    public interface ITransactionService
    {
        public Task<TransactionDto> GetTransactionById(int id);

        public Task<IEnumerable<TransactionDto>> GetTransactionsByClientId(int clientid);
        public Task<IEnumerable<TransactionDto>> GetTransactions();
        public Task InsertTransaction(CreateTransactionDto Transaction);

        public Task DeleteTransaction(int id);

    }
}
