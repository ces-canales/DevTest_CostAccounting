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
    public class InvestmentService : IInvestmentService
    {
        private readonly IInvestmentRepository _InvestmentRepository;
        private readonly ITransactionService _TransactionService;

        public InvestmentService(IInvestmentRepository irepository, ITransactionService tservice)
        {
            _InvestmentRepository = irepository;
            _TransactionService = tservice;
        }

        public async Task<InvestmentDto> GetInvestmentById(int id)
        {
            try
            {
                return (await _InvestmentRepository.GetInvestmentById(id)).toDto();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<InvestmentDto>> GetInvestmentsByClientId(int clientid)
        {
            try
            {
                return (await _InvestmentRepository.GetInvestmentsByClientId(clientid)).Select(Investment => Investment.toDto());
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<InvestmentDto>> GetInvestments()
        {
            try
            {
                return (await _InvestmentRepository.GetInvestments()).Select(Investment=>Investment.toDto());
            }
            catch
            {
                throw;
            }
        }

        public async Task InsertInvestment(CreateInvestmentDto cInvestment)
        {
            Investment Investment = new Investment() { 
                ClientId = cInvestment.ClientId, 
                CompanyId=cInvestment.CompanyId,
                Date=cInvestment.Date,
                Shares=cInvestment.Shares,
                Cost=cInvestment.Cost
            };
            //Create "Buy" Transaction
            CreateTransactionDto Trx = new CreateTransactionDto( 
                cInvestment.ClientId,
                cInvestment.CompanyId,
                DateTime.Now,
                1,
                cInvestment.Shares,
                cInvestment.Cost,
                default,
                default
            );

            try
            {
                await _TransactionService.InsertTransaction(Trx);
                await _InvestmentRepository.InsertInvestment(Investment);
                await _InvestmentRepository.Save();
            }
            catch
            {
                throw;
            }
        }

        public async Task<TrxResult> SellInvestment(SellInvestmentDto sellInvestment)
        {
            var result = new TrxResult(0,0,0,0,0);
            IEnumerable<Investment> investments = await _InvestmentRepository.GetInvestmentsByClientId(sellInvestment.ClientId);
            if (investments.Where(x=>x.CompanyId==sellInvestment.CompanyId).Any())
            {
                //CREATING SPECIFIC ROUTINES FOR DIFF ACCOUNTING METHODS.
                switch (sellInvestment.MethodId)
                {
                    case 1:
                        result=SellUsingFIFO(sellInvestment,investments);
                        break;
                    case 2:
                        result=SellUsingLIFO(sellInvestment,investments);
                        break;
                }
            }
            return result;
        }

        private TrxResult SellUsingFIFO(SellInvestmentDto sellInvestment, IEnumerable<Investment> investments)
        {
            //calculations
            decimal cprofit = 0;
            decimal costbasis = 0;

            //share counters
            int totalshares = investments.Sum(x => x.Shares);
            int totalsoldshares = 0;
            int invshares = 0;
            int invsoldshares = 0;

            //Investments Sold
            List<Investment> soldInvIds = new List<Investment>();

            //apply FIFO rules
            foreach (var investment in investments.OrderBy(i => i.Date))
            {
                
                if ((sellInvestment.Shares - totalsoldshares) > investment.Shares)
                {
                    //calculate shares
                    invsoldshares = investment.Shares;
                    totalsoldshares = totalsoldshares + invsoldshares;
                    //calculate profit
                    cprofit = cprofit + (invsoldshares * (sellInvestment.Rate - investment.Cost));
                    //calculate costbasis
                    costbasis = costbasis + invsoldshares * investment.Cost;
                    //flag investment as sold
                    soldInvIds.Add(investment);
                }
                else
                {
                    //calculate shares
                    invsoldshares = sellInvestment.Shares - totalsoldshares;
                    totalsoldshares = totalsoldshares + invsoldshares;
                    //calculate profit
                    cprofit = cprofit + (invsoldshares * (sellInvestment.Rate - investment.Cost));
                    //calculate costbasis
                    costbasis = costbasis + invsoldshares * investment.Cost;
                    //Sell only amount of shares necessary
                    investment.Shares = investment.Shares - invsoldshares;
                    invshares = investment.Shares;
                    break;
                }
            }

            var RemainingInvestments = investments.ToList().Where(p => soldInvIds.All(p2 => p2.Id != p.Id));

            //calculate costbasis
            costbasis = costbasis / totalsoldshares;

            var result = new TrxResult(
                totalshares - totalsoldshares,
                invshares,
                (RemainingInvestments.Sum(x => x.Shares * x.Cost) / RemainingInvestments.Sum(y => y.Shares)), //same formula as used in the investments list view.
                cprofit,
                costbasis
                );

            return result;
        }

        private TrxResult SellUsingLIFO(SellInvestmentDto sellInvestment, IEnumerable<Investment> investments)
        {
            //calculations
            decimal cprofit = 0;
            decimal costbasis = 0;

            //share counters
            int totalshares = investments.Sum(x => x.Shares);
            int totalsoldshares = 0;
            int invshares = 0;
            int invsoldshares = 0;

            //Investments Sold
            List<Investment> soldInvIds = new List<Investment>();

            //apply FIFO rules
            foreach (var investment in investments.OrderByDescending(i => i.Date))
            {

                if ((sellInvestment.Shares - totalsoldshares) > investment.Shares)
                {
                    //calculate shares
                    invsoldshares = investment.Shares;
                    totalsoldshares = totalsoldshares + invsoldshares;
                    //calculate profit
                    cprofit = cprofit + (invsoldshares * (sellInvestment.Rate - investment.Cost));
                    //calculate costbasis
                    costbasis = costbasis + invsoldshares * investment.Cost;
                    //flag investment as sold
                    soldInvIds.Add(investment);
                }
                else
                {
                    //calculate shares
                    invsoldshares = sellInvestment.Shares - totalsoldshares;
                    totalsoldshares = totalsoldshares + invsoldshares;
                    //calculate profit
                    cprofit = cprofit + (invsoldshares * (sellInvestment.Rate - investment.Cost));
                    //calculate costbasis
                    costbasis = costbasis + invsoldshares * investment.Cost;
                    //Sell only amount of shares necessary
                    investment.Shares = investment.Shares - invsoldshares;
                    invshares = investment.Shares;
                    break;
                }
            }

            var RemainingInvestments = investments.ToList().Where(p => soldInvIds.All(p2 => p2.Id != p.Id));

            //calculate costbasis
            costbasis = costbasis / totalsoldshares;

            var result = new TrxResult(
                totalshares - totalsoldshares,
                invshares,
                (RemainingInvestments.Sum(x => x.Shares * x.Cost) / RemainingInvestments.Sum(y => y.Shares)), //same formula as used in the investments list view.
                cprofit,
                costbasis
                );

            return result;
        }

        public async Task DeleteInvestment(int id)
        {
            await _InvestmentRepository.DeleteInvestment(id);
            await _InvestmentRepository.Save();
        }

    }
}
