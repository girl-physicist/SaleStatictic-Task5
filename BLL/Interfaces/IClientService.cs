using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
