using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SATApp.UI.MVC.Startup))]
namespace SATApp.UI.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
