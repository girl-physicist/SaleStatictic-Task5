using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.OrderService
{
  public  class ProductServise : Service,IProductService
  {
      public ProductServise(IUnitOfWork unitOfWorkow) : base(unitOfWorkow)
      {
      }

      public ProductDTO GetProduct(int? id)
      {
          if (id == null)
              throw new ValidationException("Не установлено id менеджера", "");
          var product = DataBase.Products.Get(id.Value);
          if (product == null)
              throw new ValidationException("Не найден менеджер", "");
          Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductDTO>());
          return Mapper.Map<Product, ProductDTO>(product);
      }

      public void AddProduct(ProductDTO productDto)
      {
          Mapper.Initialize(cfg => cfg.CreateMap<ProductDTO, Product>());
          var product = Mapper.Map<ProductDTO, Product>(productDto);
          DataBase.Products.Create(product);
          DataBase.Save();
      }

      public void UpdateProduct(ProductDTO productDto)
      {
          Mapper.Initialize(cfg => cfg.CreateMap<ProductDTO, Product>());
          var product = Mapper.Map<ProductDTO, Product>(productDto);
          DataBase.Products.Update(product);
          DataBase.Save();
      }

      public void RemoveProduct(ProductDTO productDto)
      {
          Mapper.Initialize(cfg => cfg.CreateMap<ProductDTO, Product>());
          var product = Mapper.Map<ProductDTO, Product>(productDto);
          DataBase.Products.Delete(product.Id);
          DataBase.Save();
      }

      public IEnumerable<ProductDTO> GetProducts()
      {
          Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductDTO>());
          return Mapper.Map<IEnumerable<Product>, List<ProductDTO>>(DataBase.Products.GetAll());
      }
    }
}
