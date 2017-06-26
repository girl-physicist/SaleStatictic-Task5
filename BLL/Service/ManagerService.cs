using System.Collections.Generic;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Service
{
    public class ManagerService : Service<Manager, ManagerDTO>, IManagerService
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
            return MapperEntityToDTO(manager);
        }
        public void AddManager(ManagerDTO managerDto)
        {
            DataBase.Managers.Create(MapperDTOtoEntity(managerDto));
            DataBase.Save();
        }
        public void UpdateManager(ManagerDTO managerDto)
        {
            DataBase.Managers.Update(MapperDTOtoEntity(managerDto));
            DataBase.Save();
        }
        public void RemoveManager(ManagerDTO managerDto)
        {
            DataBase.Managers.Delete(MapperDTOtoEntity(managerDto).Id);
            DataBase.Save();
        }
        public IEnumerable<ManagerDTO> GetManagers()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Manager, ManagerDTO>());
            return Mapper.Map<IEnumerable<Manager>, List<ManagerDTO>>(DataBase.Managers.GetAll());
        }
    }
}
