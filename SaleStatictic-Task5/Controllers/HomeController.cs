﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using SaleStatictic_Task5.Models;


namespace SaleStatictic_Task5.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IManagerService _managerService;
        private readonly IClientService _clientService;
        public HomeController(IOrderService orderService, IProductService productService, IManagerService managerService, IClientService clientService)
        {
            _orderService = orderService;
            _productService = productService;
            _managerService = managerService;
            _clientService = clientService;
        }
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Filter(string manager, string product,string client, string date)
        {
            var orders = _orderService.GetAllOrders();
            var managers = orders.Select(x => x.ManagerName).Distinct().ToList();
            managers.Insert(0, "All");
            var products = orders.Select(x => x.ProductName).Distinct().ToList();
            products.Insert(0, "All");
            var clients = orders.Select(x => x.ClientName).Distinct().ToList();
            clients.Insert(0, "All");
            var dates = orders.Select(x => x.Date.ToString("d")).Distinct().ToList();
            dates.Insert(0, "All");

            if (!string.IsNullOrEmpty(manager) && !manager.Equals("All"))
            {
                orders = orders.Where(x => x.ManagerName == manager);
            }

            if (!string.IsNullOrEmpty(product) && !product.Equals("All"))
            {
                orders = orders.Where(x => x.ProductName == product);
            }

            if (!string.IsNullOrEmpty(client) && !client.Equals("All"))
            {
                orders = orders.Where(x => x.ClientName == client);
            }

            if (!string.IsNullOrEmpty(date) && !date.Equals("All"))
            {
                orders = orders.Where(x => x.Date.ToString("d") == date);
            }
            var saleInfoViewModel = new SaleInfoViewModel
            {
                Orders = orders,
                Managers = new SelectList(managers),
                Products = new SelectList(products),
                Clients = new SelectList(clients),
                Dates = new SelectList(dates)
            };

            return PartialView(saleInfoViewModel);
        }
        public IEnumerable<ChartData> GetChartData()
        {
            IEnumerable<OrderDTO> orderDtos = _orderService.GetAllOrders();
            Mapper.Initialize(cfg => cfg.CreateMap<OrderDTO, OrderViewModel>());
            var result = Mapper.Map<IEnumerable<OrderDTO>, List<OrderViewModel>>(orderDtos);
            var data = result.GroupBy(x => x.ManagerName).Select(y => new ChartData
            {
                ManagerId = y.Key,
                CountClients = y.Select(m => m.ClientName).Distinct().Count()
            }).ToList();
            return data;
        }
        public JsonResult Piechart()
        {
            var list = GetChartData();
            return Json(new { Orders = list }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Orders()
        {
            IEnumerable<OrderDTO> orderDtos = _orderService.GetAllOrders();
            Mapper.Initialize(cfg => cfg.CreateMap<OrderDTO, OrderViewModel>());
            var orders = Mapper.Map<IEnumerable<OrderDTO>, List<OrderViewModel>>(orderDtos);
            return PartialView(orders);
        }
        [HttpGet]
        public ActionResult MakeOrder()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult MakeOrder(OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var orderDto = new OrderDTO
                    {
                        ClientName = order.ClientName,
                        ManagerName = order.ManagerName,
                        ProductName = order.ProductName,
                        Date = order.Date
                    };

                    _orderService.MakeOrder(orderDto);
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
                return RedirectToAction("Index", "Home");
            }
            return View(order);
        }
        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
            _orderService.Dispose();
            base.Dispose(disposing);
        }
    }
}