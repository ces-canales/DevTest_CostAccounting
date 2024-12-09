using BusinessLogicLayer.Services.Contracts;
using BusinessLogicLayer.Services.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevTest_CostAccounting.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ClientDto> clients = await _clientService.GetClients();
            return View(clients);
        }

        // GET: ClientController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            var client = new CreateClientDto(collection["Name"], collection["email"] );
            try
            {
                await _clientService.InsertClient(client);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ClientDto client = await _clientService.GetClientById(id);
            UpdateClientDto uclient = new UpdateClientDto(client.Name,client.Email);
            return View(uclient);
        }

        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            var client = new UpdateClientDto(collection["Name"], collection["Email"]);
            try
            {
                _clientService.UpdateClient(id, client);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            await _clientService.DeleteClient(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: ClientController/Delete/5
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
