using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Service
{
    public class ClientServise : Service<Client, ClientDTO>, IClientService
    {
        public ClientServise(IUnitOfWork unitOfWorkow) : base(unitOfWorkow)
        {
        }
        public ClientDTO GetClient(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id менеджера", "");
            var client = DataBase.Clients.Get(id.Value);
            if (client == null)
                throw new ValidationException("Не найден менеджер", "");
            return MapperEntityToDTO(client);
        }

        public void AddClient(ClientDTO clientDto)
        {
            DataBase.Clients.Create(MapperDTOtoEntity(clientDto));
            DataBase.Save();
        }

        public void UpdateClient(ClientDTO clientDto)
        {
            DataBase.Clients.Update(MapperDTOtoEntity(clientDto));
            DataBase.Save();
        }

        public void RemoveClient(ClientDTO clientDto)
        {
            DataBase.Clients.Delete(MapperDTOtoEntity(clientDto).Id);
            DataBase.Save();
        }

        public IEnumerable<ClientDTO> GetClients()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Client, ClientDTO>());
            return Mapper.Map<IEnumerable<Client>, List<ClientDTO>>(DataBase.Clients.GetAll());
        }
    }
}
