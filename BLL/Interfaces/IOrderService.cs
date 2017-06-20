using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
   public interface IOrderService
    {
        void MakeOrder(OrderDTO orderDto);
        void AddManager(ManagerDTO managerDto);
        void UpdateManager(ManagerDTO managerDto);
        void RemoveManager(ManagerDTO managerDto);
        ProductDTO GetProduct(int? id);
        ClientDTO GetClient(int? id);
        ManagerDTO GetManager(int? id);
        IEnumerable<ProductDTO> GetProducts();
        IEnumerable<ClientDTO> GetClients();
        IEnumerable<ManagerDTO> GetManagers();
        IEnumerable<OrderDTO> GetOrdersByClient(int? clientId);
        IEnumerable<OrderDTO> GetOrdersByManagerName(string managerName);
        IEnumerable<OrderDTO> GetOrdersByClientName(string clientName);
        void Dispose();
    }
}
