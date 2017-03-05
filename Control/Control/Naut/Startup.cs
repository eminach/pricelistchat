using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Naut.Startup))]
namespace Naut
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
