using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;
using BLL.Service;
using Ninject;

namespace SaleStatictic_Task5.Util
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            _kernel.Bind<IOrderService>().To<OrderService>();
            _kernel.Bind<IManagerService>().To<ManagerService>();
            _kernel.Bind<IClientService>().To<ClientServise>();
            _kernel.Bind<IProductService>().To<ProductServise>();
        }
    }
}