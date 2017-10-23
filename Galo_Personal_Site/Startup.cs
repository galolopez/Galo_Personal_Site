using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Galo_Personal_Site.Startup))]
namespace Galo_Personal_Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
