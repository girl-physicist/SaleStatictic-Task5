using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IClientService:IService
    {
        void AddClient(ClientDTO managerDto);
        void UpdateClient(ClientDTO managerDto);
        void RemoveClient(ClientDTO managerDto);
        ClientDTO GetClient(int? id);
        IEnumerable<ClientDTO> GetClients();
    }
}
