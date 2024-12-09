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
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CADbContext _dbContext;
        public CompanyRepository(CADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            return await _dbContext.Companies.ToListAsync();
        }

        public async Task<Company> GetCompanyById(int id)
        {
            return await _dbContext.Companies.FindAsync(id);
        }

        public async Task InsertCompany(Company Company)
        {
            await _dbContext.Companies.AddAsync(Company);
        }

        public async Task DeleteCompany(int id)
        {
            Company Company = await _dbContext.Companies.FindAsync(id);
            _dbContext.Companies.Remove(Company);
        }

        public void UpdateCompany(Company Company)
        {
            _dbContext.Entry(Company).State = EntityState.Modified;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
