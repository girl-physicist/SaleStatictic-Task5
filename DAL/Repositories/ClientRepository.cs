using DAL.Context;
using DAL.Entities;

namespace DAL.Repositories
{
   public class ClientRepository:GenericDalRepository<Client>
    {
        public ClientRepository(OrderContext context) : base(context)
        {
        }
    }
}
