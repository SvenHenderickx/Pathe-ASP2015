using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pathe_ASP2015.Startup))]
namespace Pathe_ASP2015
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
