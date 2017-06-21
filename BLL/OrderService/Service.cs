using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.OrderService
{
    public abstract class Service:IService
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
    }
}
