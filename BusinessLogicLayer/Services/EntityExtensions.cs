using BusinessLogicLayer.Services.Dtos;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public static class EntityExtensions
    {
        public static ClientDto toDto(this Client client)
        {
            return new ClientDto(
                client.Id,
                client.Name,
                client.Email
            );
        }

        public static CompanyDto toDto(this Company company)
        {
            return new CompanyDto(
                company.Id,
                company.Name,
                company.SharePrice
            );
        }

        public static InvestmentDto toDto(this Investment investment)
        {
            return new InvestmentDto(
                investment.Id,
                investment.ClientId,
                investment.Client.Name,
                investment.CompanyId,
                investment.Company.Name,
                investment.Date,
                investment.Shares,
                investment.Cost
            );
        }

        public static TransactionDto toDto(this Transaction transaction)
        {
            return new TransactionDto(
                transaction.Id,
                transaction.ClientId,
                transaction.Client.Name,
                transaction.CompanyId,
                transaction.Company.Name,
                transaction.Date,
                transaction.TypeId,
                transaction.Shares,
                transaction.Rate,
                transaction.SaleProfit,
                transaction.SaleCostBasis
            );
        }

    }
}
