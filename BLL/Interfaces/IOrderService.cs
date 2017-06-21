using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
   public interface IOrderService:IService
    {
        void MakeOrder(OrderDTO orderDto);
        void DeleteOrder(OrderDTO orderDto);
        OrderDTO GetOrder(int? id);
        IEnumerable<OrderDTO> GetAllOrders();
        IEnumerable<OrderDTO> GetOrdersByManager(int? managerId);
        IEnumerable<OrderDTO> GetOrdersByProduct(int? productId);
        IEnumerable<OrderDTO> GetOrdersByClient(int? clientId);
    }
}
