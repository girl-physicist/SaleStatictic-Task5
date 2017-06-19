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
        readonly IOrderService _orderService;
        public HomeController(IOrderService serv)
        {
            _orderService = serv;
        }
        [Authorize]
        public ActionResult Index()
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //    switch (Roles.GetRolesForUser(User.Identity.Name).First())
            //    {
            //        case "admin":
            //            return RedirectToAction("Index", "OrderViewModels");
            //        case "user":
            //            return RedirectToAction("Index", "ClientViewModels");
            //    }
            //}
            //else
            //    return RedirectToAction("Login", "Account");

            //return RedirectToAction("Login", "Account");




            IEnumerable<ProductDTO> productDtos = _orderService.GetProducts();
            Mapper.Initialize(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>());
            var products = Mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productDtos);
            ViewBag.Products = products;
            return View();
        }
        [HttpGet]
        public ActionResult MakeOrder(int? id)
        {
            try
            {
                ProductDTO product = _orderService.GetProduct(id);
                Mapper.Initialize(cfg => cfg.CreateMap<ProductDTO, OrderViewModel>()
                    .ForMember("ProductId", opt => opt.MapFrom(src => src.Id)));
                var order = Mapper.Map<ProductDTO, OrderViewModel>(product);
                return View(order);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
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
            _orderService.Dispose();
            base.Dispose(disposing);
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