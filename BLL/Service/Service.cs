using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Service
{
    public abstract class Service <Entity,DTO>:IService where DTO:class where Entity : class
    {
        protected IUnitOfWork DataBase { get; set; }
        protected Service(IUnitOfWork unitOfWorkow)
        {
            DataBase = unitOfWorkow;
        }
        public void Dispose()
        {
            DataBase.Dispose();
        }
        protected static Entity MapperDTOtoEntity(DTO objDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DTO, Entity>());
            return Mapper.Map<DTO, Entity>(objDto);
        }

        protected static DTO MapperEntityToDTO(Entity obj)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Entity, DTO>());
            return Mapper.Map<Entity, DTO>(obj);
        }
    }
}
