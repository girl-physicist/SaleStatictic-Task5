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
    public class ProductViewModelsController : Controller
    {
        readonly IProductService _productService;
        public ProductViewModelsController(IProductService serv)
        {
            _productService = serv;
        }
        public IEnumerable<ProductViewModel> GetAllProductViewModels()
        {
            var productsDTO = _productService.GetProducts();
            Mapper.Initialize(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>());
            var products = Mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productsDTO);
            return products;
        }
        // GET: ProductViewModels
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
                var products = GetAllProductViewModels();
                ViewBag.Products = products;
                return View(products);
            }
            return RedirectToAction("ProductView");
        }
        public ActionResult ProductView()
        {
            var products = GetAllProductViewModels();
            ViewBag.Products = products;
            return PartialView(products);
        }
        // GET: ProductViewModels/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductViewModel productViewModel = GetAllProductViewModels().FirstOrDefault(x => x.Id.Equals(id));
            if (productViewModel == null)
            {
                return HttpNotFound();
            }
            return View(productViewModel);
        }

        // GET: ProductViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductViewModels/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "Id,ProductName,ProductCost")] ProductViewModel productViewModel)
        {
            if (string.IsNullOrEmpty(productViewModel.ProductName))
            {
                ModelState.AddModelError("ProductName", "Поле должно быть заполнено");
            }
            else if (productViewModel.ProductName.Length < 3)
            {
                ModelState.AddModelError("ProductName", "Недопустимая длина строки");
            }
            else if (productViewModel.ProductCost < (decimal)0.01)
            {
                ModelState.AddModelError("ProductCost", "Введите цену товара больше '0,01'");
            }
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<ProductViewModel, ProductDTO>());
                var product = Mapper.Map<ProductViewModel, ProductDTO>(productViewModel);
                _productService.AddProduct(product);
                return RedirectToAction("Index");
            }
            return View(productViewModel);
        }

        // GET: ProductViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductViewModel productViewModel = GetAllProductViewModels().FirstOrDefault(x => x.Id.Equals(id));
            if (productViewModel == null)
            {
                return HttpNotFound();
            }
            return View(productViewModel);
        }

        // POST: ProductViewModels/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "Id,ProductName,ProductCost")] ProductViewModel productViewModel)
        {
            if (string.IsNullOrEmpty(productViewModel.ProductName) )
            {
                ModelState.AddModelError("ProductName", "Поле должно быть заполнено");
            }
            else if (productViewModel.ProductName.Length < 3)
            {
                ModelState.AddModelError("ProductName", "Недопустимая длина строки");
            }
            else if (productViewModel.ProductCost < (decimal)0.01)
            {
                ModelState.AddModelError("ProductCost", "Введите цену товара больше '0,01'");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<ProductViewModel, ProductDTO>());
                    var product = Mapper.Map<ProductViewModel, ProductDTO>(productViewModel);
                    _productService.UpdateProduct(product);
                    return RedirectToAction("Index");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }
            return View(productViewModel);
        }

        // GET: ProductViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductViewModel productViewModel = GetAllProductViewModels().FirstOrDefault(x => x.Id.Equals(id));
            if (productViewModel == null)
            {
                return HttpNotFound();
            }
            return View(productViewModel);
        }

        // POST: ProductViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductViewModel productViewModel = GetAllProductViewModels().FirstOrDefault(x => x.Id.Equals(id));
            Mapper.Initialize(cfg => cfg.CreateMap<ProductViewModel, ProductDTO>());
            var product = Mapper.Map<ProductViewModel, ProductDTO>(productViewModel);
            _productService.RemoveProduct(product);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _productService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
