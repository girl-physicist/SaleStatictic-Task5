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
using SaleStatictic_Task5.Models;
using SaleStatictic_Task5.Models.ViewModels;
using BLL.Interfaces;

namespace SaleStatictic_Task5.Controllers
{
    public class ManagerViewModelsController : Controller
    {
        readonly IManagerService _managerService;
        public ManagerViewModelsController(IManagerService serv) 
        {
            _managerService = serv;
        }
      
        public IEnumerable<ManagerViewModel> GetAllManagerViewModels()
        {
            var managersDTO =_managerService .GetManagers();
            Mapper.Initialize(cfg => cfg.CreateMap<ManagerDTO, ManagerViewModel>());
            var managers = Mapper.Map<IEnumerable<ManagerDTO>, List<ManagerViewModel>>(managersDTO);
            return managers;
        }
        // GET: ManagerViewModels
        public ActionResult Index()
        {
            var managers = GetAllManagerViewModels();
            ViewBag.Managers = managers;
            return View(managers);
        }

        // GET: ManagerViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           ManagerViewModel managerViewModel =GetAllManagerViewModels().FirstOrDefault(x=>x.Id.Equals(id));
            if (managerViewModel == null)
            {
                return HttpNotFound();
            }
            return View(managerViewModel);
        }

        // GET: ManagerViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManagerViewModels/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ManagerName")] ManagerViewModel managerViewModel)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<ManagerViewModel, ManagerDTO>());
                var manager = Mapper.Map<ManagerViewModel, ManagerDTO>(managerViewModel);
                _managerService.AddManager(manager);
                return RedirectToAction("Index");
            }

            return View(managerViewModel);
        }

        // GET: ManagerViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManagerViewModel managerViewModel = GetAllManagerViewModels().FirstOrDefault(x => x.Id.Equals(id));
            if (managerViewModel == null)
            {
                return HttpNotFound();
            }
            return View(managerViewModel);
        }

        // POST: ManagerViewModels/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ManagerName")] ManagerViewModel managerViewModel)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<ManagerViewModel, ManagerDTO>());
                var manager = Mapper.Map<ManagerViewModel, ManagerDTO>(managerViewModel);
                _managerService.UpdateManager(manager);
                return RedirectToAction("Index");
            }
            return View(managerViewModel);
        }

        // GET: ManagerViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManagerViewModel managerViewModel = GetAllManagerViewModels().FirstOrDefault(x => x.Id.Equals(id));
            if (managerViewModel == null)
            {
                return HttpNotFound();
            }
            return View(managerViewModel);
        }
        // POST: ManagerViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ManagerViewModel managerViewModel = GetAllManagerViewModels().FirstOrDefault(x => x.Id.Equals(id));
            Mapper.Initialize(cfg => cfg.CreateMap<ManagerViewModel, ManagerDTO>());
            var manager = Mapper.Map<ManagerViewModel, ManagerDTO>(managerViewModel);
           _managerService.RemoveManager(manager);
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _managerService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
