using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SaleStatictic_Task5.Startup))]
namespace SaleStatictic_Task5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
