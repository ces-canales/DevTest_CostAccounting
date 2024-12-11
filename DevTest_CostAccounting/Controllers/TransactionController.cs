using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.Contracts;
using BusinessLogicLayer.Services.Dtos;
using DataAccessLayer.Entities;
using DevTest_CostAccounting.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevTest_CostAccounting.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET: TransactionController
        public async Task<IActionResult> Index(int typeid)
        {
            IEnumerable<TransactionDto> transactions = await _transactionService.GetTransactions();
            IEnumerable<TransactionModel> ftransactions = transactions.Select(i => new TransactionModel()
            {
                Id = i.Id,
                ClientId = i.ClientId,
                ClientName = i.ClientName,
                CompanyId = i.CompanyId,
                CompanyName = i.CompanyName,
                Date = i.Date,
                TypeId = i.TypeId,
                Shares = i.Shares,
                Rate = i.Rate,
                SaleProfit = i.SaleProfit,
                SaleCostBasis = i.SaleCostBasis
            });

            if (typeid == 1 || typeid ==2)
            {
                transactions = transactions.Where(x => x.TypeId == typeid).ToList();
            }
            ViewBag.TypeId = typeid;
            return View(transactions);
        }

        // GET: TransactionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TransactionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransactionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: TransactionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TransactionController/Edit/5
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

        // GET: TransactionController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            await _transactionService.DeleteTransaction(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: TransactionController/Delete/5
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
