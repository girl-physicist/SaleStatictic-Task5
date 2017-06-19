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
    public class OrderViewModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderViewModels
        public ActionResult Index()
        {
            return View(db.OrderViewModels.ToList());
        }

        // GET: OrderViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderViewModel orderViewModel = db.OrderViewModels.Find(id);
            if (orderViewModel == null)
            {
                return HttpNotFound();
            }
            return View(orderViewModel);
        }

        // GET: OrderViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderViewModels/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,ProductId,ClientId,ManagerId")] OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                db.OrderViewModels.Add(orderViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderViewModel);
        }

        // GET: OrderViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderViewModel orderViewModel = db.OrderViewModels.Find(id);
            if (orderViewModel == null)
            {
                return HttpNotFound();
            }
            return View(orderViewModel);
        }

        // POST: OrderViewModels/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,ProductId,ClientId,ManagerId")] OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderViewModel);
        }

        // GET: OrderViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderViewModel orderViewModel = db.OrderViewModels.Find(id);
            if (orderViewModel == null)
            {
                return HttpNotFound();
            }
            return View(orderViewModel);
        }

        // POST: OrderViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderViewModel orderViewModel = db.OrderViewModels.Find(id);
            db.OrderViewModels.Remove(orderViewModel);
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
