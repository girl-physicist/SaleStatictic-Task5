using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SaleStatistic.Startup))]
namespace SaleStatistic
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
