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
    public class ClientServise : Service,IClientService
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
            Mapper.Initialize(cfg => cfg.CreateMap<Client, ClientDTO>());
            return Mapper.Map<Client, ClientDTO>(client);
        }

        public void AddClient(ClientDTO clientDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ClientDTO, Client>());
            var client = Mapper.Map<ClientDTO, Client>(clientDto);
            DataBase.Clients.Create(client);
            DataBase.Save();
        }

        public void UpdateClient(ClientDTO clientDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ClientDTO, Client>());
            var client = Mapper.Map<ClientDTO, Client>(clientDto);
            DataBase.Clients.Update(client);
            DataBase.Save();
        }

        public void RemoveClient(ClientDTO clientDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ClientDTO, Client>());
            var client = Mapper.Map<ClientDTO, Client>(clientDto);
            DataBase.Clients.Delete(client.Id);
            DataBase.Save();
        }

        public IEnumerable<ClientDTO> GetClients()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Client, ClientDTO>());
            return Mapper.Map<IEnumerable<Client>, List<ClientDTO>>(DataBase.Clients.GetAll());
        }
    }
}
