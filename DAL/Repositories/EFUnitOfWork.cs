using System;
using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;
        private readonly OrderContext _db;
        private ClientRepository _clientRepository;
        private ManagerRepository _managerRepository;
        private ProductRepository _productRepository;
        private OrderRepository _orderRepository;

        public EFUnitOfWork(string connectionString)
        {
            _db = new OrderContext(connectionString);
        }
        public IRepository<Client> Clients
        {
            get
            {
                if (_clientRepository == null)
                    _clientRepository = new ClientRepository(_db);
                return _clientRepository;
            }
        }
        public IRepository<Manager> Managers
        {
            get
            {
                if( _managerRepository == null)
                    _managerRepository = new ManagerRepository(_db);
                return _managerRepository;
            }
        }
        public IRepository<Product> Products
        {
            get
            {
                if(_productRepository == null)
                    _productRepository = new ProductRepository(_db);
                return _productRepository;
            }
        }
        public IRepository<Order> Orders
        {
            get
            {
                if (_orderRepository == null)
                    _orderRepository = new OrderRepository(_db);
                return _orderRepository;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                _disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
