using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
