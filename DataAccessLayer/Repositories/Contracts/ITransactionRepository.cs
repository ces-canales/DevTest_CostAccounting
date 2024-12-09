using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Contracts
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactionsByClientId(int id);

        Task<Transaction> GetTransactionById(int id);

        Task<IEnumerable<Transaction>> GetTransactions();

        Task InsertTransaction(Transaction Transaction);

        Task DeleteTransaction(int id);

        void UpdateTransaction(Transaction Transaction);
        Task Save();
    }
}
