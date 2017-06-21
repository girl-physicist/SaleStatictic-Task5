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
    public class OrderViewModelsController : Controller
    {
        readonly IOrderService _orderService;
        public OrderViewModelsController(IOrderService serv)
        {
            _orderService = serv;
        }
        public IEnumerable<OrderViewModel> GetAllOrderViewModels()
        {
            var ordersDTO = _orderService.GetAllOrders();
            Mapper.Initialize(cfg => cfg.CreateMap<OrderDTO, OrderViewModel>());
            var orders = Mapper.Map<IEnumerable<OrderDTO>, List<OrderViewModel>>(ordersDTO);
            return orders;
        }
        // GET: OrderViewModels
        public ActionResult Index()
        {
            var orders = GetAllOrderViewModels();
            ViewBag.Orders = orders;
            return View(orders);
        }

        // GET: OrderViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderViewModel orderViewModel = GetAllOrderViewModels().FirstOrDefault(x => x.Id.Equals(id));
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
        public ActionResult Create([Bind(Include = "Id,Date,ProductId,ClientId,OrderId")] OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<OrderViewModel, OrderDTO>());
                var order = Mapper.Map<OrderViewModel, OrderDTO>(orderViewModel);
                _orderService.MakeOrder(order);
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
            OrderViewModel orderViewModel = GetAllOrderViewModels().FirstOrDefault(x => x.Id.Equals(id));
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
        public ActionResult Edit([Bind(Include = "Id,Date,ProductId,ClientId,OrderId")] OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                //Mapper.Initialize(cfg => cfg.CreateMap<OrderViewModel, OrderDTO>());
                //var order = Mapper.Map<OrderViewModel, OrderDTO>(orderViewModel);
                //_orderService.UpdateOrder(order);
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
            OrderViewModel orderViewModel = GetAllOrderViewModels().FirstOrDefault(x => x.Id.Equals(id));
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
            OrderViewModel orderViewModel = GetAllOrderViewModels().FirstOrDefault(x => x.Id.Equals(id));
            Mapper.Initialize(cfg => cfg.CreateMap<OrderViewModel, OrderDTO>());
            var order = Mapper.Map<OrderViewModel, OrderDTO>(orderViewModel);
            _orderService.DeleteOrder(order);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _orderService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
