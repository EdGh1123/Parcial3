using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Parcial3.Startup))]
namespace Parcial3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
