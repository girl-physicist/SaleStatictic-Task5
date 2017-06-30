using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using SaleStatictic_Task5.Models;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace SaleStatictic_Task5.Controllers
{
    [Authorize]
    public class ManagerViewModelsController : Controller
    {
        readonly IManagerService _managerService;
        public ManagerViewModelsController(IManagerService serv)
        {
            _managerService = serv;
        }

        private IEnumerable<ManagerViewModel> GetAllManagerViewModels()
        {
            var managersDTO = _managerService.GetManagers();
            Mapper.Initialize(cfg => cfg.CreateMap<ManagerDTO, ManagerViewModel>());
            var managers = Mapper.Map<IEnumerable<ManagerDTO>, List<ManagerViewModel>>(managersDTO);
            return managers;
        }
        // GET: ManagerViewModels
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
                var managers = GetAllManagerViewModels();
                ViewBag.Managers = managers;
                return View(managers);
            }
            return RedirectToAction("ManagerView");
        }
        public ActionResult ManagerView()
        {
            var managers = GetAllManagerViewModels();
            ViewBag.Managers = managers;
            return PartialView(managers);
        }

        // GET: ManagerViewModels/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
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
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "Id,ManagerName")] ManagerViewModel managerViewModel)
        {
            if (string.IsNullOrEmpty(managerViewModel.ManagerName))
            {
                ModelState.AddModelError("ManagerName", "Ваедите имя менеджера");
            }
            else if (managerViewModel.ManagerName.Length < 3)
            {
                ModelState.AddModelError("ManagerName", "Недопустимая длина строки");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<ManagerViewModel, ManagerDTO>());
                    var manager = Mapper.Map<ManagerViewModel, ManagerDTO>(managerViewModel);
                    _managerService.AddManager(manager);
                    return RedirectToAction("Index");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
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
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "Id,ManagerName")] ManagerViewModel managerViewModel)
        {
            if (string.IsNullOrEmpty(managerViewModel.ManagerName))
            {
                ModelState.AddModelError("ManagerName", "Ваедите имя менеджера");
            }
            else if (managerViewModel.ManagerName.Length < 3)
            {
                ModelState.AddModelError("ManagerName", "Недопустимая длина строки");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<ManagerViewModel, ManagerDTO>());
                    var manager = Mapper.Map<ManagerViewModel, ManagerDTO>(managerViewModel);
                    _managerService.UpdateManager(manager);
                    return RedirectToAction("Index");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
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
        [Authorize(Roles = "admin")]
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
