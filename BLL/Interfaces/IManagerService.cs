using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
   public interface IManagerService:IService
    {
        void AddManager(ManagerDTO managerDto);
        void UpdateManager(ManagerDTO managerDto);
        void RemoveManager(ManagerDTO managerDto);
        ManagerDTO GetManager(int? id);
        IEnumerable<ManagerDTO> GetManagers();
    }
}
