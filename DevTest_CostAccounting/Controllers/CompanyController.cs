using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.Contracts;
using BusinessLogicLayer.Services.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevTest_CostAccounting.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // GET: CompanyController
        public async Task<IActionResult> Index()
        {
            IEnumerable<CompanyDto> companies = await _companyService.GetCompanies();
            return View(companies);
        }

        // GET: CompanyController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CompanyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            var company = new CreateCompanyDto(collection["Name"], Decimal.Parse(collection["SharePrice"]));
            try
            {
                await _companyService.InsertCompany(company);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CompanyController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            CompanyDto company = await _companyService.GetCompanyById(id);
            UpdateCompanyDto ucompany = new UpdateCompanyDto(company.Name, company.SharePrice);
            return View(ucompany);
        }

        // POST: CompanyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            var company = new UpdateCompanyDto(collection["Name"], Decimal.Parse(collection["SharePrice"]));
            try
            {
                _companyService.UpdateCompany(id, company);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CompanyController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            await _companyService.DeleteCompany(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: CompanyController/Delete/5
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
