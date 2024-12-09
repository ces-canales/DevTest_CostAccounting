using BusinessLogicLayer.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogicLayer.Services.Contracts
{
    public interface ICompanyService
    {
        public Task<CompanyDto> GetCompanyById(int id);

        public Task<IEnumerable<CompanyDto>> GetCompanies();
        public Task InsertCompany(CreateCompanyDto Company);
        public Task DeleteCompany(int id);
        public void UpdateCompany(int id, UpdateCompanyDto Company);

    }
}
