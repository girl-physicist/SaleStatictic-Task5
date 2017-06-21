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
    public class OrderService :Service,IOrderService 
    {
        public OrderService(IUnitOfWork unitOfWorkow) : base(unitOfWorkow)
        {
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
                ProductId = product.Id
            };
            DataBase.Orders.Create(order);
            DataBase.Save();
        }
        public void DeleteOrder(OrderDTO orderDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<OrderDTO, Order>());
            var order = Mapper.Map<OrderDTO, Order>(orderDto);
            DataBase.Products.Delete(order.Id);
            DataBase.Save();
        }

        public IEnumerable<OrderDTO> GetAllOrders()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Order, OrderDTO>());
            return Mapper.Map<IEnumerable<Order>, List<OrderDTO>>(DataBase.Orders.GetAll());
        }

        public OrderDTO GetOrder(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id заказа", "");
            var order = DataBase.Orders.Get(id.Value);
            if (order == null)
                throw new ValidationException("Не найден заказ", "");
            Mapper.Initialize(cfg => cfg.CreateMap<Order, OrderDTO>());
            return Mapper.Map<Order, OrderDTO>(order);
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
        public IEnumerable<OrderDTO> GetOrdersByProduct(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id продукта", "");
            var product = DataBase.Products.Get(id.Value);
            if (product == null)
                throw new ValidationException("Продукт не найден", "");
            var orders = DataBase.Orders.Find(x => x.ProductId == product.Id);
            if (orders == null)
                throw new ValidationException("Заказы не найдены", "");
            Mapper.Initialize(cfg => cfg.CreateMap<Order, OrderDTO>());
            return Mapper.Map<IEnumerable<Order>, List<OrderDTO>>(orders);
        }
    }
}

