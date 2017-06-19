using System;
using System.Collections.Generic;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.OrderService
{
    public class OrderService : IOrderService
    {
        private IUnitOfWork DataBase { get; set; }

        public OrderService(IUnitOfWork unitOfWorkow)
        {
            DataBase = unitOfWorkow;
        }

        public ManagerDTO GetManager(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id менеджера", "");
            var manager = DataBase.Managers.Get(id.Value);
            if (manager == null)
                throw new ValidationException("Не найден менеджер", "");
            Mapper.Initialize(cfg => cfg.CreateMap<Manager, ManagerDTO>());

            return Mapper.Map<Manager, ManagerDTO>(manager);
        }

        public ProductDTO GetProduct(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id товара", "");
            var product = DataBase.Products.Get(id.Value);
            if (product == null)
                throw new ValidationException("Товар не найден", "");
            Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductDTO>());
            return Mapper.Map<Product, ProductDTO>(product);
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductDTO>());
            return Mapper.Map<IEnumerable<Product>, List<ProductDTO>>(DataBase.Products.GetAll());
        }

        public void MakeOrder(OrderDTO orderDto)
        {
            Product product = DataBase.Products.Get(orderDto.ProductId);

            if (product == null)
                throw new ValidationException("Товар не найден!", "");
            Order order = new Order
            {
                ClientId = orderDto.ClientId,
                OrderDate =  DateTime.Now,
                ManagerId = orderDto.ManagerId,
                ProductId = product.Id,
            };
            DataBase.Orders.Create(order);
            DataBase.Save();
        }

        public IEnumerable<OrderDTO> GetOrdersByClient(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id клиента", "");
            var client = DataBase.Clients.Get(id.Value);
            if (client == null)
                throw new ValidationException("Клиент не найден", "");
            var orders = DataBase.Orders.Find(x => x.ClientId == client.Id);
            Mapper.Initialize(cfg => cfg.CreateMap<Order, OrderDTO>());

            return Mapper.Map<IEnumerable<Order>, List<OrderDTO>>(orders);
        }
        public IEnumerable<OrderDTO> GetOrdersByManager(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id менеджера", "");
            var client = DataBase.Clients.Get(id.Value);
            if (client == null)
                throw new ValidationException("Менеджер не найден", "");
            var orders = DataBase.Orders.Find(x => x.ClientId == client.Id);
            if (orders == null)
                throw new ValidationException("Заказы не найдены", "");
            Mapper.Initialize(cfg => cfg.CreateMap<Order, OrderDTO>());

            return Mapper.Map<IEnumerable<Order>, List<OrderDTO>>(orders);
        }

        public ClientDTO GetClient(int? clientId)
        {
            if (clientId == null)
                throw new ValidationException("Не установлено id клиента", "");
            var client = DataBase.Clients.Get(clientId.Value);
            if (client == null)
                throw new ValidationException("Клиент не найден", "");
            Mapper.Initialize(cfg => cfg.CreateMap<Client, ClientDTO>());
            return Mapper.Map<Client, ClientDTO>(client);
        }

        public IEnumerable<ClientDTO> GetClients()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Client, ClientDTO>());
            return Mapper.Map<IEnumerable<Client>, List<ClientDTO>>(DataBase.Clients.GetAll());
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }
        public IEnumerable<ManagerDTO> GetManagers()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Manager, ManagerDTO>());
            return Mapper.Map<IEnumerable<Manager>, List<ManagerDTO>>(DataBase.Managers.GetAll());
        }
    }
}

