using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Milestone2.Startup))]
namespace Milestone2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
