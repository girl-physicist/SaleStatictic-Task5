using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SaleStatictic_Task5.Models;
using SaleStatictic_Task5.Models.ViewModels;

namespace SaleStatictic_Task5.Controllers
{
    public class ManagerViewModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ManagerViewModels
        public ActionResult Index()
        {
            return View(db.ManagerViewModels.ToList());
        }

        // GET: ManagerViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManagerViewModel managerViewModel = db.ManagerViewModels.Find(id);
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
                db.ManagerViewModels.Add(managerViewModel);
                db.SaveChanges();
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
            ManagerViewModel managerViewModel = db.ManagerViewModels.Find(id);
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
                db.Entry(managerViewModel).State = EntityState.Modified;
                db.SaveChanges();
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
            ManagerViewModel managerViewModel = db.ManagerViewModels.Find(id);
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
            ManagerViewModel managerViewModel = db.ManagerViewModels.Find(id);
            db.ManagerViewModels.Remove(managerViewModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
