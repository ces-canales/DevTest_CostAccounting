using System.Diagnostics;
using System.Text;
using BusinessLogicLayer.Services.Contracts;
using BusinessLogicLayer.Services.Dtos;
using DataAccessLayer.DataContext;
using DevTest_CostAccounting.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevTest_CostAccounting.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly IClientService _clientService;

        public HomeController(IClientService clientService)
        {
            _clientService = clientService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
