using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SaleStatictic_Task5.Models;

namespace SaleStatictic_Task5.Controllers
{
    [Authorize]
    public class ClientViewModelsController : Controller
    {
        readonly IClientService _clientService;
        public ClientViewModelsController(IClientService serv)
        {
            _clientService = serv;
        }
        public IEnumerable<ClientViewModel> GetAllClientViewModels()
        {
            var clientsDTO = _clientService.GetClients();
            Mapper.Initialize(cfg => cfg.CreateMap<ClientDTO, ClientViewModel>());
            var clients = Mapper.Map<IEnumerable<ClientDTO>, List<ClientViewModel>>(clientsDTO);
            return clients;
        }
        // GET: ClientViewModels
        public ActionResult Index()
        {
            string role = null;
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                .GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
                role = userManager.GetRoles(user.Id).ElementAt(0);
            if (role == "admin")
            {
                var clients = GetAllClientViewModels();
                ViewBag.Clients = clients;
                return View(clients);
            }
            return RedirectToAction("ClientView");
        }
        public ActionResult ClientView()
        {
            var clients = GetAllClientViewModels();
            ViewBag.Clients = clients;
            return PartialView(clients);
        }
        // GET: ClientViewModels/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientViewModel clientViewModel = GetAllClientViewModels().FirstOrDefault(x => x.Id.Equals(id));
            if (clientViewModel == null)
            {
                return HttpNotFound();
            }
            return View(clientViewModel);
        }

        // GET: ClientViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientViewModels/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClientName")] ClientViewModel clientViewModel)
        {
            if (string.IsNullOrEmpty(clientViewModel.ClientName))
            {
                ModelState.AddModelError("ClienName", "Ваедите имя клиента");
            }
            else if (clientViewModel.ClientName.Length < 3)
            {
                ModelState.AddModelError("ClientName", "Недопустимая длина строки");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<ClientViewModel, ClientDTO>());
                    var client = Mapper.Map<ClientViewModel, ClientDTO>(clientViewModel);
                    _clientService.AddClient(client);
                    return RedirectToAction("Index");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }
            return View(clientViewModel);
        }

        // GET: ClientViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientViewModel clientViewModel = GetAllClientViewModels().FirstOrDefault(x => x.Id.Equals(id));
            if (clientViewModel == null)
            {
                return HttpNotFound();
            }
            return View(clientViewModel);
        }

        // POST: ClientViewModels/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClientName")] ClientViewModel clientViewModel)
        {
            if (string.IsNullOrEmpty(clientViewModel.ClientName))
            {
                ModelState.AddModelError("ClienName", "Ваедите имя клиента");
            }
            else if (clientViewModel.ClientName.Length < 3)
            {
                ModelState.AddModelError("ClientName", "Недопустимая длина строки");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<ClientViewModel, ClientDTO>());
                    var client = Mapper.Map<ClientViewModel, ClientDTO>(clientViewModel);
                    _clientService.UpdateClient(client);
                    return RedirectToAction("Index");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }
            return View(clientViewModel);
        }

        // GET: ClientViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientViewModel clientViewModel = GetAllClientViewModels().FirstOrDefault(x => x.Id.Equals(id));
            if (clientViewModel == null)
            {
                return HttpNotFound();
            }
            return View(clientViewModel);
        }

        // POST: ClientViewModels/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientViewModel clientViewModel = GetAllClientViewModels().FirstOrDefault(x => x.Id.Equals(id));
            Mapper.Initialize(cfg => cfg.CreateMap<ClientViewModel, ClientDTO>());
            var client = Mapper.Map<ClientViewModel, ClientDTO>(clientViewModel);
            _clientService.RemoveClient(client);
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _clientService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
