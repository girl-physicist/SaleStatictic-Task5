using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
   public interface IProductService:IService
    {
        void AddProduct(ProductDTO managerDto);
        void UpdateProduct(ProductDTO managerDto);
        void RemoveProduct(ProductDTO managerDto);
        ProductDTO GetProduct(int? id);
        IEnumerable<ProductDTO> GetProducts();
    }
}
