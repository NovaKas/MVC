using Microsoft.Owin;
using Owin;
using WebApplicationLogin.Helpers;

[assembly: OwinStartupAttribute(typeof(WebApplicationLogin.Startup))]
namespace WebApplicationLogin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            StartupHelper.SetDefaultRolesIfNotExists();
        }
    }
}
