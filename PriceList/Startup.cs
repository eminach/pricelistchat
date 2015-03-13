using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PriceList.Startup))]
namespace PriceList
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
            
        }
    }
}
