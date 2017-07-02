using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SaleStatictic_Task5.Models;


namespace SaleStatictic_Task5.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IOrderService _orderService;

        public HomeController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public ActionResult Index()
        {
            string role = null;
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                .GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
                role = userManager.GetRoles(user.Id).ElementAt(0);
            ViewBag.Role = role;
            return View();
        }
        public ActionResult Filter(string manager, string product, string client, string date)
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
                ManagerName = y.Key,
                CountClients = y.Select(m => m.ClientName).Distinct().Count()
            }).ToList();
            return data;
        }
        public JsonResult Piechart()
        {
            var list = GetChartData();
            return Json(new { Orders = list }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _orderService.Dispose();
            base.Dispose(disposing);
        }
    }
}