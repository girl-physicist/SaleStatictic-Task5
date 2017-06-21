using System;
using DAL.Entities;

namespace DAL.Interfaces
{
   public interface IUnitOfWork : IDisposable 
    {
        IRepository<Client> Clients { get; }
        IRepository<Manager> Managers { get; }
        IRepository<Product> Products { get; }
        IRepository<Order> Orders { get; }
      //  IRepository<T>DbSet { get; }
        void Save();
    }
    
}
