using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.OrderService
{
  public abstract class HelperMapping<T,K> where T:class where K:DTO.DTO
    {
        protected K MappingEntityToDTO(T param)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<T, K>());
            return Mapper.Map<T,K>(param);
        }
    }
}
