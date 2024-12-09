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
    public class InvestmentRepository : IInvestmentRepository
    {
        private readonly CADbContext _dbContext;
        public InvestmentRepository(CADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Investment>> GetInvestmentsByClientId(int clientid)
        {
            return await _dbContext.Investments.Where(x=>x.ClientId==clientid).ToListAsync();
        }

        public async Task<Investment> GetInvestmentById(int id)
        {
            return await _dbContext.Investments.FindAsync(id);
        }

        public async Task<IEnumerable<Investment>> GetInvestments()
        {
            return await _dbContext.Investments.ToListAsync();
        }

        public async Task InsertInvestment(Investment Investment)
        {
            await _dbContext.Investments.AddAsync(Investment);
        }

        public async Task DeleteInvestment(int id)
        {
            Investment Investment = await _dbContext.Investments.FindAsync(id);
            _dbContext.Investments.Remove(Investment);
        }

        public void UpdateInvestment(Investment Investment)
        {
            _dbContext.Entry(Investment).State = EntityState.Modified;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
