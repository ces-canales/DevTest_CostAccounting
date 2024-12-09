using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetCompanies();
        Task<Company> GetCompanyById(int id);

        Task InsertCompany(Company Company);

        Task DeleteCompany(int id);

        void UpdateCompany(Company Company);
        Task Save();
    }
}
