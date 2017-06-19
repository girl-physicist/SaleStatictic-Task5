using System.Collections.Generic;
using System.Data.Entity;
using DAL.Context;
using DAL.Entities;

namespace DAL.Repositories
{
   public class OrderRepository:GenericDalRepository<Order>
    {
        public OrderRepository(OrderContext context) : base(context)
        {
        }
        public override IEnumerable<Order> GetAll()
        {
            return Db.Orders.Include(o => o.ProductId);
        }
    }
}
