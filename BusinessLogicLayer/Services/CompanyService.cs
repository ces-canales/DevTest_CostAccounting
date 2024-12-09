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
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _CompanyRepository;

        public CompanyService(ICompanyRepository repository)
        {
            _CompanyRepository = repository;
        }

        public async Task<CompanyDto> GetCompanyById(int id)
        {
            try
            {
                return (await _CompanyRepository.GetCompanyById(id)).toDto();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<CompanyDto>> GetCompanies()
        {
            try
            {
                return (await _CompanyRepository.GetCompanies()).Select(Company => Company.toDto());
            }
            catch
            {
                throw;
            }
        }

        public async Task InsertCompany(CreateCompanyDto createCompany)
        {
            Company Company = new Company() { Name = createCompany.Name, SharePrice = createCompany.SharePrice };
            try
            {
                await _CompanyRepository.InsertCompany(Company);
                await _CompanyRepository.Save();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteCompany(int id)
        {
            try
            {
                await _CompanyRepository.DeleteCompany(id);
                await _CompanyRepository.Save();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateCompany(int id, UpdateCompanyDto updateCompany)
        {
            Company Company = new Company() { Id = id, Name = updateCompany.Name, SharePrice = updateCompany.SharePrice };
            try
            {
                _CompanyRepository.UpdateCompany(Company);
            }
            catch
            {
                throw;
            }
        }

    }
}
