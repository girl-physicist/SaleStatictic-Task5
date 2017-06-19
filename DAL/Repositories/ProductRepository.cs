using System.Collections.Generic;
using DAL.Context;
using DAL.Entities;

namespace DAL.Repositories
{
    public class ProductRepository:GenericDalRepository<Product>
    {
        public ProductRepository(OrderContext context) : base(context)
        {
        }
    }
}
