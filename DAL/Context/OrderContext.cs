using System.Data.Entity;
using DAL.Entities;

namespace DAL.Context
{
    public class OrderContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        static OrderContext()
        {
            Database.SetInitializer<OrderContext>(new StoreDbInitializer());
        }

        public OrderContext(string connectionString)
            : base(connectionString)
        {

            Database.Initialize(true);
        }

        
    }
}
