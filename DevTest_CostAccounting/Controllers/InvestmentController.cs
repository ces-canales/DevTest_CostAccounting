using BusinessLogicLayer.Services.Contracts;
using BusinessLogicLayer.Services.Dtos;
using DevTest_CostAccounting.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevTest_CostAccounting.Controllers
{
    public class InvestmentController : Controller
    {
        private readonly IInvestmentService _investmentService;
        private readonly IClientService _clientService;
        private readonly ICompanyService _companyService;

        public InvestmentController(IInvestmentService investmentService, IClientService clientService, ICompanyService companyService)
        {
            _investmentService = investmentService;
            _clientService = clientService;
            _companyService = companyService;
        }
        // GET: InvestmentController
        public async Task<IActionResult> Index()
        {
            IEnumerable<InvestmentDto> investments = await _investmentService.GetInvestments();
            //fixing display issues non-working data annotations on "record"
            IEnumerable<InvestmentModel> finvestments = investments.Select(i => new InvestmentModel()
            {
                Id = i.Id,
                ClientId = i.ClientId,
                ClientName = i.ClientName,
                CompanyId = i.CompanyId,
                CompanyName = i.CompanyName,
                Date = i.Date,
                Shares = i.Shares,
                Cost = i.Cost
            });
            return View(finvestments.OrderBy(c=>c.ClientId ).ThenBy(n=>n.CompanyId));
        }

        // GET: InvestmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InvestmentController/Create
        public async Task<IActionResult> Create()
        {
            IEnumerable<ClientDto> clients = await _clientService.GetClients();
            IEnumerable<CompanyDto> companies = await _companyService.GetCompanies();
            ViewData["ClientList"] = clients.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewData["CompanyList"] = companies.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewData["CompanyPrice"] = companies.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.SharePrice.ToString() }).ToList();
            return View();
        }

        // POST: InvestmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            var investment = new CreateInvestmentDto(
                    Int16.Parse(collection["ClientId"]), 
                    Int16.Parse(collection["CompanyId"]),
                    DateTime.Parse(collection["Date"]),
                    Int16.Parse(collection["Shares"]), 
                    Decimal.Parse(collection["Cost"])
                  );
            try
            {
                await _investmentService.InsertInvestment(investment);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InvestmentController/Sell
        public async Task<IActionResult> Sell()
        {
            IEnumerable<ClientDto> clients = await _clientService.GetClients();
            IEnumerable<CompanyDto> companies = await _companyService.GetCompanies();
            ViewData["ClientList"] = clients.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewData["CompanyList"] = companies.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewData["AccountingMethods"] = new List<SelectListItem>() { new SelectListItem("FIFO", "1"), new SelectListItem("LIFO", "2") };
            return View();
        }

        // POST: InvestmentController/Sell
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sell(IFormCollection collection)
        {
            IEnumerable<ClientDto> clients = await _clientService.GetClients();
            IEnumerable<CompanyDto> companies = await _companyService.GetCompanies();
            ViewData["ClientList"] = clients.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewData["CompanyList"] = companies.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewData["AccountingMethods"] = new List<SelectListItem>() { new SelectListItem("FIFO", "1"), new SelectListItem("LIFO", "2") };
            var sale = new SellInvestmentDto(
                Int16.Parse(collection["ClientId"]),
                Int16.Parse(collection["CompanyId"]),
                DateTime.Parse(collection["Date"]),
                Int16.Parse(collection["Shares"]),
                Decimal.Parse(collection["Rate"]),
                Int16.Parse(collection["MethodId"])
            );

            SellInvestmentModel inv = new SellInvestmentModel()
            {
                ClientId = sale.ClientId,
                CompanyId = sale.CompanyId,
                Date = sale.Date,
                Shares = sale.Shares,
                Rate = sale.Rate,
                MethodId = sale.MethodId
            };

            try
            {
                TrxResult Trx = await _investmentService.SellInvestment(sale);
                inv.TrxResult = Trx;
            }
            catch
            {
                throw;
            }
            return View(inv);
        }

        // GET: InvestmentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InvestmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InvestmentController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _investmentService.DeleteInvestment(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: InvestmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
