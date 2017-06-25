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
using BLL.Infrastructure;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SaleStatictic_Task5.Models;

namespace SaleStatictic_Task5.Controllers
{
    [Authorize]
    public class OrderViewModelsController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderViewModelsController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        private IEnumerable<OrderViewModel> GetAllOrderViewModels()
        {
            var ordersDTO = _orderService.GetAllOrders();
            Mapper.Initialize(cfg => cfg.CreateMap<OrderDTO, OrderViewModel>());
            var orders = Mapper.Map<IEnumerable<OrderDTO>, List<OrderViewModel>>(ordersDTO);
            return orders;
        }
        // GET: OrderViewModels
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
                var orders = GetAllOrderViewModels();
                ViewBag.Orders = orders;
                return View(orders);
            }
            return RedirectToAction("OrderView");
        }
        public ActionResult OrderView()
        {
            var orders = GetAllOrderViewModels();
            ViewBag.Orders = orders;
            return PartialView(orders);
        }

        // GET: OrderViewModels/Details/5
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "Id,Date,ClientName,ManagerName,ProductName")] OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var orderDto = new OrderDTO
                    {
                        ClientName = orderViewModel.ClientName,
                        ManagerName = orderViewModel.ManagerName,
                        ProductName = orderViewModel.ProductName,
                        Date = orderViewModel.Date
                    };
                    _orderService.MakeOrder(orderDto);
                    return RedirectToAction("Index");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
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
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "Id,Date,ClientName,ManagerName,ProductName")] OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<OrderViewModel, OrderDTO>());
                var order = Mapper.Map<OrderViewModel, OrderDTO>(orderViewModel);
                _orderService.Update(order);
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
        [Authorize(Roles = "admin")]
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
