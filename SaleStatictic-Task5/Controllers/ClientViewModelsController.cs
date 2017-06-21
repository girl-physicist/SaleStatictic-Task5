using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using SaleStatictic_Task5.Models;

namespace SaleStatictic_Task5.Controllers
{
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
            var clients = GetAllClientViewModels();
            ViewBag.Clients = clients;
            return View(clients);
        }

        // GET: ClientViewModels/Details/5
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClientName")] ClientViewModel clientViewModel)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<ClientViewModel, ClientDTO>());
                var client = Mapper.Map<ClientViewModel, ClientDTO>(clientViewModel);
                _clientService.AddClient(client);
                return RedirectToAction("Index");
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClientName")] ClientViewModel clientViewModel)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<ClientViewModel, ClientDTO>());
                var client = Mapper.Map<ClientViewModel, ClientDTO>(clientViewModel);
                _clientService.UpdateClient(client);
                return RedirectToAction("Index");
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
