using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Contracts
{
    public interface IInvestmentRepository
    {
        Task<Investment> GetInvestmentById(int id);

        Task<IEnumerable<Investment>> GetInvestmentsByClientId(int clientid);

        Task<IEnumerable<Investment>> GetInvestments();

        Task InsertInvestment(Investment investment);

        Task DeleteInvestment(int id);

        void UpdateInvestment(Investment investment);
        Task Save();
    }
}
