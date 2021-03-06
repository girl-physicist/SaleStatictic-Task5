﻿using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IOrderService : IService
    {
        void MakeOrder(OrderDTO orderDto);
        void DeleteOrder(OrderDTO orderDto);
        void Update(OrderDTO orderDto);
        OrderDTO GetOrder(int? id);
        IEnumerable<OrderDTO> GetAllOrders();
    }
}
