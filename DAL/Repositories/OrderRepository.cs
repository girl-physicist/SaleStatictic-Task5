using DAL.Context;
using DAL.Entities;

namespace DAL.Repositories
{
    public class OrderRepository : GenericDalRepository<Order>
    {
        public OrderRepository(OrderContext context) : base(context)
        {
        }
    }
}
