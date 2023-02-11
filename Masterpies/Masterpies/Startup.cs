using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Masterpies.Startup))]
namespace Masterpies
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
