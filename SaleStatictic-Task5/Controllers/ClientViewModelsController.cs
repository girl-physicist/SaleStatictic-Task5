using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SaleStatictic_Task5.Models;

namespace SaleStatictic_Task5.Controllers
{
    public class ClientViewModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ClientViewModels
        public ActionResult Index()
        {
            return View(db.ClientViewModels.ToList());
        }

        // GET: ClientViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientViewModel clientViewModel = db.ClientViewModels.Find(id);
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
                db.ClientViewModels.Add(clientViewModel);
                db.SaveChanges();
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
            ClientViewModel clientViewModel = db.ClientViewModels.Find(id);
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
                db.Entry(clientViewModel).State = EntityState.Modified;
                db.SaveChanges();
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
            ClientViewModel clientViewModel = db.ClientViewModels.Find(id);
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
            ClientViewModel clientViewModel = db.ClientViewModels.Find(id);
            db.ClientViewModels.Remove(clientViewModel);
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
