using System.Data.Entity;
using DAL.Entities;

namespace DAL.Context
{
    public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<OrderContext>
    {
        protected override void Seed(OrderContext db)
        {
            db.Products.Add(new Product {ProductName = "Product1", ProductCost = 1});
            db.Products.Add(new Product {ProductName = "Product2", ProductCost = 2});

            db.Managers.Add(new Manager {ManagerName = "Manager1"});
            db.Managers.Add(new Manager {ManagerName = "Manager2"});

            db.Clients.Add(new Client {ClientName = "Client1"});
            db.Clients.Add(new Client {ClientName = "Client2"});
            db.SaveChanges();
        }
    }
}
