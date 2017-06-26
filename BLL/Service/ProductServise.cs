using System.Collections.Generic;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Service
{
    public class ProductServise : Service<Product, ProductDTO>, IProductService
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
            return MapperEntityToDTO(product);
        }

        public void AddProduct(ProductDTO productDto)
        {
            DataBase.Products.Create(MapperDTOtoEntity(productDto));
            DataBase.Save();
        }

        public void UpdateProduct(ProductDTO productDto)
        {
            DataBase.Products.Update(MapperDTOtoEntity(productDto));
            DataBase.Save();
        }

        public void RemoveProduct(ProductDTO productDto)
        {
            DataBase.Products.Delete(MapperDTOtoEntity(productDto).Id);
            DataBase.Save();
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductDTO>());
            return Mapper.Map<IEnumerable<Product>, List<ProductDTO>>(DataBase.Products.GetAll());
        }
    }
}
