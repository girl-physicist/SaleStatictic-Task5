using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using SaleStatictic_Task5.Models;
using SaleStatictic_Task5.Models.ViewModels;

namespace SaleStatictic_Task5.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IClientService _clientService;
        private readonly IManagerService _managerService;
        public HomeController(IOrderService orderService, IManagerService managerService, IClientService clientService, IProductService productService)
        {
            _orderService = orderService;
            _managerService = managerService;
            _clientService = clientService;
            _productService = productService;
        }
        //[Authorize]
        public ActionResult Index()
        {
            IEnumerable<ProductDTO> productDtos = _productService.GetProducts();
            Mapper.Initialize(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>());
            var products = Mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productDtos);
            ViewBag.Products = products;
            return View();
        }

        public ActionResult Managers()
        {
            IEnumerable<ManagerDTO> managerDtos = _managerService.GetManagers();
            Mapper.Initialize(cfg => cfg.CreateMap<ManagerDTO, ManagerViewModel>());
            var managers = Mapper.Map<IEnumerable<ManagerDTO>, List<ManagerViewModel>>(managerDtos);
            return PartialView(managers);
        }
        public JsonResult GetProducts()
        {
            IEnumerable<ProductDTO> productDtos = _productService.GetProducts();
            Mapper.Initialize(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>());
            var result = Mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productDtos);

            return Json(new { Products = result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Clients()
        {
            IEnumerable<ClientDTO> managerDtos = _clientService.GetClients();
            Mapper.Initialize(cfg => cfg.CreateMap<ClientDTO, ClientViewModel>());
            var clients = Mapper.Map<IEnumerable<ClientDTO>, List<ClientViewModel>>(managerDtos);
            return PartialView(clients);
        }
        public ActionResult Products()
        {
            IEnumerable<ProductDTO> productDtos = _productService.GetProducts();
            Mapper.Initialize(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>());
            var products = Mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productDtos);
            return PartialView(products);
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
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<OrderViewModel, OrderDTO>());
                var orderDto = Mapper.Map<OrderViewModel, OrderDTO>(order);
                _orderService.MakeOrder(orderDto);
                return Content("<h2>Ваш заказ успешно оформлен</h2>");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(order);
        }
        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
            _orderService.Dispose();
            base.Dispose(disposing);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult ProductSearch(string name)
        {
            var allProduct = _productService.GetProducts().Where(x => x.ProductName.Contains(name)).ToList();
            Mapper.Initialize(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>());
            var products = Mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(allProduct);
            if (products.Count <= 0)
            {
                return HttpNotFound();
            }
            return PartialView(products);
        }
        [Authorize(Roles = "admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}