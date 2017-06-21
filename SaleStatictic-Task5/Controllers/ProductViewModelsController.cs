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
            var products = GetAllProductViewModels();
            ViewBag.Products = products;
            return View(products);
        }

        // GET: ProductViewModels/Details/5
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
        public ActionResult Create([Bind(Include = "Id,ProductName,ProductCost")] ProductViewModel productViewModel)
        {
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
        public ActionResult Edit([Bind(Include = "Id,ProductName,ProductCost")] ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<ProductViewModel, ProductDTO>());
                var product = Mapper.Map<ProductViewModel, ProductDTO>(productViewModel);
                _productService.UpdateProduct(product);
                return RedirectToAction("Index");
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
