using DAL.Context;
using DAL.Entities;

namespace DAL.Repositories
{
 public   class ManagerRepository:GenericDalRepository<Manager>
    {
        public ManagerRepository(OrderContext context) : base(context)
        {
        }
    }
}
