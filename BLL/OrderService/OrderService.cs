﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
        private static void AdjustmentAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<OrderDTO, Product>()
                    .ForMember(x => x.ProductName, opt => opt.MapFrom(src => src.ProductName));
                cfg.CreateMap<OrderDTO, Manager>()
                    .ForMember(x => x.ManagerName, opt => opt.MapFrom(src => src.ManagerName));
                cfg.CreateMap<OrderDTO, Client>()
                    .ForMember(x => x.ClientName, opt => opt.MapFrom(src => src.ClientName));
                cfg.CreateMap<OrderDTO, Order>()
                    .ForMember(x => x.Date, opt => opt.MapFrom(src => src.Date));
            });
        }
        public void MakeOrder(OrderDTO orderDto)
        {
            AdjustmentAutoMapper();
            var order = Mapper.Map<OrderDTO, Order>(orderDto);
            var manager = DataBase.Managers.GetAll().FirstOrDefault(x => x.ManagerName == orderDto.ManagerName);
            var product = DataBase.Products.GetAll().FirstOrDefault(x => x.ProductName == orderDto.ProductName);
            var client = DataBase.Clients.GetAll().FirstOrDefault(x => x.ClientName == orderDto.ClientName);

            if (product == null)
            {
                //product = Mapper.Map<OrderDTO, Product>(orderDto);
                //DataBase.Products.Create(product);
               throw new ValidationException("Товар не найден!", "");
            }
            if (client == null)
            {
                client = Mapper.Map<OrderDTO, Client>(orderDto);
                DataBase.Clients.Create(client);
            }
            if (manager == null)
            {
                manager = Mapper.Map<OrderDTO, Manager>(orderDto);
                DataBase.Managers.Create(manager);
            }
            order.Product = product;
            order.Client = client;
            order.Manager = manager;
            DataBase.Save();
        }
        public void DeleteOrder(int id)
        {
            DataBase.Orders.Delete(id);
            DataBase.Save();
        }
        public IEnumerable<OrderDTO> GetAllOrders()
        {
            var orders = new List<OrderDTO>();
            var allOrders = DataBase.Orders.GetAll().Include(x => x.Client)
                .Include(x => x.Manager).Include(x => x.Product);
            foreach (var order in allOrders)
            {
                var orderDto = new OrderDTO
                {
                    Id = order.Id,
                    ClientName = order.Client.ClientName,
                    ManagerName = order.Manager.ManagerName,
                    ProductName = order.Product.ProductName,
                    Date = order.Date
                };
                orders.Add(orderDto);
            }
            return orders;
        }

        public OrderDTO GetOrder(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id заказа", "");
            var order = GetAllOrders().FirstOrDefault(x => x.Id == id);
            if (order == null)
                throw new ValidationException("Не найден заказ", "");
            return order;
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
        public void Update(OrderDTO orderDto)
        {
            AdjustmentAutoMapper();
            var order = DataBase.Orders.Get(orderDto.Id);
            var manager = DataBase.Managers.GetAll().FirstOrDefault(x => x.ManagerName == orderDto.ManagerName);
            var product = DataBase.Products.GetAll().FirstOrDefault(x => x.ProductName == orderDto.ProductName);
            var client = DataBase.Clients.GetAll().FirstOrDefault(x => x.ClientName == orderDto.ClientName);

            if (manager == null)
            {
                manager = Mapper.Map<OrderDTO, Manager>(orderDto);
                DataBase.Managers.Create(manager);
            }
            if (product == null)
            {
                product = Mapper.Map<OrderDTO, Product>(orderDto);
                DataBase.Products.Create(product);
            }
            if (client == null)
            {
                client = Mapper.Map<OrderDTO, Client>(orderDto);
                DataBase.Clients.Create(client);
            }
            order.Date = orderDto.Date;
            order.Product = product;
            order.Client = client;
            order.Manager = manager;

            DataBase.Orders.Update(order);
            DataBase.Save();
        }

       
    }
}

