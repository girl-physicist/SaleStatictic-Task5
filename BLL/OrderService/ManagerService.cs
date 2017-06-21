using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.OrderService
{
    public class ManagerService : Service,IManagerService
    {
        public ManagerService(IUnitOfWork unitOfWorkow) : base(unitOfWorkow)
        {
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
        public void AddManager(ManagerDTO managerDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ManagerDTO, Manager>());
            var manager = Mapper.Map<ManagerDTO, Manager>(managerDto);
            DataBase.Managers.Create(manager);
            DataBase.Save();
        }
        public void UpdateManager(ManagerDTO managerDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ManagerDTO, Manager>());
            var manager = Mapper.Map<ManagerDTO, Manager>(managerDto);
            DataBase.Managers.Update(manager);
            DataBase.Save();
        }
        public void RemoveManager(ManagerDTO managerDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ManagerDTO, Manager>());
            var manager = Mapper.Map<ManagerDTO, Manager>(managerDto);
            DataBase.Managers.Delete(manager.Id);
            DataBase.Save();
        }
        public IEnumerable<ManagerDTO> GetManagers()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Manager, ManagerDTO>());
            return Mapper.Map<IEnumerable<Manager>, List<ManagerDTO>>(DataBase.Managers.GetAll());
        }
    }
}
