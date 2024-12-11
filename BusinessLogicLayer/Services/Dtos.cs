using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Dtos
{
    public record ClientDto
        ( int Id, string Name, string Email);

    public record CreateClientDto (
        [Required] [StringLength(100)] string Name,
        [Required] string Email
    );

    public record UpdateClientDto(
        [Required][StringLength(100)] string Name,
        [Required] string Email
    );

    public record CompanyDto
        ( int Id, string Name, decimal SharePrice);

    public record CreateCompanyDto(
        [Required][StringLength(100)] string Name,
        [Required] decimal SharePrice
    );

    public record UpdateCompanyDto(
        [Required][StringLength(100)] string Name,
        [Required] decimal SharePrice
    );

    public record InvestmentDto
        ( int Id, 
        int ClientId,
        string ClientName,
        int CompanyId,
        string CompanyName,
        DateTime Date, 
        int Shares, 
        decimal Cost);

    public record CreateInvestmentDto(
        [Required] int ClientId,
        [Required] int CompanyId,
        [Required] DateTime Date,
        [Required] int Shares,
        [Required] decimal Cost
    );

    public record SellInvestmentDto(
        [Required] int ClientId,
        [Required] int CompanyId,
        [Required] DateTime Date,
        [Required] int Shares,
        [Required] decimal Rate,
        [Required] int MethodId
    );

    public record TransactionDto( 
        int Id, 
        int ClientId,
        string ClientName,
        int CompanyId,
        string CompanyName,
        DateTime Date, 
        int TypeId, 
        int Shares, 
        decimal Rate, 
        decimal SaleProfit,
        decimal SaleCostBasis
    );

    public record CreateTransactionDto(
        [Required] int ClientId,
        [Required] int CompanyId,
        [Required] DateTime Date,
        [Required] int TypeId,
        [Required] int Shares,
        [Required] decimal Rate,
        decimal SaleProfit,
        decimal SaleCostBasis
    );

    public record TrxResult(
        int SharesRemainingTotal,
        int SharesRemainingLastInvestment,
        decimal SharesRemainingCostBasis,
        decimal SaleProfit,
        decimal SaleCostBasis
        );
}
