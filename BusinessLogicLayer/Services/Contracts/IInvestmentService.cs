using BusinessLogicLayer.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogicLayer.Services.Contracts
{
    public interface IInvestmentService
    {
        public Task<InvestmentDto> GetInvestmentById(int id);

        public Task<IEnumerable<InvestmentDto>> GetInvestmentsByClientId(int clientid);
        public Task<IEnumerable<InvestmentDto>> GetInvestments();
        public Task InsertInvestment(CreateInvestmentDto Investment);
        public Task<TrxResult> SellInvestment(SellInvestmentDto Investment);

        public Task DeleteInvestment(int id);

    }
}
