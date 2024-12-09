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
    public class TransactionRepository : ITransactionRepository
    {
        private readonly CADbContext _dbContext;
        public TransactionRepository(CADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByClientId(int clientid)
        {
            return await _dbContext.Transactions.Where(x=>x.ClientId==clientid).ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            return await _dbContext.Transactions.ToListAsync();
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            return await _dbContext.Transactions.FindAsync(id);
        }

        public async Task InsertTransaction(Transaction Transaction)
        {
            await _dbContext.Transactions.AddAsync(Transaction);
        }

        public async Task DeleteTransaction(int id)
        {
            Transaction Transaction = await _dbContext.Transactions.FindAsync(id);
            _dbContext.Transactions.Remove(Transaction);
        }

        public void UpdateTransaction(Transaction Transaction)
        {
            _dbContext.Entry(Transaction).State = EntityState.Modified;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
